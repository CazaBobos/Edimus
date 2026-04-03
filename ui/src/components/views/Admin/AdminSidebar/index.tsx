"use client";

import { useAdminStore, useAppUserStore } from "@/stores";
import { useRouter } from "next/navigation";
import { BiBook, BiBox, BiCog, BiGrid, BiLogOut, BiUser } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import styles from "./styles.module.scss";

const NAV_ITEMS = [
  { label: "Plano de Salón", icon: <BiGrid />, tab: 0 },
  { label: "Menú", icon: <BiBook />, tab: 1 },
  { label: "Ingredientes", icon: <BiBox />, tab: 2 },
  { label: "Configuración", icon: <BiCog />, tab: 3 },
];

export const AdminSidebar = () => {
  const router = useRouter();
  const user = useAppUserStore((store) => store.user);
  const endSession = useAppUserStore((store) => store.endSession);
  const selectedTab = useAdminStore((store) => store.headerPanelState.selectedTab);
  const setHeaderPanelState = useAdminStore((store) => store.setHeaderPanelState);

  const handleTabChange = (tab: number) => {
    setHeaderPanelState({ selectedTab: tab, open: false });
  };

  const handleLogout = () => {
    endSession();
    router.refresh();
  };

  return (
    <aside className={styles.sidebar}>
      <div className={styles.brand}>
        <h2>Ēdimus</h2>
        <span className={styles.brandLabel}>Admin</span>
      </div>

      <nav className={styles.nav}>
        {NAV_ITEMS.map((item) => (
          <Button
            key={item.tab}
            variant="nav"
            label={item.label}
            icon={item.icon}
            active={selectedTab === item.tab}
            onClick={() => handleTabChange(item.tab)}
          />
        ))}
      </nav>

      <div className={styles.userArea}>
        <div className={styles.userInfo}>
          <div className={styles.avatar}>
            <BiUser />
          </div>
          <span className={styles.username}>{user?.username}</span>
        </div>
        <Button variant="subtle" danger icon={<BiLogOut size={17} />} onClick={handleLogout} title="Cerrar sesión" />
      </div>
    </aside>
  );
};
