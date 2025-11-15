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

  const variantsMap = useMemo(() => {
    const map: Record<number, Product[]> = {};

    products.forEach((product) => {
      if (!product.parentId) {
        if (!map[product.id]) map[product.id] = [];
      } else if (map[product.parentId]) {
        map[product.parentId] = [...map[product.parentId], product];
      }
    });

    return map;
  }, [products]);

  return (
    <div className={styles.container}>
      <h2>
        Editor de Menú
        <Button label="Nuevo Producto" icon={<BiPlus />} onClick={() => setProductDialogOpenState(0)} />
      </h2>
      <div className={styles.products}>
        {Object.entries(categoryMap).map(([category, products], i) => (
          <Accordion title={category} key={i}>
            <div className={styles.categorySection}>
              {products.map((p) => (
                <Card key={p.id} className={styles.card} onClick={() => setProductDialogOpenState(p.id)}>
                  <span>{p.name}</span>
                  <BiSolidCircle className={styles.status} data-enabled={p.enabled} />
                  <Accordion title={`${variantsMap[p.id].length} Variantes`}>
                    <div className={styles.variants}>
                      {variantsMap[p.id].map((v) => (
                        <span key={v.id}>{v.name}</span>
                      ))}
                    </div>
                  </Accordion>
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
