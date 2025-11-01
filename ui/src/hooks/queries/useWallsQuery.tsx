import { wallsApi } from "@/services";
import { useQuery } from "@tanstack/react-query";

export const useWallsQuery = () => {
  const query = useQuery({
    queryKey: ["walls"],
    queryFn: () => wallsApi.findMany(),
  });

  return { data: query.data ?? [] };
};
