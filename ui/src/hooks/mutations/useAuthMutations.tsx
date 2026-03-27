import { useToast } from "@/hooks/useToast";
import { authApi } from "@/services/api.auth";
import { useAppUserStore } from "@/stores";
import { LoginRequest } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "next/navigation";

export const useAuthMutations = () => {
  const router = useRouter();
  const setUser = useAppUserStore((store) => store.setUser);
  const { showInfo, showError, clearToast } = useToast();

  const loginMutation = useMutation({
    mutationFn: async (request: LoginRequest) => await authApi.login(request),
    onMutate: () => {
      showInfo("Por favor, espere...");
    },
    onError: () => {
      showError("Ha ocurrido un error. Por favor, intente de nuevo.");
    },
    onSuccess: (response) => {
      clearToast();
      setUser(response);
      router.refresh();
    },
  });

  return { loginMutation };
};
