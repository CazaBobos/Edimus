import { ChangeEvent } from "react";

import { ControlState } from "../common";
import styles from "./styles.module.scss";

type ColorPickerProps = {
  name?: string;
  value?: string;
  defaultValue?: string;
  onChange?: (state: ControlState) => void;
};
export const ColorPicker = (props: ColorPickerProps) => {
  const { name, value, defaultValue, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;

    onChange?.({ name, value });
  };

  return (
    <div className={styles.colorPicker}>
      <span>Color:</span>
      <input type="color" name={name} value={value} defaultValue={defaultValue} onChange={handleChange} />
    </div>
  );
};
