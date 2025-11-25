import { sectorsApi } from "@/services";
import { CreateSectorRequest, Sector, UpdateSectorRequest } from "@/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export const useSectorMutations = () => {
  const queryClient = useQueryClient();

  const createSectorMutation = useMutation({
    mutationFn: async (request: CreateSectorRequest) => await sectorsApi.create(request),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (id, variables) => {
      toast.success("El sector se ha creado correctamente.");

      queryClient.setQueriesData<Sector[]>({ queryKey: ["sectors"] }, (query) => {
        if (!query) return;

        return [...query, { id, ...variables }];
      });
    },
  });

  const updateSectorMutation = useMutation({
    mutationFn: async ({ id, request }: { id: number; request: UpdateSectorRequest }) =>
      await sectorsApi.update(id, request),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (_, variables) => {
      toast.success("El sector se ha actualizado correctamente.");

      queryClient.setQueriesData<Sector[]>({ queryKey: ["sectors"] }, (query) => {
        if (!query) return;

        return query.map((s) => (s.id === variables.id ? { ...s, ...variables.request } : s));
      });
    },
  });

  const removeSectorMutation = useMutation({
    mutationFn: async (id: number) => await sectorsApi.remove(id),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      toast.success("El sector se ha eliminado correctamente.");

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
