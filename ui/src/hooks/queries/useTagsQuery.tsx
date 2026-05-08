import { tagsApi } from "@/services";
import { GetTagsParams } from "@/types";

import { useAxiosQuery } from "../axiosHooks";

export const useTagsQuery = (params: GetTagsParams = {}) => {
  const query = useAxiosQuery({
    queryKey: ["tags", params],
    queryFn: () => tagsApi.findMany(params),
  });

  return {
    data: query.data ?? [],
    isLoading: query.isLoading,
  };
};
