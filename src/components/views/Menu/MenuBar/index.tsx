import { useMenuStore } from "@/stores";
import { BiFilterAlt } from "react-icons/bi";
import { PiCallBell, PiBasket } from "react-icons/pi";

import styles from "./styles.module.scss";

export const MenuBar = () => {
  const { setIsFiltersDialogOpen, setIsOrderDialogOpen } = useMenuStore();
  return (
    <>
      <button className={styles.button} onClick={() => setIsFiltersDialogOpen(true)}>
        <BiFilterAlt size={32} />
      </button>
      <button className={styles.button} onClick={() => setIsOrderDialogOpen(true)}>
        <PiBasket size={32} />
      </button>
      <button className={styles.button}>
        <PiCallBell size={32} />
      </button>
    </>
  );
};
