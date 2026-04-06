import { useAppUserStore } from "@/stores";
import { LoginResponse } from "@/types";
import axios, { AxiosInstance, InternalAxiosRequestConfig } from "axios";

const API_URI: string = process.env.NEXT_PUBLIC_API_BASE_URL!;

export const axiosClient: AxiosInstance = axios.create({
  baseURL: API_URI,
  timeout: 30000,
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
  },
  paramsSerializer: {
    indexes: null,
  },
});

// Tracks whether a token refresh is already in progress.
let isRefreshing = false;

// Requests that arrived while a refresh was in progress are held here.
// Once the refresh settles, they are either retried or rejected in bulk.
let pendingQueue: Array<{ resolve: (value?: unknown) => void; reject: (err: unknown) => void }> = [];

// Resolves or rejects every queued request depending on whether the refresh succeeded.
const drainQueue = (error: unknown) => {
  pendingQueue.forEach((p) => (error ? p.reject(error) : p.resolve()));
  pendingQueue = [];
};

const ISO_DATE_REGEX = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/;

const parseDates = (value: unknown): unknown => {
  if (typeof value === "string" && ISO_DATE_REGEX.test(value)) return new Date(value);
  if (Array.isArray(value)) return value.map(parseDates);
  if (value !== null && typeof value === "object") {
    return Object.fromEntries(Object.entries(value).map(([k, v]) => [k, parseDates(v)]));
  }
  return value;
};

axiosClient.interceptors.response.use(
  (response) => {
    response.data = parseDates(response.data);
    return response;
  },
  async (error) => {
    const axiosError = error as {
      config: InternalAxiosRequestConfig & { _retry?: boolean };
      response?: { status: number };
    };
    const original = axiosError.config;

    // Only handle 401s. The _retry flag prevents infinite loops if the
    // retried request itself comes back with another 401.
    if (axiosError.response?.status !== 401 || original._retry) {
      return Promise.reject(error);
    }

    if (isRefreshing) {
      // A refresh is already underway — park this request in the queue.
      // It will be retried automatically once the refresh completes.
      return new Promise((resolve, reject) => {
        pendingQueue.push({ resolve, reject });
      }).then(() => axiosClient(original));
    }

    original._retry = true;
    isRefreshing = true;

    try {
      // The browser sends the refreshToken HTTP-only cookie automatically.
      // On success the server sets fresh token + refreshToken cookies in the response.
      const response = await axiosClient.post<LoginResponse>(`auth/refresh`);
      useAppUserStore.getState().renewSession(response.data.refreshTokenExpiresAt);
      drainQueue(null);
      return axiosClient(original); // retry the original request with the new cookie
    } catch (refreshError) {
      // Refresh failed (expired / invalid) — reject all queued requests and log the user out.
      drainQueue(refreshError);
      useAppUserStore.getState().endSession();
      return Promise.reject(refreshError);
    } finally {
      isRefreshing = false;
    }
  },
);
