import { useCompanyQuery } from "@/hooks/queries/useCompanyQuery";
import { BiLayer, BiWine, BiArea } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Select } from "@/components/ui/Select";

import { SaloonGrid } from "./SaloonGrid";
import { SectorDialog } from "./SectorDialog";
import styles from "./styles.module.scss";
import { TableDialog } from "./TableDialog";

export const SaloonPanel = () => {
  const { data: company } = useCompanyQuery(1);

  return (
    <div className={styles.container}>
      <h2>
        <div>
          Plano de salón
          <Select title="Plano actual:" options={company?.premises[0].layouts.map((l) => l.name) ?? []} />
        </div>
        <Button label="Planos" icon={<BiLayer />} onClick={() => {}} />
        <Button label="Mesas" icon={<BiWine />} onClick={() => {}} />
        <Button label="Sectores" icon={<BiArea />} onClick={() => {}} />
      </h2>
      <SaloonGrid />
      <TableDialog />
      <SectorDialog />
    </div>
  );
};
