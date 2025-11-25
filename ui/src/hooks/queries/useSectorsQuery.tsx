import { sectorsApi } from "@/services";
import { GetSectorsParams } from "@/types";
import { useQuery } from "@tanstack/react-query";

export const useSectorsQuery = (params: GetSectorsParams = {}) => {
  const query = useQuery({
    queryKey: ["sectors", params],
    queryFn: () => sectorsApi.findMany(params),
  });

  return {
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
