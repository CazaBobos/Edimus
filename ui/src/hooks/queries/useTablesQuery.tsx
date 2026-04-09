import { tablesApi } from "@/services";
import { GetTablesParams } from "@/types";

import { useAxiosQuery } from "../axiosHooks";

export const useTablesQuery = (params: GetTablesParams = {}) => {
  const query = useAxiosQuery({
    queryKey: ["tables", params],
    queryFn: () => tablesApi.findMany(params),
  });

  return {
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
