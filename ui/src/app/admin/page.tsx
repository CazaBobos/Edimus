"use client";
import { useAppUserStore } from "@/stores";
import { useState } from "react";

import { Card } from "@/components/ui/Card";
import { Tabs } from "@/components/ui/Tabs";
import { AdminHeader } from "@/components/views/Admin/AdminHeader";
import { IngredientsPanel } from "@/components/views/Admin/IngredientsPanel";
import { LoginCard } from "@/components/views/Admin/LoginCard";
import { MenuPanel } from "@/components/views/Admin/MenuPanel";
import { SaloonPanel } from "@/components/views/Admin/SaloonPanel";

import styles from "./styles.module.scss";

export default function Admin() {
  const [selectedTab, setSelectedTab] = useState(0);

  const planeTags = ["Plano de Salon", "Menú", "Stock de Ingredientes"];

  const user = useAppUserStore((store) => store.user);

  const isValidSession = () => {
    if (!user) return false;

    const sessionExpiration = new Date(user.created)?.getTime() + user.expiresIn * 60000;
    if (new Date(sessionExpiration) > new Date()) {
      return true;
    } else {
      return false;
    }
  };

  return (
    <div className={styles.page}>
      <AdminHeader />
      {!isValidSession() ? (
        <LoginCard />
      ) : (
        <div className={styles.content}>
          <Tabs source={planeTags} active={selectedTab} onChange={setSelectedTab} />
          <Card className={styles.mainCard}>
            {
              {
                0: <SaloonPanel />,
                1: <MenuPanel />,
                2: <IngredientsPanel />,
              }[selectedTab]
            }
          </Card>
        </div>
      )}
    </div>
  );
}
