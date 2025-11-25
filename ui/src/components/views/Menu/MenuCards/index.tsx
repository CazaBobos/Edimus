import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { Product } from "@/types";
import { useMemo, useState } from "react";

import { Select, SelectOption } from "@/components/ui/Select";

import styles from "./styles.module.scss";

export const MenuCards = () => {
  const { data: categories } = useCategoriesQuery();
  const { data: products } = useProductsQuery();

  const [selectedCategory, setSelectedCategory] = useState<number>(1);

  const categoryOptions: SelectOption[] = categories.map((c) => ({
    label: c.name,
    value: c.id,
  }));

  const variantsMap = useMemo(() => {
    const map: Record<number, Product[]> = {};

    products.forEach((p) => {
      if (!p.parentId) return;

      if (!map[p.parentId]) map[p.parentId] = [];

      map[p.parentId] = [...map[p.parentId], p];
    });

    return map;
  }, [products]);

  return (
    <>
      <Select
        options={categoryOptions}
        value={selectedCategory.toString()}
        onChange={({ value }) => setSelectedCategory(Number(value))}
      />
      {products
        .filter((p) => p.categoryId === selectedCategory)
        .map((p) => (
          <div key={p.id} className={styles.card}>
            <div className={styles.title}>
              <h3>{p.name}</h3>
              {!!p.price && <b>${p.price}</b>}
            </div>
            <p>{p.description}</p>
            <ul className={styles.list}>
              {variantsMap[p.id]?.map((v) => (
                <li key={v.name}>
                  <div className={styles.title}>
                    <h5>{v.name}</h5>
                    {!!v.price && <span>${v.price}</span>}
                  </div>
                  <p>{v.description}</p>
                </li>
              ))}
            </ul>
          </div>
        ))}
    </>
  );
};
