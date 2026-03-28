"use client";

import { useAdminStore, useAppUserStore } from "@/stores";
import { useRouter } from "next/navigation";
import { BiGrid, BiBook, BiBox, BiUser, BiLogOut } from "react-icons/bi";

import styles from "./styles.module.scss";

const NAV_ITEMS = [
  { label: "Plano de Salón", icon: <BiGrid />, tab: 0 },
  { label: "Menú", icon: <BiBook />, tab: 1 },
  { label: "Ingredientes", icon: <BiBox />, tab: 2 },
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
          <button
            key={item.tab}
            className={`${styles.navItem} ${selectedTab === item.tab ? styles.navItemActive : ""}`}
            onClick={() => handleTabChange(item.tab)}
          >
            <span className={styles.navIcon}>{item.icon}</span>
            <span>{item.label}</span>
          </button>
        ))}
      </nav>

      <div className={styles.userArea}>
        <div className={styles.userInfo}>
          <div className={styles.avatar}>
            <BiUser />
          </div>
          <span className={styles.username}>{user?.username}</span>
        </div>
        <button className={styles.logoutBtn} onClick={handleLogout} title="Cerrar sesión">
          <BiLogOut />
        </button>
      </div>
    </aside>
  );
};
