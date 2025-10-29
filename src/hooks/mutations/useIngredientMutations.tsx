import { Ingredient } from "@/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useSaloonMutations = () => {
  const queryClient = useQueryClient();

  const createIngredientMutation = useMutation({
    mutationFn: async (request: unknown) => await Promise.resolve(1),
    onSuccess: (id, request) =>
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return query;
      }),
  });

  const updateIngredientMutation = useMutation({
    mutationFn: async (variables: { id: number; request: unknown }) => await Promise.resolve(),
    onSuccess: (_, variables) =>
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return query;
      }),
  });

  const removeIngredientMutation = useMutation({
    mutationFn: async (id: number) => await Promise.resolve(),
    onSuccess: (_, id) =>
      queryClient.setQueriesData<Ingredient[]>({ queryKey: ["ingredients"] }, (query) => {
        if (!query) return;

        return query.filter((i) => i.id !== id);
      }),
  });

  return {
    createIngredientMutation,
    updateIngredientMutation,
    removeIngredientMutation,
  };
};
