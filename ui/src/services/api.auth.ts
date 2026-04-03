import type { ResetPasswordRequest } from "@/types";
import { LoginRequest, LoginResponse } from "@/types";
import { AxiosResponse } from "axios";

import { axiosClient } from "./axios";

export const authApi = {
  login: async (request: LoginRequest) => {
    const response: AxiosResponse<LoginResponse> = await axiosClient.post(`auth/login`, request);

    return response.data;
  },

  refresh: async (refreshToken: string) => {
    const response: AxiosResponse<LoginResponse> = await axiosClient.post(`auth/refresh`, { refreshToken });

    return response.data;
  },

  resetPassword: async (request: ResetPasswordRequest) => {
    await axiosClient.post(`auth/reset-password`, request);
  },
};
