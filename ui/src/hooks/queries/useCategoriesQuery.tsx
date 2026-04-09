import { categoriesApi } from "@/services";
import { GetCategoriesParams } from "@/types";

import { useAxiosQuery } from "../axiosHooks";

export const useCategoriesQuery = (params: GetCategoriesParams = {}) => {
  const query = useAxiosQuery({
    queryKey: ["categories", params],
    queryFn: () => categoriesApi.findMany(params),
  });

  return {
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
