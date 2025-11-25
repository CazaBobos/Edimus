import { companiesApi } from "@/services";
import { useQuery } from "@tanstack/react-query";

export const useCompanyQuery = (companyId: number) => {
  const query = useQuery({
    queryKey: ["company", companyId],
    queryFn: () => companiesApi.findOne(companyId),
  });

  return {
    data: query.data,
    isLoading: query.isLoading,
  };
};
