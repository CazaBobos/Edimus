import { CreateTableRequest, GetTablesParams, Table, UpdateTableRequest } from "@/types";

import { axiosClient } from "./axios";

export const tablesApi = {
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

  link: async (tableId: string) => {
    const response = await axiosClient.get<Table>(`tables/${tableId}`);

    return response.data;
  },
  call: async (id: number) => {
    const response = await axiosClient.put<void>(`tables/${id}/call`);

    return response.data;
  },
};
