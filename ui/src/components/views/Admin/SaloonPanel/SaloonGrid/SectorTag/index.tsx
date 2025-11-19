import { useAdminStore } from "@/stores";
import { Sector } from "@/types";

import styles from "./styles.module.scss";

type SectorTagProps = {
  sector: Sector;
};
export const SectorTag = (props: SectorTagProps) => {
  const { sector } = props;

  const setSectorDialogOpenState = useAdminStore((store) => store.setSectorDialogOpenState);

  return (
    <span
      onClick={() => setSectorDialogOpenState(sector)}
      className={styles.tag}
      style={{
        background: sector.color,
      }}
    >
      {sector.name}
    </span>
  );
};
