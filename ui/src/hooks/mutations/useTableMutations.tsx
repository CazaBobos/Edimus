import { useToast } from "@/hooks/useToast";
import { tablesApi } from "@/services";
import { CreateTableRequest, Table, TableStatus, UpdateTableRequest } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

export const useTableMutations = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError, showInfo } = useToast();

  const createTableMutation = useAxiosMutation({
    mutationFn: async (request: CreateTableRequest) => await tablesApi.create(request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: ({ id, qrId }, variables) => {
      showSuccess("La mesa se ha creado correctamente.");

      queryClient.setQueriesData<Table[]>({ queryKey: ["tables"] }, (query) => {
        if (!query) return;

        return [
          ...query,
          {
            ...variables,
            id,
            qrId,
            status: variables.status ?? TableStatus.Free,
            orders: [],
          },
        ];
      });
    },
  });

  const updateTableMutation = useAxiosMutation({
    mutationFn: async ({ id, request }: { id: number; request: UpdateTableRequest }) =>
      await tablesApi.update(id, request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, variables) => {
      showSuccess("La mesa se ha actualizado correctamente.");

      queryClient.setQueriesData<Table[]>({ queryKey: ["tables"] }, (query) => {
        if (!query) return;

        const { id, request } = variables;

        return query.map((t) =>
          t.id === id
            ? {
                ...t,
                ...request,
                orders: request.status === TableStatus.Free ? [] : (request.orders ?? t.orders),
              }
            : t,
        );
      });
    },
  });

  const removeTableMutation = useAxiosMutation({
    mutationFn: async (id: number) => await tablesApi.remove(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("La mesa se ha eliminado correctamente.");

      queryClient.setQueriesData<Table[]>({ queryKey: ["tables"] }, (query) => {
        if (!query) return;

        return query.filter((s) => s.id !== id);
      });
    },
  });

  return {
    createTableMutation,
    updateTableMutation,
    removeTableMutation,
  };
};
