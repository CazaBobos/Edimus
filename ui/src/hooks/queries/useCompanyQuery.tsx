import { companiesApi } from "@/services";

import { useAxiosQuery } from "../axiosHooks";

export const useCompanyQuery = (companyId: number | null) => {
  const query = useAxiosQuery({
    queryKey: ["company", companyId],
    queryFn: () => companiesApi.findOne(companyId!),
    enabled: !!companyId,
  });

  return {
    data: query.data,
    isLoading: query.isLoading,
  };
};
