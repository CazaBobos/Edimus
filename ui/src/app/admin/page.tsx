"use client";
import { useState } from "react";

import { Card } from "@/components/ui/Card";
import { Tabs } from "@/components/ui/Tabs";
import { AdminHeader } from "@/components/views/Admin/AdminHeader";
import { IngredientsPanel } from "@/components/views/Admin/IngredientsPanel";
import { MenuPanel } from "@/components/views/Admin/MenuPanel";
import { SaloonPanel } from "@/components/views/Admin/SaloonPanel";

import styles from "./styles.module.scss";

export default function Admin() {
  const [selectedTab, setSelectedTab] = useState(0);

  const planeTags = ["Plano de Salon", "Menú", "Stock de Ingredientes"];

  return (
    <div className={styles.page}>
      <AdminHeader />
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
    </div>
  );
}
