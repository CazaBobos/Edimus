import { tablesApi } from "@/services";
import { CreateTableRequest, Table, TableStatus, UpdateTableRequest } from "@/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export const useTableMutations = () => {
  const queryClient = useQueryClient();

  const createTableMutation = useMutation({
    mutationFn: async (request: CreateTableRequest) => await tablesApi.create(request),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: ({ id, qrId }, variables) => {
      toast.success("La mesa se ha creado correctamente.");

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

  const updateTableMutation = useMutation({
    mutationFn: async ({ id, request }: { id: number; request: UpdateTableRequest }) =>
      await tablesApi.update(id, request),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (_, variables) => {
      toast.success("La mesa se ha actualizado correctamente.");

      queryClient.setQueriesData<Table[]>({ queryKey: ["tables"] }, (query) => {
        if (!query) return;

        const { id, request } = variables;

        return query.map((t) =>
          t.id === id
            ? {
                ...t,
                ...request,
                orders: request.status === TableStatus.Free ? [] : t.orders,
              }
            : t,
        );
      });
    },
  });

  const removeTableMutation = useMutation({
    mutationFn: async (id: number) => await tablesApi.remove(id),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      toast.success("La  mesa se ha eliminado correctamente.");

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
