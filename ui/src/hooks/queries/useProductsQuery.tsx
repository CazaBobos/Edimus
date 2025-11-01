import { productsApi } from "@/services";
import { GetProductsParams } from "@/types";
import { useQuery } from "@tanstack/react-query";

export const useProductsQuery = (params: GetProductsParams = {}) => {
  const query = useQuery({
    queryKey: ["products", params],
    queryFn: () => productsApi.findMany(params),
  });

  return { data: query.data ?? [] };
};
