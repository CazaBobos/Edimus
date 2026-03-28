import { colors } from "@/common/colors";
import { useAdminStore } from "@/stores";
import { Coords } from "@/types";
import { ReactNode, useEffect, useRef, useState } from "react";
import { BiEraser, BiSolidCheckSquare } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import { Square } from "../SaloonGrid/Square";
import styles from "./styles.module.scss";

type SurfaceEditorProps = {
  content?: ReactNode;
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
  const surfaceRef = useRef<Coords[]>(defaultValue);

  const isPainting = useRef(false);
  const paintMode = useRef<"add" | "remove">("add");

  useEffect(() => {
    const stop = () => {
      isPainting.current = false;
    };
    window.addEventListener("mouseup", stop);
    return () => window.removeEventListener("mouseup", stop);
  }, []);

  const applyPaint = (coords: Coords, mode: "add" | "remove") => {
    if (coords.x === 0 && coords.y === 0) return;

    const prev = surfaceRef.current;
    const exists = prev.some((s) => s.x === coords.x && s.y === coords.y);

    if (mode === "add" && exists) return;
    if (mode === "remove" && !exists) return;

    const next = mode === "add" ? [...prev, coords] : prev.filter((c) => c.x !== coords.x || c.y !== coords.y);

    surfaceRef.current = next;
    setCurrentSurface(next);
    onChange(next);
  };

  const handleMouseDown = (coords: Coords) => {
    if (coords.x === 0 && coords.y === 0) return;
    const exists = surfaceRef.current.some((s) => s.x === coords.x && s.y === coords.y);
    paintMode.current = exists ? "remove" : "add";
    isPainting.current = true;
    applyPaint(coords, paintMode.current);
  };

  const handleMouseEnter = (coords: Coords) => {
    if (!isPainting.current) return;
    applyPaint(coords, paintMode.current);
  };

  const handleFillAll = () => {
    const all: Coords[] = [];
    for (let x = 0; x < width; x++) {
      for (let y = 0; y < height; y++) {
        all.push({ x: x + offset.x, y: y + offset.y });
      }
    }
    surfaceRef.current = all;
    setCurrentSurface(all);
    onChange(all);
  };

  const handleClearAll = () => {
    const origin = [{ x: 0, y: 0 }];
    surfaceRef.current = origin;
    setCurrentSurface(origin);
    onChange(origin);
  };

  return (
    <div className={styles.container}>
      <span>Superficie</span>
      <div
        className={styles.editor}
        style={{
          height: `${squareSize * height}px`,
          width: `${squareSize * width}px`,
        }}
        onMouseLeave={() => {
          isPainting.current = false;
        }}
        draggable={false}
      >
        {[...Array(height).keys()].map((_, x) =>
          [...Array(width).keys()].map((_, y) => {
            const centerX = x + offset.x;
            const centerY = y + offset.y;
            const isOrigin = centerX === 0 && centerY === 0;
            return (
              <Square
                key={[x, y].join(",")}
                position={{ x, y }}
                borderColor={colors.lightText}
                color={colors.grey}
                content={isOrigin ? content : ""}
                filled={currentSurface.some((s) => s.x === centerX && s.y === centerY)}
                onMouseDown={() => handleMouseDown({ x: centerX, y: centerY })}
                onMouseEnter={() => handleMouseEnter({ x: centerX, y: centerY })}
              />
            );
          }),
        )}
      </div>
      <div className={styles.actions}>
        <Button icon={<BiSolidCheckSquare />} label="Rellenar" onClick={handleFillAll} />
        <Button icon={<BiEraser />} label="Limpiar" onClick={handleClearAll} />
      </div>
    </div>
  );
};
