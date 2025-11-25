import { tablesApi } from "@/services";
import { useQuery } from "@tanstack/react-query";
import { useSearchParams } from "next/navigation";

export const useSingleTableQuery = () => {
  const searchParams = useSearchParams();

  const tableId = searchParams.get("tableId");

  const query = useQuery({
    queryKey: ["table", tableId],
    queryFn: () => tablesApi.link(tableId as string),
    enabled: !!tableId,
  });

  return {
    data: query.data,
    isLoading: query.isLoading,
  };
};
