import { LoginRequest, LoginResponse } from "@/types";
import { AxiosResponse } from "axios";

import { axiosClient } from "./axios";

export const authApi = {
  login: async (request: LoginRequest) => {
    const response: AxiosResponse<LoginResponse> = await axiosClient.post(`auth/login`, request);

    return response.data;
  },
};
