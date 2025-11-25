import { ingredientsApi } from "@/services";
import { GetIngredientsParams } from "@/types";
import { useQuery } from "@tanstack/react-query";

export const useIngredientsQuery = (params: GetIngredientsParams = {}) => {
  const query = useQuery({
    queryKey: ["ingredients", params],
    queryFn: () => ingredientsApi.findMany(params),
  });

  return {
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
