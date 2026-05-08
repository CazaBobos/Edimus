import { useToast } from "@/hooks/useToast";
import { tagsApi } from "@/services";
import { Tag } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

export const useTagMutations = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError, showInfo } = useToast();

  const createTagMutation = useAxiosMutation({
    mutationFn: async (name: string) => await tagsApi.create(name),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (id, name) => {
      showSuccess("Etiqueta creada correctamente.");
      queryClient.setQueriesData<Tag[]>({ queryKey: ["tags"] }, (query) => {
        if (!query) return;
        return [...query, { id, name, enabled: true }];
      });
    },
  });

  const updateTagMutation = useAxiosMutation({
    mutationFn: async ({ id, name }: { id: number; name: string }) => await tagsApi.update(id, name),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, { id, name }) => {
      showSuccess("Etiqueta actualizada correctamente.");
      queryClient.setQueriesData<Tag[]>({ queryKey: ["tags"] }, (query) => {
        if (!query) return;
        return query.map((t) => (t.id === id ? { ...t, name } : t));
      });
    },
  });

  const removeTagMutation = useAxiosMutation({
    mutationFn: async (id: number) => await tagsApi.remove(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("Etiqueta eliminada correctamente.");
      queryClient.setQueriesData<Tag[]>({ queryKey: ["tags"] }, (query) => {
        if (!query) return;
        return query.map((t) => (t.id === id ? { ...t, enabled: false } : t));
      });
    },
  });

  const restoreTagMutation = useAxiosMutation({
    mutationFn: async (id: number) => await tagsApi.restore(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("Etiqueta restaurada correctamente.");
      queryClient.setQueriesData<Tag[]>({ queryKey: ["tags"] }, (query) => {
        if (!query) return;
        return query.map((t) => (t.id === id ? { ...t, enabled: true } : t));
      });
    },
  });

  return { createTagMutation, updateTagMutation, removeTagMutation, restoreTagMutation };
};
