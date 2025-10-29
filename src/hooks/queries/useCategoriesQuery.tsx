import { categoriesApi } from "@/services";
import { GetCategoriesParams } from "@/types";
import { useQuery } from "@tanstack/react-query";

export const useCategoriesQuery = (params: GetCategoriesParams = {}) => {
  const query = useQuery({
    queryKey: ["categories", params],
    queryFn: () => categoriesApi.findMany(params),
  });

  return { data: query.data ?? [] };
};
