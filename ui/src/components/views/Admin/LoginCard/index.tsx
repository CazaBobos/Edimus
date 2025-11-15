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
    userOrEmail: "DbSeeder",
    password: "T3s7P@ssw0rd",
  });
  const handleSetRequest = (state: ControlState) => {
    const { name, value } = state;

    setRequest((prev) => ({ ...prev, [name]: value }));
  };
  const { loginMutation } = useAuthMutations();

  const onSubmit = () => {
    loginMutation.mutate(request);
  };

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
      <Button label="Ingresar" onClick={onSubmit} disabled={!request.userOrEmail || !request.password} />
    </Card>
  );
};
