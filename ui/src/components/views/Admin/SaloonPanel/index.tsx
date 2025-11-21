import { useAdminStore } from "@/stores";
import { BiWine, BiArea } from "react-icons/bi";

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
        <span>Plano de salón</span>
        <div>
          <Button label="Añadir Mesa" icon={<BiWine />} onClick={() => setTableDialogOpenState(null)} />
          <Button label="Añadir Sector" icon={<BiArea />} onClick={() => setSectorDialogOpenState(null)} />
        </div>
      </h2>
      <SaloonGrid />
      <TableDialog />
      <SectorDialog />
    </div>
  );
};
