import { Switch } from "@mantine/core";
import { ChangeEvent } from "react";

import { ControlState } from "../common";

type ToggleProps = {
  name: string;
  title?: string;
  checked?: boolean;
  defaultChecked?: boolean;
  onChange?: (state: ControlState) => void;
};

export const Toggle = ({ name, title, checked, defaultChecked, onChange }: ToggleProps) => {
  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    onChange?.({ name, value: String(event.currentTarget.checked) });
  };

  return <Switch label={title} checked={checked} defaultChecked={defaultChecked} onChange={handleChange} />;
};
