import { Coords } from "@/types";
import { useCallback, useEffect, useState } from "react";
import { BiSolidUpArrow, BiSolidDownArrow, BiSolidRightArrow, BiSolidLeftArrow } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import styles from "./styles.module.scss";

type PositionerProps = {
  positionX?: number;
  positionY?: number;
  onChange: (coords: Coords) => void;
};
export const Positioner = (props: PositionerProps) => {
  const { positionX = 0, positionY = 0, onChange } = props;

  const [currentCoords, setCurrentCoords] = useState<Coords>({ x: positionX, y: positionY });

  const up = () => handleChange({ x: currentCoords.x, y: currentCoords.y - 1 });
  const left = () => handleChange({ x: currentCoords.x - 1, y: currentCoords.y });
  const right = () => handleChange({ x: currentCoords.x + 1, y: currentCoords.y });
  const down = () => handleChange({ x: currentCoords.x, y: currentCoords.y + 1 });

  const handleChange = useCallback(
    (coords: Coords) => {
      setCurrentCoords(coords);
      onChange(coords);
    },
    [onChange],
  );

  useEffect(() => {
    const deltas: Partial<Record<string, Coords>> = {
      ArrowUp: { x: 0, y: -1 },
      ArrowDown: { x: 0, y: 1 },
      ArrowLeft: { x: -1, y: 0 },
      ArrowRight: { x: 1, y: 0 },
    };

    const onKeyDown = (e: KeyboardEvent) => {
      const delta = deltas[e.key];
      if (!delta) return;
      e.preventDefault();
      handleChange({ x: currentCoords.x + delta.x, y: currentCoords.y + delta.y });
    };

    window.addEventListener("keydown", onKeyDown);
    return () => window.removeEventListener("keydown", onKeyDown);
  }, [currentCoords, handleChange]);

  return (
    <div className={styles.positioner}>
      <div className={styles.row}>
        <span>X: {currentCoords.x}</span>
        <span>Y: {currentCoords.y}</span>
      </div>
      <Button icon={<BiSolidUpArrow />} onClick={up} />
      <div className={styles.row}>
        <Button icon={<BiSolidLeftArrow />} onClick={left} />
        <div className={styles.spacer} />
        <Button icon={<BiSolidRightArrow />} onClick={right} />
      </div>
      <Button icon={<BiSolidDownArrow />} onClick={down} />
    </div>
  );
};
