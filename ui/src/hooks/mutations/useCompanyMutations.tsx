import { useToast } from "@/hooks/useToast";
import { companiesApi } from "@/services";
import { Company, UpdateCompanyRequest } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

export const useCompanyMutations = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError } = useToast();

  const updateSettingsMutation = useAxiosMutation({
    mutationFn: async ({ id, request }: { id: number; request: UpdateCompanyRequest }) =>
      await companiesApi.update(id, request),
    onError: () => {
      showError("Ocurrió un error al guardar la configuración.");
    },
    onSuccess: (_, { id, request }) => {
      showSuccess("Configuración guardada correctamente.");
      queryClient.setQueriesData<Company>(
        {
          queryKey: ["company", id],
        },
        (query) => {
          if (!query) return query;
          return {
            ...query,
            ...request,
          };
        },
      );
    },
  });

  return { updateSettingsMutation };
};
