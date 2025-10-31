import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useState } from "react";

import { Select, SelectOption } from "@/components/ui/Select";

import styles from "./styles.module.scss";

export const MenuCards = () => {
  const { data: categories } = useCategoriesQuery();
  const { data: products } = useProductsQuery();

  const [selectedCategory, setSelectedCategory] = useState<number>();

  const categoryOptions: SelectOption[] = categories.map((c) => ({
    label: c.name,
    value: c.id,
  }));

  return (
    <>
      <Select options={categoryOptions} onChange={({ value }) => setSelectedCategory(Number(value))}></Select>
      {products
        .filter((p) => p.categoryId === (selectedCategory ?? 1))
        .map((p) => (
          <div key={p.id} className={styles.card}>
            <div className={styles.title}>
              <h3>{p.name}</h3>
              {p.price && <b>${p.price}</b>}
            </div>
            <p>{p.description}</p>
            <ul className={styles.list}>
              {p.variants?.map((v) => (
                <li key={v.name}>
                  <div className={styles.title}>
                    <h5>{v.name}</h5>
                    {v.price && <span>${v.price}</span>}
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
