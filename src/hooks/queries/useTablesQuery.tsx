import { tablesApi } from "@/services";
import { useQuery } from "@tanstack/react-query";

export const useTablesQuery = () => {
  const query = useQuery({
    queryKey: ["tables"],
    queryFn: () => tablesApi.findMany(),
  });

  return { data: query.data ?? [] };
};
