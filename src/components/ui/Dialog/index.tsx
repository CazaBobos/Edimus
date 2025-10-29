import { ReactNode } from "react";

import { Card } from "../Card";
import styles from "./styles.module.scss";

type DialogProps = {
  open: boolean;
  onClose: () => void;
  children: ReactNode;
};
export const Dialog = (props: DialogProps) => {
  const { open, onClose, children } = props;

  const handleClose = (e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    e.stopPropagation();
    onClose();
  };

  if (!open) return null;
  return (
    <div className={styles.backdrop} onClick={handleClose}>
      <Card className={styles.dialog}>{children}</Card>
    </div>
  );
};
