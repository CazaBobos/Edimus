import { CreateTableRequest, GetTablesParams, Table, UpdateTableRequest } from "@/types";

import { axiosClient } from "./axios";

export const tablesApi = {
  findOne: async (id: number) => {
    const response = await axiosClient.get<Table>(`tables/${id}`);

    return response.data;
  },
  findMany: async (params: GetTablesParams) => {
    const response = await axiosClient.get<Table[]>("tables", { params });

    return response.data;
  },
  create: async (request: CreateTableRequest) => {
    const response = await axiosClient.post<{ id: number; qrId: string }>("tables", request);

    return response.data;
  },
  update: async (id: number, request: UpdateTableRequest) => {
    const response = await axiosClient.put<void>(`tables/${id}`, request);

    return response.data;
  },
  remove: async (id: number) => {
    const response = await axiosClient.delete<void>(`tables/${id}`);

    return response.data;
  },
  link: async (qrId: string) => {
    const response = await axiosClient.post<Table>(`tables/${qrId}/link`);

    return response.data;
  },
  call: async (id: number) => {
    const response = await axiosClient.put<void>(`tables/${id}/call`);

    return response.data;
  },
};
