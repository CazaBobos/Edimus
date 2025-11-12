import { GetIngredientsParams, Ingredient, IngredientRequest } from "@/types";

import { axiosClient } from "./axios";

export const ingredientsApi = {
  findMany: async (params: GetIngredientsParams) => {
    const response = await axiosClient.get<Ingredient[]>("ingredients", { params });

    return response.data;
  },
  create: async (request: Required<IngredientRequest>) => {
    const response = await axiosClient.post<number>("ingredients", request);

    return response.data;
  },
  update: async (id: number, request: IngredientRequest) => {
    const response = await axiosClient.put<void>(`ingredients/${id}`, request);

    return response.data;
  },
  remove: async (id: number) => {
    const response = await axiosClient.delete<void>(`ingredients/${id}`);

    return response.data;
  },
  restore: async (id: number) => {
    const response = await axiosClient.patch<void>(`ingredients/${id}`);

    return response.data;
  },
};
