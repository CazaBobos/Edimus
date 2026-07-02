"use client";

import { useCompanyBySlugQuery } from "@/hooks/queries/useCompanyBySlugQuery";
import { useSingleTableQuery } from "@/hooks/queries/useSingleTableQuery";
import { Badge, Loader } from "@mantine/core";
import { useSearchParams } from "next/navigation";
import { Suspense } from "react";

import { FiltersDialog } from "@/components/views/Menu/FiltersDialog";
import { MenuBar } from "@/components/views/Menu/MenuBar";
import { MenuCards } from "@/components/views/Menu/MenuCards";
import { OrderDialog } from "@/components/views/Menu/OrderDialog";

import styles from "./page.module.scss";

function MenuContent() {
  const searchParams = useSearchParams();
  const slug = searchParams.get("slug") ?? "";

  const { data: company, isLoading } = useCompanyBySlugQuery(slug);
  const { data: table } = useSingleTableQuery();

  if (isLoading) {
    return (
      <div className={styles.page} style={{ alignItems: "center", justifyContent: "center" }}>
        <Loader size="sm" />
      </div>
    );
  }

  if (!company) {
    return (
      <div className={styles.empty}>
        <h1>Ēdimus</h1>
        {!slug ? (
          <p>Escaneá el código QR de tu mesa para acceder al menú.</p>
        ) : (
          <p>No encontramos el restaurante que buscás.</p>
        )}
      </div>
    );
  }

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
        <MenuCards companyId={company.id} publicPrices={company.publicPrices} hasTable={!!table} />
        <OrderDialog />
        <FiltersDialog companyId={company.id} />
      </main>
      <MenuBar publicOrders={company.publicOrders} />
    </div>
  );
}

export default function Menu() {
  return (
    <Suspense>
      <MenuContent />
    </Suspense>
  );
}
