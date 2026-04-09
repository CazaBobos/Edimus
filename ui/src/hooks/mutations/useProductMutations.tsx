import { useToast } from "@/hooks/useToast";
import { productsApi } from "@/services";
import { Product, ProductRequest } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

export const useProductMutations = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError, showInfo } = useToast();

  const createProductMutation = useAxiosMutation({
    mutationFn: async (request: Required<ProductRequest>) => await productsApi.create(request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (id, request) => {
      showSuccess("El producto se ha creado correctamente.");

      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;

        return [...query, { id, ...request, enabled: true }];
      });
    },
  });

  const updateProductMutation = useAxiosMutation({
    mutationFn: async ({ id, request }: { id: number; request: ProductRequest }) =>
      await productsApi.update(id, request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, variables) => {
      showSuccess("El producto se ha actualizado correctamente.");

      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;
        return query.map((p) => (p.id === variables.id ? { ...p, ...variables.request } : p));
      });
    },
  });

  const removeProductMutation = useAxiosMutation({
    mutationFn: async (id: number) => await productsApi.remove(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("El producto se ha eliminado correctamente.");

      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;
        return query.map((p) => (p.id === id ? { ...p, enabled: false } : p));
      });
    },
  });

  const restoreProductMutation = useAxiosMutation({
    mutationFn: async (id: number) => await productsApi.restore(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("El producto se ha restaurado correctamente.");

      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;
        return query.map((p) => (p.id === id ? { ...p, enabled: true } : p));
      });
    },
  });

  return {
    createProductMutation,
    updateProductMutation,
    removeProductMutation,
    restoreProductMutation,
  };
};
