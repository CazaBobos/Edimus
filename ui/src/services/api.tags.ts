import { GetTagsParams, Tag } from "@/types";

import { axiosClient } from "./axios";

export const tagsApi = {
  findMany: async (params: GetTagsParams = {}) => {
    const response = await axiosClient.get<Tag[]>("tags", { params });
    return response.data;
  },
  create: async (request: { companyId: number; name: string }) => {
    const response = await axiosClient.post<number>("tags", request);
    return response.data;
  },
  update: async (id: number, name: string) => {
    await axiosClient.put(`tags/${id}`, { name });
  },
  remove: async (id: number) => {
    await axiosClient.delete(`tags/${id}`);
  },
  restore: async (id: number) => {
    await axiosClient.patch(`tags/${id}`);
  },
};
