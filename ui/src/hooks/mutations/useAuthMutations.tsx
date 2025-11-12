import { authApi } from "@/services/api.auth";
import { useAppUserStore } from "@/stores";
import { LoginRequest } from "@/types";
import { useMutation } from "@tanstack/react-query";

export const useAuthMutations = () => {
  const setUser = useAppUserStore((store) => store.setUser);

  const loginMutation = useMutation({
    mutationFn: async (request: LoginRequest) => await authApi.login(request),
    onSuccess: (response) => setUser(response),
  });

  return { loginMutation };
};
