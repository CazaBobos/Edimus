import { useToast } from "@/hooks/useToast";
import { sectorsApi } from "@/services";
import { CreateSectorRequest, Sector, UpdateSectorRequest } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

export const useSectorMutations = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError, showInfo } = useToast();

  const createSectorMutation = useAxiosMutation({
    mutationFn: async (request: CreateSectorRequest) => await sectorsApi.create(request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (id, variables) => {
      showSuccess("El sector se ha creado correctamente.");

      queryClient.setQueriesData<Sector[]>({ queryKey: ["sectors"] }, (query) => {
        if (!query) return;

        return [...query, { id, ...variables }];
      });
    },
  });

  const updateSectorMutation = useAxiosMutation({
    mutationFn: async ({ id, request }: { id: number; request: UpdateSectorRequest }) =>
      await sectorsApi.update(id, request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, variables) => {
      showSuccess("El sector se ha actualizado correctamente.");

      queryClient.setQueriesData<Sector[]>({ queryKey: ["sectors"] }, (query) => {
        if (!query) return;

        return query.map((s) => (s.id === variables.id ? { ...s, ...variables.request } : s));
      });
    },
  });

  const removeSectorMutation = useAxiosMutation({
    mutationFn: async (id: number) => await sectorsApi.remove(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("El sector se ha eliminado correctamente.");

      queryClient.setQueriesData<Sector[]>({ queryKey: ["sectors"] }, (query) => {
        if (!query) return;

        return query.filter((s) => s.id !== id);
      });
    },
  });

  return {
    createSectorMutation,
    updateSectorMutation,
    removeSectorMutation,
  };
};
