import { CreateProductRequest, Product, UpdateProductRequest } from "@/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useProductMutations = () => {
  const queryClient = useQueryClient();

  const createProductMutation = useMutation({
    mutationFn: async (request: CreateProductRequest) => await Promise.resolve(1),
    onSuccess: (id, request) =>
      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;

        return [...query, { id, ...request, enabled: true }];
      }),
  });

  const updateProductMutation = useMutation({
    mutationFn: async (variables: { id: number; request: UpdateProductRequest }) => await Promise.resolve(),
    onSuccess: (_, variables) =>
      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;
        return query.map((p) =>
          p.id === variables.id
            ? {
                ...p,
                ...variables.request,
              }
            : p,
        );
      }),
  });

  return {
    createProductMutation,
    updateProductMutation,
  };
};
