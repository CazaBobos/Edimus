import { useAdminStore } from "@/stores";
import { Coords } from "@/types";
import { ReactNode } from "react";

import styles from "./styles.module.scss";

type SquareProps = {
  color: string;
  borderColor?: string;
  content?: ReactNode;
  filled?: boolean;
  position: Coords;
  onClick?: () => void;
  onMouseDown?: () => void;
  onMouseEnter?: () => void;
};

export const Square = (props: SquareProps) => {
  const squareSize = useAdminStore((store) => store.squareSize);
  const { borderColor, color, filled, position, content, onClick, onMouseDown, onMouseEnter } = props;

  const cursor = onClick ? "pointer" : onMouseDown ? "crosshair" : "default";

  return (
    <div
      onClick={onClick}
      onMouseDown={onMouseDown}
      onMouseEnter={onMouseEnter}
      className={styles.square}
      style={{
        cursor,
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
