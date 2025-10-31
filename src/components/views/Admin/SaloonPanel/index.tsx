import { useAdminStore } from "@/stores";
import { BiMove, BiPlus, BiTrash } from "react-icons/bi";

import { Accordion } from "@/components/ui/Accordion";
import { Button } from "@/components/ui/Button";
import { Select } from "@/components/ui/Select";

import { SaloonGrid } from "./SaloonGrid";
import styles from "./styles.module.scss";
import { TableCard } from "./TableCard";

export const SaloonPanel = () => {
  const { tableDialogOpenState } = useAdminStore();

  const planeOptions = ["Planta Baja"];

  return (
    <>
      <SaloonGrid />
      <div className={styles.panel}>
        {tableDialogOpenState !== 0 ? (
          <TableCard />
        ) : (
          <>
            <Accordion title="Planos">
              <div>
                <Button icon={<BiPlus size={28} />} />
                <Button icon={<BiTrash size={28} />} />
                <Button icon={<BiMove size={28} />} />
                <Select title="Plano actual:" options={planeOptions} />
              </div>
            </Accordion>
            <Accordion title="Mesas">
              <Button icon={<BiPlus size={28} />} />
              <Button icon={<BiTrash size={28} />} />
              <Button icon={<BiMove size={28} />} />
            </Accordion>
            <Accordion title="Sectores">
              <Button icon={<BiPlus size={28} />} />
              <Button icon={<BiTrash size={28} />} />
              <Button icon={<BiMove size={28} />} />
            </Accordion>
          </>
        )}
      </div>
    </>
  );
};
