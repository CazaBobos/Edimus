"use client";
import { useAdminStore, useAppUserStore } from "@/stores";

import { Card } from "@/components/ui/Card";
import { AdminHeader } from "@/components/views/Admin/AdminHeader";
import { IngredientsManager } from "@/components/views/Admin/IngredientsManager";
import { LoginCard } from "@/components/views/Admin/LoginCard";
import { MenuEditor } from "@/components/views/Admin/MenuEditor";
import { SaloonPanel } from "@/components/views/Admin/SaloonPanel";

import styles from "./styles.module.scss";

export default function Admin() {
  const selectedTab = useAdminStore((store) => store.headerPanelState.selectedTab);

  const isLoggedIn = useAppUserStore((store) => store.isLoggedIn);

  return (
    <div className={styles.page}>
      <AdminHeader />
      <div className={styles.content}>
        {!isLoggedIn() ? (
          <LoginCard />
        ) : (
          <Card className={styles.mainCard}>
            {
              {
                0: <SaloonPanel />,
                1: <MenuEditor />,
                2: <IngredientsManager />,
              }[selectedTab]
            }
          </Card>
        )}
      </div>
    </div>
  );
}
