import { useAdminStore } from "@/stores";
import { BiWine, BiArea } from "react-icons/bi";

import { Accordion } from "@/components/ui/Accordion";
import { Button } from "@/components/ui/Button";

import { SaloonGrid } from "./SaloonGrid";
import { SectorDialog } from "./SectorDialog";
import styles from "./styles.module.scss";
import { TableDialog } from "./TableDialog";

export const SaloonPanel = () => {
  const { setTableDialogOpenState, setSectorDialogOpenState } = useAdminStore();

  return (
    <div className={styles.container}>
      <h2>
        Plano de salón
        <Accordion title="Nuevo">
          <Button label="Mesa" icon={<BiWine />} onClick={() => setTableDialogOpenState(null)} />
          <Button label="Sector" icon={<BiArea />} onClick={() => setSectorDialogOpenState(null)} />
        </Accordion>
      </h2>
      <SaloonGrid />
      <TableDialog />
      <SectorDialog />
    </div>
  );
};
