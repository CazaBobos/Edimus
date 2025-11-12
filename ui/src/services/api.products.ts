import { CreateProductRequest, GetProductsParams, Product, UpdateProductRequest } from "@/types";

import { axiosClient } from "./axios";

export const productsApi = {
  findMany: async (params: GetProductsParams) => {
    const response = await axiosClient.get<Product[]>("products", { params });

    return response.data;
  },
  create: async (request: CreateProductRequest) => {
    const response = await axiosClient.post<number>("products", request);

    return response.data;
  },
  update: async (id: number, request: UpdateProductRequest) => {
    const response = await axiosClient.put<void>(`products/${id}`, request);

    return response.data;
  },
  remove: async (id: number) => {
    const response = await axiosClient.delete<void>(`products/${id}`);

    return response.data;
  },
  restore: async (id: number) => {
    const response = await axiosClient.patch<void>(`products/${id}`);

    return response.data;
  },
};
