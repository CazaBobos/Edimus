import { ChangeEvent } from "react";

import { ControlState } from "../common";
import styles from "./styles.module.scss";

export type SelectOption = {
  label: string;
  value: string | number;
  hidden?: boolean;
};

type SelectProps = {
  width?: string;
  name?: string;
  title?: string;
  options: SelectOption[] | string[];
  value?: string;
  defaultValue?: string;
  onChange?: (state: ControlState) => void;
};

export const Select = (props: SelectProps) => {
  const { width, name, title, options, value, defaultValue, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = event.target;

    onChange?.({ name, value });
  };

  return (
    <div className={styles.select} style={{ width }}>
      {title && <span className={styles.title}>{title}</span>}
      <select
        name={name}
        style={{ height: "32px", width: "100%" }}
        value={value}
        defaultValue={defaultValue}
        onChange={handleChange}
      >
        <option hidden selected value="-1">
          -- seleccione una opción --
        </option>
        {options.map((o) => {
          const option = typeof o === "string" ? { label: o, value: o } : o;
          return (
            <option key={option.value} value={option.value} hidden={option.hidden}>
              {option.label}
            </option>
          );
        })}
      </select>
    </div>
  );
};
