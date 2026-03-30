import { ingredientsApi } from "@/services";
import { GetIngredientsParams, Ingredient } from "@/types";
import { useQuery } from "@tanstack/react-query";
import { useMemo } from "react";

export const useIngredientsQuery = (params: GetIngredientsParams = {}) => {
  const query = useQuery({
    queryKey: ["ingredients", params],
    queryFn: () => ingredientsApi.findMany(params),
  });

  const ingredientsMap = useMemo(() => {
    if (!query.data) return undefined;

    return new Map<number, Ingredient>(query.data.map((i) => [i.id, i]));
  }, [query.data]);

  return {
    ingredientsMap,
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
