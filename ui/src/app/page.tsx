"use client";

import { useSingleTableQuery } from "@/hooks/queries/useSingleTableQuery";

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
        <div className={styles.title}>
          <div>
            <h1>Ēdimus</h1>
            <b>Nunc est Ēdimus</b>
          </div>
          {table && <span>Nº Mesa: {table.id}</span>}
        </div>
      </header>
      <main className={styles.main}>
        <MenuCards />
        <OrderDialog />
        <FiltersDialog />
      </main>
      <footer className={styles.footer}>
        <MenuBar />
      </footer>
    </div>
  );
}
