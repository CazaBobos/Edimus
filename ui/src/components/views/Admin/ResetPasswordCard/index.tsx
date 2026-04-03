"use client";

import { useAuthMutations } from "@/hooks/mutations/useAuthMutations";
import { ResetPasswordRequest } from "@/types";
import { useState } from "react";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";
import { ControlState } from "@/components/ui/common";
import { Input } from "@/components/ui/Input";

import styles from "./styles.module.scss";

type Props = {
  userId: number;
  token: string;
};

export const ResetPasswordCard = ({ userId, token }: Props) => {
  const [request, setRequest] = useState<ResetPasswordRequest>({
    userId,
    token,
    newPassword: "",
  });

  const handleSetRequest = (state: ControlState) => {
    setRequest((prev) => ({ ...prev, [state.name]: state.value }));
  };

  const { resetPasswordMutation } = useAuthMutations();

  return (
    <Card className={styles.reset}>
      <strong>Restablecer Contraseña</strong>
      <Input
        width="100%"
        name="newPassword"
        title="Nueva Contraseña"
        type="password"
        value={request.newPassword}
        onChange={handleSetRequest}
      />
      <Button label="Confirmar" onClick={() => resetPasswordMutation.mutate(request)} disabled={!request.newPassword} />
    </Card>
  );
};
