import { authApi } from "@/services/api.auth";
import { useAppUserStore } from "@/stores";
import { LoginRequest } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { toast } from "react-toastify";

export const useAuthMutations = () => {
  const setUser = useAppUserStore((store) => store.setUser);

  const loginMutation = useMutation({
    mutationFn: async (request: LoginRequest) => await authApi.login(request),
    onMutate: () => {
      toast.info("Por favor, espere...");
    },
    onError: () => {
      toast.error("Ha ocurrido un error. Por favor, intente de nuevo.");
    },
    onSuccess: (response) => {
      toast.dismiss();
      setUser(response);
    },
  });

  return { loginMutation };
};
