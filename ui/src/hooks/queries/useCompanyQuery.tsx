import { companiesApi } from "@/services";
import { useAppUserStore } from "@/stores";

import { useAxiosQuery } from "../axiosHooks";

export const useCompanyQuery = () => {
  const companyId = useAppUserStore((s) => s.activeCompanyId);

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
