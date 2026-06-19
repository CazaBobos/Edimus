import { companiesApi } from "@/services";

import { useAxiosQuery } from "../axiosHooks";

export const useCompanyBySlugQuery = (slug: string) => {
  const query = useAxiosQuery({
    queryKey: ["company-slug", slug],
    queryFn: () => companiesApi.findBySlug(slug),
    enabled: !!slug,
  });

  return {
    data: query.data,
    isLoading: query.isLoading,
  };
};
