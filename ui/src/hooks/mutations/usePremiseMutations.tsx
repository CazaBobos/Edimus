import { useToast } from "@/hooks/useToast";
import { premisesApi } from "@/services";
import { Company, CreatePremiseRequest, UpdatePremiseRequest } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

export const usePremiseMutations = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError, showInfo } = useToast();

  const createPremiseMutation = useAxiosMutation({
    mutationFn: async (request: CreatePremiseRequest) => await premisesApi.create(request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (id, { companyId, name }) => {
      showSuccess("Local creado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: [...company.premises, { id, companyId, name, enabled: true, layouts: [] }],
        };
      });
    },
  });

  const updatePremiseMutation = useAxiosMutation({
    mutationFn: async ({ id, ...request }: { id: number } & UpdatePremiseRequest) =>
      await premisesApi.update(id, request),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, { id, name }) => {
      showSuccess("Local actualizado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: company.premises.map((p) => (p.id === id ? { ...p, ...(name && { name }) } : p)),
        };
      });
    },
  });

  const removePremiseMutation = useAxiosMutation({
    mutationFn: async (id: number) => await premisesApi.remove(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("Local eliminado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: company.premises.map((p) => (p.id === id ? { ...p, enabled: false } : p)),
        };
      });
    },
  });

  const restorePremiseMutation = useAxiosMutation({
    mutationFn: async (id: number) => await premisesApi.restore(id),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, id) => {
      showSuccess("Local restaurado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: company.premises.map((p) => (p.id === id ? { ...p, enabled: true } : p)),
        };
      });
    },
  });

  return { createPremiseMutation, updatePremiseMutation, removePremiseMutation, restorePremiseMutation };
};
