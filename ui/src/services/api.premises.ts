import { CreatePremiseRequest, GetPremisesParams, Premise, UpdatePremiseRequest } from "@/types";

import { axiosClient } from "./axios";

export const premisesApi = {
  findMany: async (params: GetPremisesParams = {}) => {
    const response = await axiosClient.get<Premise[]>("premises", { params });
    return response.data;
  },
  create: async (request: CreatePremiseRequest) => {
    const response = await axiosClient.post<number>("premises", request);
    return response.data;
  },
  update: async (id: number, request: UpdatePremiseRequest) => {
    await axiosClient.put(`premises/${id}`, request);
  },
  remove: async (id: number) => {
    await axiosClient.delete(`premises/${id}`);
  },
  restore: async (id: number) => {
    await axiosClient.patch(`premises/${id}`);
  },
};
