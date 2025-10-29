import { ingredientsApi } from "@/services";
import { useQuery } from "@tanstack/react-query";

export const useIngredientsQuery = () => {
  const query = useQuery({
    queryKey: ["ingredients"],
    queryFn: () => ingredientsApi.findMany(),
  });

  return { data: query.data ?? [] };
};
