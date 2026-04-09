import { Input } from "@/components/ui/Input";
import { Select } from "@/components/ui/Select";

import styles from "./styles.module.scss";

const today = () => new Date().toISOString().split("T")[0];

type GroupBy = "day" | "week" | "month";

type RangeControlsProps = {
  rangeFrom: string;
  rangeTo: string;
  groupBy: GroupBy;
  onFromChange: (v: string) => void;
  onToChange: (v: string) => void;
  onGroupByChange: (v: GroupBy) => void;
};

const GROUP_BY_OPTIONS = [
  { label: "Día", value: "day" },
  { label: "Semana", value: "week" },
  { label: "Mes", value: "month" },
];

export const RangeControls = ({
  rangeFrom,
  rangeTo,
  groupBy,
  onFromChange,
  onToChange,
  onGroupByChange,
}: RangeControlsProps) => (
  <section className={styles.controls}>
    <Input type="date" title="Desde" value={rangeFrom} max={rangeTo} onChange={({ value }) => onFromChange(value)} />
    <Input
      type="date"
      title="Hasta"
      value={rangeTo}
      min={rangeFrom}
      max={today()}
      onChange={({ value }) => onToChange(value)}
    />
    <Select
      title="Agrupar por"
      options={GROUP_BY_OPTIONS}
      value={groupBy}
      onChange={({ value }) => onGroupByChange(value as GroupBy)}
    />
  </section>
);
