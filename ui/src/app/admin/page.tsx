"use client";

import { useUrlSync } from "@/hooks/useUrlSync";
import { useAdminStore, useAppUserStore } from "@/stores";
import { useEffect, useState } from "react";

import { AdminSidebar } from "@/components/views/Admin/AdminSidebar";
import { IngredientsManager } from "@/components/views/Admin/IngredientsManager";
import { LoginCard } from "@/components/views/Admin/LoginCard";
import { MenuEditor } from "@/components/views/Admin/MenuEditor";
import { SaloonPanel } from "@/components/views/Admin/SaloonPanel";
import { SettingsPanel } from "@/components/views/Admin/SettingsPanel";
import { Statistics } from "@/components/views/Admin/Statistics";

import styles from "./styles.module.scss";

export default function Admin() {
  useUrlSync();

  const selectedTab = useAdminStore((store) => store.headerPanelState.selectedTab);

  const isLoggedIn = useAppUserStore((store) => store.isLoggedIn);

  const [mounted, setMounted] = useState(false);
  useEffect(() => setMounted(true), []);

  if (!mounted) return null;

  if (!isLoggedIn()) {
    return (
      <div className={styles.loginPage}>
        <div className={styles.loginContent}>
          <div className={styles.loginBrand}>
            <h1>Ēdimus</h1>
            <p>Panel de Administración</p>
          </div>
          <LoginCard />
        </div>
      </div>
    );
  }

  return (
    <div className={styles.page}>
      <AdminSidebar />
      <main className={styles.main}>
        {
          {
            0: <SaloonPanel />,
            1: <MenuEditor />,
            2: <IngredientsManager />,
            3: <Statistics />,
            4: <SettingsPanel />,
          }[selectedTab]
        }
      </main>
    </div>
  );
}
