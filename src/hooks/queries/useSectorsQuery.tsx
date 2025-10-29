import { sectorsApi } from "@/services";
import { useQuery } from "@tanstack/react-query";

export const useSectorsQuery = () => {
  const query = useQuery({
    queryKey: ["sectors"],
    queryFn: () => sectorsApi.findMany(),
  });

  return { data: query.data ?? [] };
};
