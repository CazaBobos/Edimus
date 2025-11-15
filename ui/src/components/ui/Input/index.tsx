import { ChangeEvent } from "react";

import { ControlState } from "../common";
import styles from "./styles.module.scss";

type InputProps = {
  width?: string;
  type?: "text" | "password";
  multiline?: boolean;
  name?: string;
  title?: string;
  value?: string | number;
  defaultValue?: string | number;
  onChange?: (state: ControlState) => void;
};

export const Input = (props: InputProps) => {
  const { width, type = "text", multiline, name, title, value, defaultValue, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = event.target;

    onChange?.({ name, value });
  };

  return (
    <div className={styles.container} style={{ width }}>
      {title && <span>{title}</span>}
      {multiline ? (
        <textarea
          className={styles.input}
          name={name}
          value={value}
          defaultValue={defaultValue}
          onChange={handleChange}
        />
      ) : (
        <input
          className={styles.input}
          type={type}
          name={name}
          value={value}
          defaultValue={defaultValue}
          onChange={handleChange}
        />
      )}
    </div>
  );
};
