import { useAppUserStore } from "@/stores";
import axios, { AxiosInstance } from "axios";

const API_URI: string = "http://localhost:5183/api/";

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
