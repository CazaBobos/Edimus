import { tablesApi } from "@/services";
import { GetTablesParams } from "@/types";
import { useQuery } from "@tanstack/react-query";

export const useTablesQuery = (params: GetTablesParams = {}) => {
  const query = useQuery({
    queryKey: ["tables", params],
    queryFn: () => tablesApi.findMany(params),
  });

  return {
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
