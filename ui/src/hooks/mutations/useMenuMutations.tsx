import { tablesApi } from "@/services";
import { Table, TableStatus } from "@/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

import { useSingleTableQuery } from "../queries/useSingleTableQuery";

export const useMenuMutations = () => {
  const queryClient = useQueryClient();

  const { data: table } = useSingleTableQuery();

  const callTableMutation = useMutation({
    mutationFn: async (id: number) => await tablesApi.call(id),
    onError: () => toast.error("Ha ocurrido un error. Intente nuevamente."),
    onSuccess: () => {
      toast.success("Solicitud enviada. Aguarde y será atendido.");

      queryClient.setQueriesData<Table>({ queryKey: ["table", table?.qrId] }, (query) => {
        if (!query) return;

        return { ...query, status: TableStatus.Calling };
      });
    },
  });

  return {
    callTableMutation,
  };
};
