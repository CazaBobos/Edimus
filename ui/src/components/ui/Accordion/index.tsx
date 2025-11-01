import { ReactNode, useState } from "react";
import { BiChevronDown, BiChevronUp } from "react-icons/bi";

import { Button } from "../Button";
import styles from "./styles.module.scss";

type AccordionProps = {
  title: string;
  children: ReactNode;
  defaultOpen?: boolean;
};
export const Accordion = (props: AccordionProps) => {
  const { title, children, defaultOpen = false } = props;

  const [open, setOpen] = useState<boolean>(defaultOpen);
  const toggleOpen = () => setOpen(!open);

  return (
    <div className={styles.container}>
      <Button label={title} icon={open ? <BiChevronDown /> : <BiChevronUp />} onClick={toggleOpen} />
      {open && children}
    </div>
  );
};
