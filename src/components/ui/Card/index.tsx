import clsx from "clsx";
import { HTMLAttributes } from "react";

import styles from "./styles.module.scss";

type CardProps = Pick<HTMLAttributes<HTMLDivElement>, "children" | "className" | "onClick">;

export const Card = (props: CardProps) => {
  const { children, className, onClick } = props;

  return (
    <div className={clsx(styles.card, className)} onClick={onClick}>
      {children}
    </div>
  );
};
