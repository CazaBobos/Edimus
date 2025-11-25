import { ChangeEvent } from "react";

import { ControlState } from "../common";
import styles from "./styles.module.scss";

type CheckboxProps = {
  name?: string;
  title?: string;
  checked?: boolean;
  defaultChecked?: boolean;
  placeholder?: string;
  onChange?: (state: ControlState) => void;
};

export const Checkbox = (props: CheckboxProps) => {
  const { name, title, checked, defaultChecked, placeholder, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = event.target;

    onChange?.({ name, value });
  };

  return (
    <div className={styles.container}>
      <input
        className={styles.input}
        type="checkbox"
        name={name}
        checked={checked}
        defaultChecked={defaultChecked}
        placeholder={placeholder}
        onChange={handleChange}
      />
      {title && <span>{title}</span>}
    </div>
  );
};
