import { TextInput, Textarea } from "@mantine/core";
import { ChangeEvent } from "react";

import { ControlState } from "../common";

type InputProps = {
  width?: string;
  type?: "text" | "password" | "number";
  multiline?: boolean;
  name?: string;
  title?: string;
  value?: string | number;
  defaultValue?: string | number;
  placeholder?: string;
  onChange?: (state: ControlState) => void;
};

export const Input = (props: InputProps) => {
  const { width, type = "text", multiline, name, title, value, defaultValue, placeholder, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = event.target;
    onChange?.({ name, value });
  };

  const stringValue = value !== undefined ? String(value) : undefined;
  const stringDefault = defaultValue !== undefined ? String(defaultValue) : undefined;

  const commonProps = {
    style: { width },
    name,
    label: title,
    value: stringValue,
    defaultValue: stringDefault,
    placeholder,
    onChange: handleChange,
  };

  if (multiline) {
    return <Textarea {...commonProps} autosize minRows={2} />;
  }

  return <TextInput {...commonProps} type={type} />;
};
