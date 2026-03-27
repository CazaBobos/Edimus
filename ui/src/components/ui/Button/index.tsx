import { ActionIcon, Button as MantineButton } from "@mantine/core";
import { ReactNode } from "react";

type ButtonProps = {
  label?: string;
  icon?: ReactNode;
  disabled?: boolean;
  onClick?: () => void;
};

export const Button = ({ label, icon, disabled, onClick }: ButtonProps) => {
  if (!label) {
    return (
      <ActionIcon variant="default" size="lg" radius="xl" disabled={disabled} onClick={onClick}>
        {icon}
      </ActionIcon>
    );
  }

  return (
    <MantineButton variant="default" radius="xl" rightSection={icon} disabled={disabled} onClick={onClick} fullWidth>
      {label}
    </MantineButton>
  );
};
