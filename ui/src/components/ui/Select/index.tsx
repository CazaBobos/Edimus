import { ChangeEvent } from "react";

import { ControlState } from "../common";
import styles from "./styles.module.scss";

export type SelectOption = {
  label: string;
  value: string | number;
};

type SelectProps = {
  name?: string;
  title?: string;
  options: SelectOption[] | string[];
  value?: string;
  defaultValue?: string;
  onChange?: (state: ControlState) => void;
};

export const Select = (props: SelectProps) => {
  const { name, title, options, value, defaultValue, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = event.target;

    onChange?.({ name, value });
  };

  return (
    <div className={styles.select}>
      {title && <span>{title}</span>}
      <select
        name={name}
        style={{ height: "32px", width: "100%" }}
        value={value}
        defaultValue={defaultValue}
        onChange={handleChange}
      >
        {options.map((o) => {
          const option = typeof o === "string" ? { label: o, value: o } : o;
          return (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          );
        })}
      </select>
    </div>
  );
};
