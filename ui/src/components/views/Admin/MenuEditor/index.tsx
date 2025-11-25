import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useAdminStore } from "@/stores";
import { Product } from "@/types";
import { useMemo } from "react";
import React from "react";
import { BiPlus, BiSolidCircle } from "react-icons/bi";

import { Accordion } from "@/components/ui/Accordion";
import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";

import { ProductDialog } from "./ProductDialog";
import styles from "./styles.module.scss";
import { VariantsList } from "./VariantsList";

export const MenuEditor = () => {
  const { setProductDialogOpenState } = useAdminStore();
  const { data: products } = useProductsQuery();
  const { data: categories } = useCategoriesQuery();

  const categoryMap = useMemo(() => {
    const map: Record<string, Product[]> = {};
    categories.forEach((c) => {
      map[c.name] = products.filter((p) => !!p.categoryId && p.categoryId === c.id);
    });
    return map;
  }, [categories, products]);

  return (
    <div className={styles.container}>
      <h2>
        Editor de Menú
        <Button label="Nuevo Producto" icon={<BiPlus />} onClick={() => setProductDialogOpenState(null)} />
      </h2>
      <div className={styles.products}>
        {Object.entries(categoryMap).map(([category, products], i) => (
          <Accordion title={category} key={i}>
            <div className={styles.categorySection}>
              {products.map((p) => (
                <Card key={p.id} className={styles.card} onClick={() => setProductDialogOpenState(p)}>
                  <span>{p.name}</span>
                  <BiSolidCircle className={styles.status} data-enabled={p.enabled} />
                  <VariantsList product={p} />
                </Card>
              ))}
            </div>
          </Accordion>
        ))}
      </div>
      <ProductDialog />
    </div>
  );
};
