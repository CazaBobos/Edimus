import { useAdminStore } from "@/stores";

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
        <div>
          <span>Plano actual: </span>
          <select name="" id="" style={{ height: "32px" }}>
            {planeOptions.map((o) => (
              <option key={o} value={o}>
                {o}
              </option>
            ))}
          </select>
        </div>
        {tableDialogOpenState !== 0 ? (
          <TableCard />
        ) : (
          <div className={styles.panel}>
            <button className={styles.actionButton}>Añadir o quitar planos</button>
            <button className={styles.actionButton}>Editor de mesas</button>
            <button className={styles.actionButton}>Editar sectores</button>
          </div>
        )}
      </div>
    </>
  );
};
