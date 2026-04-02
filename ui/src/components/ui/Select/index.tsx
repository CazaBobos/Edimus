import { Select as MantineSelect, ComboboxItem, OptionsFilter } from "@mantine/core";
import { ReactNode } from "react";

import { ControlState } from "../common";
import styles from "./styles.module.scss";

export type SelectOption = {
  label: string;
  value: string | number;
  hidden?: boolean;
  disabled?: boolean;
  icon?: ReactNode;
};

type SelectProps = {
  width?: string;
  name?: string;
  title?: string;
  options: SelectOption[] | string[];
  value?: string;
  disabled?: boolean;
  onChange?: (state: ControlState) => void;
};

export const Select = (props: SelectProps) => {
  const { width, name, title, options, value, disabled, onChange } = props;

  const handleChange = (value: string | null) => {
    onChange?.({
      name: name ?? "",
      value: value ?? "",
    });
  };

  const normalized = options.map((o) => (typeof o === "string" ? { label: o, value: o } : o));
  const data = [
    { label: "-- seleccione una opción --", value: "-1", disabled: true },
    ...normalized.map((o) => ({
      label: o.label,
      value: String(o.value),
      disabled: o.hidden || o.disabled,
    })),
  ];
  const iconMap = new Map(normalized.filter((o) => o.icon).map((o) => [String(o.value), o.icon]));

  const currentIcon = value ? iconMap.get(value) : undefined;

  const filter: OptionsFilter = ({ options, search }) =>
    (options as ComboboxItem[]).filter((o) => o.disabled || o.label.toLowerCase().includes(search.toLowerCase()));

  return (
    <MantineSelect
      style={{ width }}
      label={title}
      value={value ?? null}
      disabled={disabled}
      data={data}
      filter={filter}
      allowDeselect={false}
      leftSection={currentIcon}
      renderOption={({ option }) => {
        const icon = iconMap.get(option.value);
        return icon ? (
          <div className={styles.row}>
            {icon}
            {option.label}
          </div>
        ) : (
          option.label
        );
      }}
      onChange={handleChange}
    />
  );
};
