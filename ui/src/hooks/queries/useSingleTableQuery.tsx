import { tablesApi } from "@/services";
import { useSearchParams } from "next/navigation";

import { useAxiosQuery } from "../axiosHooks";

export const useSingleTableQuery = () => {
  const searchParams = useSearchParams();

  const tableId = searchParams.get("tableId");

  const query = useAxiosQuery({
    queryKey: ["table", tableId],
    queryFn: () => tablesApi.link(tableId as string),
    enabled: !!tableId,
  });

  return {
    data: query.data,
    isLoading: query.isLoading,
  };
};
