import { useAdminStore, useAppUserStore } from "@/stores";
import { useRouter } from "next/navigation";
import { BiExit, BiUser } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";

import styles from "./styles.module.scss";

export const HeaderPanel = () => {
  const router = useRouter();
  const user = useAppUserStore((store) => store.user);
  const endSession = useAppUserStore((store) => store.endSession);
  const headerPanelState = useAdminStore((store) => store.headerPanelState);
  const setHeaderPanelState = useAdminStore((store) => store.setHeaderPanelState);

  const handleEndSession = () => {
    endSession();
    setHeaderPanelState({ open: false });
    router.refresh();
  };

  const handleButtonClick = (tabIndex: number) => {
    setHeaderPanelState({
      open: false,
      selectedTab: tabIndex,
    });
  };

  const tabs = ["Plano de Salon", "Menú", "Stock de Ingredientes"];

  if (!headerPanelState.open) return null;
  return (
    <Card className={styles.panel}>
      <div className={styles.user}>
        <BiUser className={styles.profile} />
        <span>{user?.username}</span>
        <BiExit onClick={handleEndSession} className={styles.exit} />
      </div>
      {tabs.map((tab, i) => (
        <Button
          key={`${tab + i}`}
          label={tab}
          disabled={headerPanelState.selectedTab === i}
          onClick={() => handleButtonClick(i)}
        />
      ))}
    </Card>
  );
};
