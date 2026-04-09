import { companiesApi } from "@/services";

import { useAxiosQuery } from "../axiosHooks";

export const useCompanyQuery = (companyId: number) => {
  const query = useAxiosQuery({
    queryKey: ["company", companyId],
    queryFn: () => companiesApi.findOne(companyId),
  });

  return {
    data: query.data,
    isLoading: query.isLoading,
  };
};
