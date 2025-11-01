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

  if (!open) return null;
  return (
    <div className={styles.backdrop} onClick={onClose}>
      <Card className={styles.dialog} onClick={(e) => e.stopPropagation()}>
        {children}
      </Card>
    </div>
  );
};
