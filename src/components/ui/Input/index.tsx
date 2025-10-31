import { ChangeEvent } from "react";

import { ControlState } from "../common";

type InputProps = {
  name?: string;
  title?: string;
  value?: string | number;
  defaultValue?: string | number;
  onChange?: (state: ControlState) => void;
};

export const Input = (props: InputProps) => {
  const { name, title, value, defaultValue, onChange } = props;

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;

    onChange?.({ name, value });
  };

  return (
    <div>
      {title && <span>{title}</span>}
      <input type="text" name={name} value={value} defaultValue={defaultValue} onChange={handleChange} />
    </div>
  );
};
