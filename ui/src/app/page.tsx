"use client";

import { useSingleTableQuery } from "@/hooks/queries/useSingleTableQuery";
import { Badge } from "@mantine/core";

import { FiltersDialog } from "@/components/views/Menu/FiltersDialog";
import { MenuBar } from "@/components/views/Menu/MenuBar";
import { MenuCards } from "@/components/views/Menu/MenuCards";
import { OrderDialog } from "@/components/views/Menu/OrderDialog";

import styles from "./page.module.scss";

export default function Menu() {
  const { data: table } = useSingleTableQuery();

  return (
    <div className={styles.page}>
      <header className={styles.header}>
        <div className={styles.brand}>
          <h1>Ēdimus</h1>
          <span className={styles.tagline}>Nunc est Ēdimus</span>
        </div>
        {table && (
          <Badge size="lg" variant="light" color="orange">
            Mesa {table.id}
          </Badge>
        )}
      </header>

      <main className={styles.main}>
        <MenuCards />
        <OrderDialog />
        <FiltersDialog />
      </main>

      <div className={styles.fab}>
        <MenuBar />
      </div>
    </div>
  );
}
