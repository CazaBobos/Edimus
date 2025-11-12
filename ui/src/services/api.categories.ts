import { Category, CreateCategoryRequest, GetCategoriesParams, UpdateCategoryRequest } from "@/types";

import { axiosClient } from "./axios";

export const categoriesApi = {
  findMany: async (params: GetCategoriesParams) => {
    const response = await axiosClient.get<Category[]>("categories", { params });

    return response.data;
  },
  create: async (request: CreateCategoryRequest) => {
    const response = await axiosClient.post<number>("categories", request);

    return response.data;
  },
  update: async (id: number, request: UpdateCategoryRequest) => {
    const response = await axiosClient.put<void>(`categories/${id}`, request);

    return response.data;
  },
  remove: async (id: number) => {
    const response = await axiosClient.delete<void>(`categories/${id}`);

    return response.data;
  },
  restore: async (id: number) => {
    const response = await axiosClient.patch<void>(`categories/${id}`);

    return response.data;
  },
};
