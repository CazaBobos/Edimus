import { ActionIcon, Button as MantineButton } from "@mantine/core";
import { ReactNode } from "react";

import styles from "./styles.module.scss";

type CustomVariant = "brand" | "nav" | "tab" | "action" | "subtle";

type ButtonProps = {
  label?: string;
  icon?: ReactNode;
  disabled?: boolean;
  onClick?: () => void;
  // Custom variants
  variant?: CustomVariant;
  active?: boolean;
  danger?: boolean;
  count?: number;
  title?: string;
};

export const Button = ({ label, icon, disabled, onClick, variant, active, danger, count, title }: ButtonProps) => {
  if (variant) {
    return (
      <button
        className={styles[variant]}
        disabled={disabled}
        onClick={onClick}
        data-active={active}
        data-danger={danger}
        title={title}
      >
        {variant === "nav" && icon ? <span className={styles.icon}>{icon}</span> : icon}
        {label}
        {count !== undefined && <span className={styles.count}>{count}</span>}
      </button>
    );
  }

  if (!label) {
    return (
      <ActionIcon variant="default" size="lg" radius="xl" disabled={disabled} onClick={onClick} title={title}>
        {icon}
      </ActionIcon>
    );
  }

  return (
    <MantineButton variant="filled" radius="xl" rightSection={icon} disabled={disabled} onClick={onClick} fullWidth>
      {label}
    </MantineButton>
  );
};
