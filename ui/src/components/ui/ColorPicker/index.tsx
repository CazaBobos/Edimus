import { ColorInput } from "@mantine/core";

import { ControlState } from "../common";

type ColorPickerProps = {
  name?: string;
  value?: string;
  defaultValue?: string;
  onChange?: (state: ControlState) => void;
};

export const ColorPicker = ({ name, value, defaultValue, onChange }: ColorPickerProps) => (
  <ColorInput
    label="Color"
    value={value}
    defaultValue={defaultValue}
    format="hex"
    onChange={(color) => onChange?.({ name: name ?? "", value: color })}
  />
);
