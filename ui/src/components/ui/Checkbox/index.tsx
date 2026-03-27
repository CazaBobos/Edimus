import { Checkbox as MantineCheckbox } from "@mantine/core";
import { ChangeEvent } from "react";

import { ControlState } from "../common";

type CheckboxProps = {
  name?: string;
  title?: string;
  checked?: boolean;
  defaultChecked?: boolean;
  onChange?: (state: ControlState) => void;
};

export const Checkbox = ({ name, title, checked, defaultChecked, onChange }: CheckboxProps) => {
  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    onChange?.({ name, value });
  };

  return (
    <MantineCheckbox
      name={name}
      label={title}
      checked={checked}
      defaultChecked={defaultChecked}
      onChange={handleChange}
    />
  );
};
