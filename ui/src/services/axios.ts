import { useAppUserStore } from "@/stores";
import { LoginResponse } from "@/types";
import axios, { AxiosInstance, InternalAxiosRequestConfig } from "axios";

const API_URI: string = process.env.NEXT_PUBLIC_API_BASE_URL!;

export const axiosClient: AxiosInstance = axios.create({
  baseURL: API_URI,
  timeout: 30000,
  headers: {
    "Content-Type": "application/json",
  },
  paramsSerializer: {
    indexes: null,
  },
});

axiosClient.interceptors.request.use((requestConfig) => {
  const token = useAppUserStore.getState().user?.token;
  requestConfig.headers.Authorization = `Bearer ${token}`;
  return requestConfig;
});

let isRefreshing = false;
let pendingQueue: Array<{ resolve: (token: string) => void; reject: (err: unknown) => void }> = [];

const drainQueue = (error: unknown, token: string | null = null) => {
  pendingQueue.forEach((p) => (error ? p.reject(error) : p.resolve(token!)));
  pendingQueue = [];
};

axiosClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    const original: InternalAxiosRequestConfig & { _retry?: boolean } = error.config;

    if (error.response?.status !== 401 || original._retry) {
      return Promise.reject(error);
    }

    const refreshToken = useAppUserStore.getState().user?.refreshToken;
    if (!refreshToken) {
      useAppUserStore.getState().endSession();
      return Promise.reject(error);
    }

    if (isRefreshing) {
      return new Promise((resolve, reject) => {
        pendingQueue.push({ resolve, reject });
      }).then((token) => {
        original.headers.Authorization = `Bearer ${token}`;
        return axiosClient(original);
      });
    }

    original._retry = true;
    isRefreshing = true;

    try {
      const response = await axiosClient.post<LoginResponse>(`auth/refresh`, {
        refreshToken,
      });
      const { token, refreshToken: newRefreshToken } = response.data;

      useAppUserStore.getState().updateTokens(token, newRefreshToken);
      original.headers.Authorization = `Bearer ${token}`;
      drainQueue(null, token);

      return axiosClient(original);
    } catch (refreshError) {
      drainQueue(refreshError);
      useAppUserStore.getState().endSession();
      return Promise.reject(refreshError);
    } finally {
      isRefreshing = false;
    }
  },
);
