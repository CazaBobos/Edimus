import { useToast } from "@/hooks/useToast";
import { authApi } from "@/services/api.auth";
import { useAppUserStore } from "@/stores";
import { LoginRequest, ResetPasswordRequest } from "@/types";
import { useRouter } from "next/navigation";

import { useAxiosMutation } from "../axiosHooks";

export const useAuthMutations = () => {
  const router = useRouter();
  const { setUser, endSession } = useAppUserStore();
  const { showInfo, showError, clearToast } = useToast();

  const loginMutation = useAxiosMutation({
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

  const logoutMutation = useAxiosMutation({
    mutationFn: async () => await authApi.logout(),
    onSuccess: () => {
      endSession();
      router.refresh();
    },
  });

  const recoverPasswordMutation = useAxiosMutation({
    mutationFn: async (email: string) => await authApi.recoverPassword(email),
    onMutate: () => {
      showInfo("Por favor, espere...");
    },
    onError: () => {
      showError("No se encontró una cuenta con ese email.");
    },
    onSuccess: () => {
      clearToast();
      showInfo("Si el email existe, recibirás un enlace para restablecer tu contraseña.");
    },
  });

  const resetPasswordMutation = useAxiosMutation({
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

  return { loginMutation, logoutMutation, recoverPasswordMutation, resetPasswordMutation };
};
