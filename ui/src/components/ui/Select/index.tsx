import { NativeSelect } from "@mantine/core";
import { ChangeEvent } from "react";

import { ControlState } from "../common";

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
  disabled?: boolean;
  onChange?: (state: ControlState) => void;
};

export const Select = (props: SelectProps) => {
  const { width, name, title, options, value, defaultValue, disabled, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = event.target;
    onChange?.({ name, value });
  };

  const data = [
    { label: "-- seleccione una opción --", value: "-1", disabled: true },
    ...options.map((o) => {
      const option = typeof o === "string" ? { label: o, value: o } : o;
      return { label: option.label, value: String(option.value), disabled: !!option.hidden };
    }),
  ];

  return (
    <NativeSelect
      style={{ width }}
      name={name}
      label={title}
      value={value}
      defaultValue={defaultValue ?? "-1"}
      disabled={disabled}
      onChange={handleChange}
      data={data}
    />
  );
};
