import { useAdminStore } from "@/stores";
import { Coords } from "@/types";

import styles from "./styles.module.scss";

type SquareProps = {
  color: string;
  borderColor?: string;
  content?: string | number;
  filled?: boolean;
  position: Coords;
  onClick?: () => void;
};
export const Square = (props: SquareProps) => {
  const squareSize = useAdminStore((store) => store.squareSize);
  const { borderColor, color, filled, position, content, onClick } = props;

  return (
    <div
      onClick={onClick}
      className={styles.square}
      style={{
        cursor: onClick ? "pointer" : "default",
        top: `${position.y * squareSize}px`,
        left: `${position.x * squareSize}px`,
        border: `1px solid ${borderColor ?? color}`,
        backgroundColor: filled ? color : "transparent",
      }}
    >
      {content}
    </div>
  );
};
