import { useAuthMutations } from "@/hooks/mutations/useAuthMutations";
import { LoginRequest } from "@/types";
import { useState } from "react";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";
import { ControlState } from "@/components/ui/common";
import { Input } from "@/components/ui/Input";

import styles from "./styles.module.scss";

export const LoginCard = () => {
  const [request, setRequest] = useState<LoginRequest>({
    userOrEmail: process.env.NEXT_PUBLIC_DEV_USER ?? "",
    password: process.env.NEXT_PUBLIC_DEV_PASS ?? "",
  });
  const [recoverEmail, setRecoverEmail] = useState("");
  const [showRecover, setShowRecover] = useState(false);

  const handleSetRequest = (state: ControlState) => {
    const { name, value } = state;
    setRequest((prev) => ({ ...prev, [name]: value }));
  };

  const { loginMutation, recoverPasswordMutation } = useAuthMutations();

  if (showRecover) {
    return (
      <Card className={styles.login}>
        <strong>Recuperar Contraseña</strong>
        <Input
          width="100%"
          name="email"
          title="Email"
          value={recoverEmail}
          onChange={(s) => setRecoverEmail(s.value as string)}
        />
        <Button
          label="Enviar enlace"
          onClick={() => recoverPasswordMutation.mutate(recoverEmail)}
          disabled={!recoverEmail}
        />
        <span className={styles.forgotLink} onClick={() => setShowRecover(false)}>
          Volver
        </span>
      </Card>
    );
  }

  return (
    <Card className={styles.login}>
      <strong>Iniciar Sesión</strong>
      <Input
        width="100%"
        name="userOrEmail"
        title="Usuario o Email"
        value={request.userOrEmail}
        onChange={handleSetRequest}
      />
      <Input
        width="100%"
        name="password"
        title="Contraseña"
        type="password"
        value={request.password}
        onChange={handleSetRequest}
      />
      <Button label="Ingresar" onClick={() => loginMutation.mutate(request)} disabled={!request.userOrEmail || !request.password} />
      <span className={styles.forgotLink} onClick={() => setShowRecover(true)}>
        ¿Olvidaste tu contraseña?
      </span>
    </Card>
  );
};
