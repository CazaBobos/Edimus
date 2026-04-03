import { useToast } from "@/hooks/useToast";
import { authApi } from "@/services/api.auth";
import { useAppUserStore } from "@/stores";
import { LoginRequest, ResetPasswordRequest } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "next/navigation";

export const useAuthMutations = () => {
  const router = useRouter();
  const { setUser, endSession } = useAppUserStore();
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

  const logoutMutation = useMutation({
    mutationFn: async () => await authApi.logout(),
    onSuccess: () => {
      endSession();
      router.refresh();
    },
  });

  const resetPasswordMutation = useMutation({
    mutationFn: async (request: ResetPasswordRequest) => await authApi.resetPassword(request),
    onMutate: () => {
      showInfo("Por favor, espere...");
    },
    onError: () => {
      showError("El enlace es inválido o ha expirado.");
    },
    onSuccess: () => {
      clearToast();
      router.push("/admin");
    },
  });

  return { loginMutation, logoutMutation, resetPasswordMutation };
};
