import { productsApi } from "@/services";
import { GetProductsParams } from "@/types";

import { useAxiosQuery } from "../axiosHooks";

export const useProductsQuery = (params: GetProductsParams = {}) => {
  const query = useAxiosQuery({
    queryKey: ["products", params],
    queryFn: () => productsApi.findMany(params),
  });

  return {
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
