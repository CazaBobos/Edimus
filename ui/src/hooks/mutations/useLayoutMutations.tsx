import { useToast } from "@/hooks/useToast";
import { layoutsApi } from "@/services";
import { Company, CreateLayoutRequest, UpdateLayoutRequest } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

export const useLayoutMutations = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError, showInfo } = useToast();

  const createLayoutMutation = useAxiosMutation({
    mutationFn: async (request: CreateLayoutRequest) => await layoutsApi.create(request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (id, { premiseId, name, height, width }) => {
      showSuccess("Plano creado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: company.premises.map((p) =>
            p.id === premiseId
              ? { ...p, layouts: [...p.layouts, { id, name, height, width, enabled: true, boundaries: [] }] }
              : p,
          ),
        };
      });
    },
  });

  const updateLayoutMutation = useAxiosMutation({
    mutationFn: async ({ id, ...request }: { id: number } & UpdateLayoutRequest) =>
      await layoutsApi.update(id, request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, { id, ...fields }) => {
      showSuccess("Plano actualizado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: company.premises.map((p) => ({
            ...p,
            layouts: p.layouts.map((l) => (l.id === id ? { ...l, ...fields } : l)),
          })),
        };
      });
    },
  });

  const removeLayoutMutation = useAxiosMutation({
    mutationFn: async (id: number) => await layoutsApi.remove(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("Plano eliminado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: company.premises.map((p) => ({
            ...p,
            layouts: p.layouts.map((l) => (l.id === id ? { ...l, enabled: false } : l)),
          })),
        };
      });
    },
  });

  const restoreLayoutMutation = useAxiosMutation({
    mutationFn: async (id: number) => await layoutsApi.restore(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("Plano restaurado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: company.premises.map((p) => ({
            ...p,
            layouts: p.layouts.map((l) => (l.id === id ? { ...l, enabled: true } : l)),
          })),
        };
      });
    },
  });

  return { createLayoutMutation, updateLayoutMutation, removeLayoutMutation, restoreLayoutMutation };
};
