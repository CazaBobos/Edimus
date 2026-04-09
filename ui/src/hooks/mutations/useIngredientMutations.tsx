import { useToast } from "@/hooks/useToast";
import { ingredientsApi } from "@/services";
import { Ingredient, IngredientRequest } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

export const useIngredientMutations = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError, showInfo } = useToast();

  const createIngredientMutation = useAxiosMutation({
    mutationFn: async (request: Required<IngredientRequest>) => await ingredientsApi.create(request),
    onMutate: () => {
      showInfo("Por favor, espere...");
    },
    onError: () => {
      showError("Ocurrió un error.");
    },
    onSuccess: (id, request) => {
      showSuccess("El ingrediente fue creado correctamente.");
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return [...query, { id, ...request, enabled: true }];
      });
    },
  });

  const updateIngredientMutation = useAxiosMutation({
    mutationFn: async ({ id, request }: { id: number; request: IngredientRequest }) =>
      await ingredientsApi.update(id, request),
    onMutate: () => {
      showInfo("Por favor, espere...");
    },
    onError: () => {
      showError("Ocurrió un error.");
    },
    onSuccess: (_, variables) => {
      showSuccess("El ingrediente fue actualizado correctamente.");
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return query.map((i) => (i.id === variables.id ? { ...i, ...variables.request } : i));
      });
    },
  });

  const removeIngredientMutation = useAxiosMutation({
    mutationFn: async (id: number) => await ingredientsApi.remove(id),
    onMutate: () => {
      showInfo("Por favor, espere...");
    },
    onError: () => {
      showError("Ocurrió un error.");
    },
    onSuccess: (_, id) => {
      showSuccess("El ingrediente fue removido correctamente.");
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return query.map((i) => (i.id === id ? { ...i, enabled: false } : i));
      });
    },
  });

  const restoreIngredientMutation = useAxiosMutation({
    mutationFn: async (id: number) => await ingredientsApi.restore(id),
    onMutate: () => {
      showInfo("Por favor, espere...");
    },
    onError: () => {
      showError("Ocurrió un error.");
    },
    onSuccess: (_, id) => {
      showSuccess("El ingrediente fue restaurado correctamente.");
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return query.map((i) => (i.id === id ? { ...i, enabled: true } : i));
      });
    },
  });

  return {
    createIngredientMutation,
    updateIngredientMutation,
    removeIngredientMutation,
    restoreIngredientMutation,
  };
};
