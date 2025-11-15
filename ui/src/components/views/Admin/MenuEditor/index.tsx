import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useAdminStore } from "@/stores";
import { useMemo } from "react";
import { BiPlus, BiSolidCircle } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";

import { ProductDialog } from "./ProductDialog";
import styles from "./styles.module.scss";
export const MenuEditor = () => {
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
    <div className={styles.container}>
      <h2>
        Editor de Menú
        <Button label="Nuevo" icon={<BiPlus />} onClick={() => setProductDialogOpenState(0)} />
      </h2>
      <div className={styles.products}>
        {products
          .filter((p) => !!p.categoryId)
          .map((p) => (
            <Card key={p.id} className={styles.card} onClick={() => setProductDialogOpenState(p.id)}>
              <span>{categoryMap[p.categoryId ?? 0]}</span>
              <span>{p.name}</span>
              <BiSolidCircle data-enabled={p.enabled} />
            </Card>
          ))}
      </div>
      <ProductDialog />
    </div>
  );
};
