import { ReactNode } from "react";

import styles from "./styles.module.scss";

type ButtonProps = {
  label?: string;
  icon?: ReactNode;
  disabled?: boolean;
  onClick?: () => void;
};
export const Button = (props: ButtonProps) => {
  const { label, icon, disabled, onClick } = props;

  return (
    <button className={styles.button} disabled={disabled} onClick={onClick}>
      {label && <span>{label}</span>}
      {icon}
    </button>
  );
};
