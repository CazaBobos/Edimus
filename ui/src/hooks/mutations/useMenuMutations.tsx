import { useToast } from "@/hooks/useToast";
import { tablesApi } from "@/services";
import { Table, TableStatus } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";
import { useSingleTableQuery } from "../queries/useSingleTableQuery";

export const useMenuMutations = () => {
  const queryClient = useQueryClient();
  const { data: table } = useSingleTableQuery();
  const { showSuccess, showError } = useToast();

  const callTableMutation = useAxiosMutation({
    mutationFn: async (id: number) => await tablesApi.call(id),
    onError: () => showError("Ha ocurrido un error. Intente nuevamente."),
    onSuccess: () => {
      showSuccess("Solicitud enviada. Aguarde y será atendido.");

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
