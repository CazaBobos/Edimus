import { useAdminStore } from "@/stores";
import { Coords } from "@/types";
import { useState } from "react";

import { Square } from "../SaloonGrid/Square";
import styles from "./styles.module.scss";

type SurfaceEditorProps = {
  content?: string | number;
  offset?: Coords;
  height: number;
  width: number;
  defaultValue: Coords[] | undefined;
  onChange: (surface: Coords[]) => void;
};
export const SurfaceEditor = (props: SurfaceEditorProps) => {
  const { content = "", offset = { x: 0, y: 0 }, height, width, defaultValue = [{ x: 0, y: 0 }], onChange } = props;

  const squareSize = useAdminStore((store) => store.squareSize);

  const [currentSurface, setCurrentSurface] = useState<Coords[]>(defaultValue);
  const handleClick = (coords: Coords) => {
    if (coords.x === 0 && coords.y === 0) return;

    const exists = currentSurface.some((s) => s.x === coords.x && s.y === coords.y);

    const newSurface = exists
      ? currentSurface.filter((c) => c.x !== coords.x || c.y !== coords.y)
      : [...currentSurface, coords];

    setCurrentSurface(newSurface);
    onChange(newSurface);
  };

  return (
    <div className={styles.container}>
      <span>Superficie</span>
      <div
        className={styles.editor}
        style={{
          height: `${squareSize * height}px`,
          width: `${squareSize * height}px`,
        }}
      >
        {[...Array(height).keys()].map((_, x) =>
          [...Array(width).keys()].map((_, y) => {
            const centerX = x + offset.x;
            const centerY = y + offset.y;
            return (
              <Square
                key={[x, y].join(",")}
                position={{ x, y }}
                borderColor="#f2f2f2"
                color="grey"
                content={centerX === 0 && centerY === 0 ? content : ""}
                filled={currentSurface.some((s) => s.x === centerX && s.y === centerY)}
                onClick={() => handleClick({ x: centerX, y: y + offset.y })}
              />
            );
          }),
        )}
      </div>
    </div>
  );
};
