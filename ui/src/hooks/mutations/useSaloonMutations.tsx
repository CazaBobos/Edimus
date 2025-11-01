import { Sector, Table, Wall } from "@/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useSaloonMutations = () => {
  const queryClient = useQueryClient();

  const updateSectorMutation = useMutation({
    mutationFn: async () => await Promise.resolve(),
    onSuccess: () =>
      queryClient.setQueriesData<Sector[]>({ queryKey: ["sectors"] }, (query) => {
        if (!query) return;

        return query;
      }),
  });

  const updateTableMutation = useMutation({
    mutationFn: async () => await Promise.resolve(),
    onSuccess: () =>
      queryClient.setQueriesData<Table[]>({ queryKey: ["tables"] }, (query) => {
        if (!query) return;
        return query;
      }),
  });

  const updateWallsMutation = useMutation({
    mutationFn: async () => await Promise.resolve(),
    onSuccess: () =>
      queryClient.setQueriesData<Wall[]>({ queryKey: ["walls"] }, (query) => {
        if (!query) return;
        return query;
      }),
  });

  return {
    updateSectorMutation,
    updateTableMutation,
    updateWallsMutation,
  };
};
