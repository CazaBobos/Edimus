import { productsApi } from "@/services";
import { Product, ProductRequest } from "@/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

export const useProductMutations = () => {
  const queryClient = useQueryClient();

  const createProductMutation = useMutation({
    mutationFn: async (request: Required<ProductRequest>) => await productsApi.create(request),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (id, request) => {
      toast.success("El producto se ha creado correctamente.");

      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;

        return [...query, { id, ...request, enabled: true }];
      });
    },
  });

  const updateProductMutation = useMutation({
    mutationFn: async ({ id, request }: { id: number; request: ProductRequest }) =>
      await productsApi.update(id, request),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (_, variables) => {
      toast.success("El producto se ha actualizado correctamente.");

      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;
        return query.map((p) => (p.id === variables.id ? { ...p, ...variables.request } : p));
      });
    },
  });

  const removeProductMutation = useMutation({
    mutationFn: async (id: number) => await productsApi.remove(id),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      toast.success("El producto se ha eliminado correctamente.");

      queryClient.setQueriesData<Product[]>({ queryKey: ["products"] }, (query) => {
        if (!query) return;
        return query.map((p) => (p.id === id ? { ...p, enabled: false } : p));
      });
    },
  });

  const restoreProductMutation = useMutation({
    mutationFn: async (id: number) => await productsApi.restore(id),
    onMutate: () => toast.info("Por favor, espere..."),
    onError: () => toast.error("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      toast.success("El producto se ha restaurado correctamente.");

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
