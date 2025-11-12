import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useAdminStore } from "@/stores";
import { useMemo } from "react";

import { Card } from "@/components/ui/Card";

import { ProductDialog } from "./ProductDialog";
import styles from "./styles.module.scss";
export const MenuPanel = () => {
  const { setProductDialogOpenState } = useAdminStore();
  const { data: products } = useProductsQuery();
  const { data: categories } = useCategoriesQuery();

  const categoryMap = useMemo(() => {
    const map: Record<number, string> = {};
    categories.forEach((c) => {
      if (map[c.id]) return;
      map[c.id] = c.name;
    });
    return map;
  }, [categories]);

  return (
    <>
      <Card className={styles.products}>
        {products
          .filter((p) => !p.parentId)
          .map((p) => (
            <Card key={p.id} className={styles.card} onClick={() => setProductDialogOpenState(p.id)}>
              <span>{categoryMap[p.categoryId]}</span>
              <span>{p.name}</span>
            </Card>
          ))}
      </Card>
      <ProductDialog />
    </>
  );
};
