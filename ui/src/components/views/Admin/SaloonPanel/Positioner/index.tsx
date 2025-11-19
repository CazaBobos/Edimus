import { Coords } from "@/types";
import { useState } from "react";
import { BiSolidUpArrow, BiSolidDownArrow, BiSolidRightArrow, BiSolidLeftArrow, BiCheck } from "react-icons/bi";

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

  const handleChange = (coords: Coords) => {
    setCurrentCoords(coords);
    onChange(coords);
  };

  return (
    <div className={styles.positioner}>
      <div className={styles.row}>
        <span>X: {currentCoords.x}</span>
        <span>Y: {currentCoords.y}</span>
      </div>
      <Button icon={<BiSolidUpArrow />} onClick={up} />
      <div className={styles.row}>
        <Button icon={<BiSolidLeftArrow />} onClick={left} />
        <Button icon={<BiCheck />} />
        <Button icon={<BiSolidRightArrow />} onClick={right} />
      </div>
      <Button icon={<BiSolidDownArrow />} onClick={down} />
    </div>
  );
};
