import { useAdminStore, useAppUserStore } from "@/stores";
import { BiMenu } from "react-icons/bi";

import { HeaderPanel } from "./HeaderPanel";
import styles from "./styles.module.scss";

export const AdminHeader = () => {
  const isLoggedIn = useAppUserStore((store) => store.isLoggedIn);
  const isPanelOpen = useAdminStore((store) => store.headerPanelState.open);
  const setHeaderPanelState = useAdminStore((store) => store.setHeaderPanelState);
  const openHeaderPanel = () => {
    setHeaderPanelState({
      open: !isPanelOpen,
    });
  };

  return (
    <div className={styles.title}>
      <h1>Ēdimus - Admin</h1>
      {isLoggedIn() && <BiMenu className={styles.menu} onClick={openHeaderPanel} />}
      <HeaderPanel />
    </div>
  );
};
