import { ingredientsApi } from "@/services";
import { Ingredient, IngredientRequest } from "@/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useIngredientMutations = () => {
  const queryClient = useQueryClient();

  const createIngredientMutation = useMutation({
    mutationFn: async (request: Required<IngredientRequest>) => await ingredientsApi.create(request),
    onSuccess: (id, request) =>
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return [...query, { id, ...request, enabled: true }];
      }),
  });

  const updateIngredientMutation = useMutation({
    mutationFn: async ({ id, request }: { id: number; request: IngredientRequest }) =>
      await ingredientsApi.update(id, request),
    onSuccess: (_, variables) =>
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return query.map((i) => (i.id === variables.id ? { ...i, ...variables.request } : i));
      }),
  });

  return {
    createIngredientMutation,
    updateIngredientMutation,
  };
};
