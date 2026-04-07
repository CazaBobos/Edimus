import type { ResetPasswordRequest } from "@/types";
import { LoginRequest, LoginResponse } from "@/types";
import { AxiosResponse } from "axios";

import { axiosClient } from "./axios";

export const authApi = {
  login: async (request: LoginRequest) => {
    const response: AxiosResponse<LoginResponse> = await axiosClient.post(`auth/login`, request);
    return response.data;
  },

  refresh: async () => {
    const response: AxiosResponse<LoginResponse> = await axiosClient.post(`auth/refresh`);
    return response.data;
  },

  logout: async () => {
    await axiosClient.post(`auth/logout`);
  },

  recoverPassword: async (email: string) => {
    await axiosClient.post(`auth/recover-password`, { email });
  },

  resetPassword: async (request: ResetPasswordRequest) => {
    await axiosClient.post(`auth/reset-password`, request);
  },
};
