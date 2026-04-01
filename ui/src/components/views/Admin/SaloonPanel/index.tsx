import { useCompanyQuery } from "@/hooks/queries/useCompanyQuery";
import { useAdminStore } from "@/stores";
import { SegmentedControl } from "@mantine/core";
import { useState } from "react";
import { BiWine, BiArea } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import { SaloonGrid } from "./SaloonGrid";
import { SectorDialog } from "./SectorDialog";
import styles from "./styles.module.scss";
import { TableDialog } from "./TableDialog";

export const SaloonPanel = () => {
  const { setTableDialogOpenState, setSectorDialogOpenState } = useAdminStore();
  const { data: company } = useCompanyQuery(1);

  const layouts = company?.premises[0].layouts ?? [];
  const [selectedLayoutId, setSelectedLayoutId] = useState<number | undefined>(undefined);

  const currentLayoutId = selectedLayoutId ?? layouts[0]?.id;

  return (
    <div className={styles.container}>
      <h2>
        <span>
          Plano de salón
          {layouts.length > 1 && (
            <SegmentedControl
              value={String(currentLayoutId)}
              onChange={(v) => setSelectedLayoutId(Number(v))}
              data={layouts.map((l) => ({ value: l.id.toString(), label: l.name }))}
            />
          )}
        </span>
        <div>
          <Button label="Añadir Mesa" icon={<BiWine />} onClick={() => setTableDialogOpenState(null)} />
          <Button label="Añadir Sector" icon={<BiArea />} onClick={() => setSectorDialogOpenState(null)} />
        </div>
      </h2>
      <SaloonGrid layoutId={currentLayoutId} />
      <TableDialog />
      <SectorDialog />
    </div>
  );
};
