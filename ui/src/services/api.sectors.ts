import { CreateSectorRequest, GetSectorsParams, Sector, UpdateSectorRequest } from "@/types";

import { axiosClient } from "./axios";

export const sectorsApi = {
  findMany: async (params: GetSectorsParams) => {
    const response = await axiosClient.get<Sector[]>("sectors", { params });

    return response.data;
  },
  create: async (request: CreateSectorRequest) => {
    const response = await axiosClient.post<number>("sectors", request);

    return response.data;
  },
  update: async (id: number, request: UpdateSectorRequest) => {
    const response = await axiosClient.put<void>(`sectors/${id}`, request);

    return response.data;
  },
  remove: async (id: number) => {
    const response = await axiosClient.delete<void>(`sectors/${id}`);

    return response.data;
  },
};
