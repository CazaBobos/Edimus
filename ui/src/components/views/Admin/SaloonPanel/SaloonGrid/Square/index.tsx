import { useAdminStore } from "@/stores";

import styles from "./styles.module.scss";

type SquareProps = {
  color: string;
  content?: string | number;
  filled?: boolean;
  position: {
    x: number;
    y: number;
  };
  onClick?: () => void;
};
export const Square = (props: SquareProps) => {
  const squareSize = useAdminStore((store) => store.squareSize);
  const { color, filled, position, content, onClick } = props;

  return (
    <div
      onClick={onClick}
      className={styles.square}
      style={{
        cursor: onClick ? "pointer" : "default",
        top: `${position.y * squareSize}px`,
        left: `${position.x * squareSize}px`,
        border: `1px solid ${color}`,
        backgroundColor: filled ? color : "transparent",
      }}
    >
      {content}
    </div>
  );
};
