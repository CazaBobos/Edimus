import { Paper } from "@mantine/core";
import { HTMLAttributes } from "react";

type CardProps = Pick<HTMLAttributes<HTMLDivElement>, "children" | "className" | "onClick">;

export const Card = ({ children, className, onClick }: CardProps) => (
  <Paper shadow="sm" withBorder p="sm" radius="md" className={className} onClick={onClick}>
    {children}
  </Paper>
);
