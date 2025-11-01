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
  const { color, filled, position, content, onClick } = props;

  return (
    <div
      onClick={onClick}
      className={styles.square}
      style={{
        cursor: onClick ? "pointer" : "default",
        top: `${position.y * 32}px`,
        left: `${position.x * 32}px`,
        border: `1px solid ${color}`,
        backgroundColor: filled ? color : "transparent",
      }}
    >
      {content}
    </div>
  );
};
