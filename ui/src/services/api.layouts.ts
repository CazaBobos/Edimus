import { CreateLayoutRequest, GetLayoutsParams, Layout, UpdateLayoutRequest } from "@/types";

import { axiosClient } from "./axios";

export const layoutsApi = {
  findMany: async (params: GetLayoutsParams = {}) => {
    const response = await axiosClient.get<Layout[]>("layouts", { params });
    return response.data;
  },
  create: async (request: CreateLayoutRequest) => {
    const response = await axiosClient.post<number>("layouts", request);
    return response.data;
  },
  update: async (id: number, request: UpdateLayoutRequest) => {
    await axiosClient.put(`layouts/${id}`, request);
  },
  remove: async (id: number) => {
    await axiosClient.delete(`layouts/${id}`);
  },
  restore: async (id: number) => {
    await axiosClient.patch(`layouts/${id}`);
  },
};
