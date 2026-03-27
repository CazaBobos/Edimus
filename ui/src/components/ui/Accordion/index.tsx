import { Accordion as MantineAccordion } from "@mantine/core";
import { ReactNode } from "react";

type AccordionProps = {
  title: string;
  children: ReactNode;
  defaultOpen?: boolean;
};

export const Accordion = ({ title, children, defaultOpen = false }: AccordionProps) => (
  <MantineAccordion defaultValue={defaultOpen ? "item" : null}>
    <MantineAccordion.Item value="item">
      <MantineAccordion.Control>{title}</MantineAccordion.Control>
      <MantineAccordion.Panel>{children}</MantineAccordion.Panel>
    </MantineAccordion.Item>
  </MantineAccordion>
);
