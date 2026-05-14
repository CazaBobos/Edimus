import { useToast } from "@/hooks/useToast";
import { layoutsApi } from "@/services";
import { BoundaryType, Company } from "@/types";
import { useQueryClient } from "@tanstack/react-query";

import { useAxiosMutation } from "../axiosHooks";

type BoundaryCell = { x: number; y: number; type: BoundaryType };

export const useBoundaryMutation = () => {
  const queryClient = useQueryClient();
  const { showSuccess, showError, showInfo } = useToast();

  const saveBoundariesMutation = useAxiosMutation({
    mutationFn: async ({ id, boundaries }: { id: number; boundaries: BoundaryCell[] }) =>
      await layoutsApi.updateBoundaries(id, boundaries),
    onMutate: () => showInfo("Por favor, espere..."),
    onError: () => showError("Ha ocurrido un error."),
    onSuccess: (_, { id, boundaries }) => {
      showSuccess("Plano guardado correctamente.");
      queryClient.setQueriesData<Company>({ queryKey: ["company"] }, (company) => {
        if (!company) return;
        return {
          ...company,
          premises: company.premises.map((p) => ({
            ...p,
            layouts: p.layouts.map((l) => (l.id === id ? { ...l, boundaries } : l)),
          })),
        };
      });
    },
  });

  return { saveBoundariesMutation };
};
