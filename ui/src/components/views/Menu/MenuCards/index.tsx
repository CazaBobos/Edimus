"use client";

import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { Product } from "@/types";
import { useMemo, useState } from "react";

import styles from "./styles.module.scss";

export const MenuCards = () => {
  const { data: categories } = useCategoriesQuery();
  const { data: products } = useProductsQuery();
  const [selectedCategory, setSelectedCategory] = useState<number | null>(null);

  const activeCategory = selectedCategory ?? categories[0]?.id ?? null;

  const variantsMap = useMemo(() => {
    const map: Record<number, Product[]> = {};
    products.forEach((p) => {
      if (!p.parentId) return;
      if (!map[p.parentId]) map[p.parentId] = [];
      map[p.parentId] = [...map[p.parentId], p];
    });
    return map;
  }, [products]);

  const filteredProducts = products.filter((p) => !p.parentId && p.categoryId === activeCategory);

  return (
    <div className={styles.container}>
      <div className={styles.categoryStrip}>
        {categories.map((c) => (
          <button
            key={c.id}
            className={`${styles.pill} ${activeCategory === c.id ? styles.pillActive : ""}`}
            onClick={() => setSelectedCategory(c.id)}
          >
            {c.name}
          </button>
        ))}
      </div>

      <div className={styles.productList}>
        {filteredProducts.length === 0 && <p className={styles.empty}>No hay productos en esta categoría</p>}
        {filteredProducts.map((p, i) => (
          <div key={p.id} className={styles.product} style={{ "--index": i } as React.CSSProperties}>
            <div className={styles.productMain}>
              <div className={styles.productInfo}>
                <span className={styles.productName}>{p.name}</span>
                {p.description && <span className={styles.productDesc}>{p.description}</span>}
              </div>
              {!!p.price && <span className={styles.price}>${p.price}</span>}
            </div>

            {!!variantsMap[p.id]?.length && (
              <ul className={styles.variants}>
                {variantsMap[p.id].map((v) => (
                  <li key={v.id} className={styles.variant}>
                    <div className={styles.variantInfo}>
                      <span className={styles.variantName}>{v.name}</span>
                      {v.description && <span className={styles.productDesc}>{v.description}</span>}
                    </div>
                    {!!v.price && <span className={styles.variantPrice}>${v.price}</span>}
                  </li>
                ))}
              </ul>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};
