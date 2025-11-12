import { ChangeEvent } from "react";

import { ControlState } from "../common";
import styles from "./styles.module.scss";

type InputProps = {
  type?: "text" | "password";
  name?: string;
  title?: string;
  value?: string | number;
  defaultValue?: string | number;
  onChange?: (state: ControlState) => void;
};

export const Input = (props: InputProps) => {
  const { type = "text", name, title, value, defaultValue, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;

    onChange?.({ name, value });
  };

  return (
    <div className={styles.input}>
      {title && <span>{title}</span>}
      <input
        type={type}
        style={{ height: "32px" }}
        name={name}
        value={value}
        defaultValue={defaultValue}
        onChange={handleChange}
      />
    </div>
  );
};
