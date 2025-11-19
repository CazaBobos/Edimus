import { useAdminStore } from "@/stores";
import { ReactNode } from "react";

import styles from "./styles.module.scss";

type AreaProps = {
  positionX?: number;
  positionY?: number;
  children: ReactNode;
};
export const Area = (props: AreaProps) => {
  const { positionX = 0, positionY = 0, children } = props;
  const squareSize = useAdminStore((store) => store.squareSize);

  return (
    <div
      className={styles.area}
      style={{
        left: `${positionX * squareSize}px`,
        top: `${positionY * squareSize}px`,
      }}
    >
      {children}
    </div>
  );
};
