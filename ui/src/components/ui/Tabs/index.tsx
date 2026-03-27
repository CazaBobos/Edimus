import { Tabs as MantineTabs } from "@mantine/core";

type TabsProps = {
  source: string[];
  active: number;
  onChange: (index: number) => void;
};

export const Tabs = ({ source, active, onChange }: TabsProps) => (
  <MantineTabs value={String(active)} onChange={(v) => onChange(Number(v ?? 0))}>
    <MantineTabs.List>
      {source.map((t, i) => (
        <MantineTabs.Tab key={t} value={String(i)}>
          {t}
        </MantineTabs.Tab>
      ))}
    </MantineTabs.List>
  </MantineTabs>
);
