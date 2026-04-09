import { sectorsApi } from "@/services";
import { GetSectorsParams } from "@/types";

import { useAxiosQuery } from "../axiosHooks";

export const useSectorsQuery = (params: GetSectorsParams = {}) => {
  const query = useAxiosQuery({
    queryKey: ["sectors", params],
    queryFn: () => sectorsApi.findMany(params),
  });

  return {
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
