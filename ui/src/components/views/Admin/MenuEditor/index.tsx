import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useAdminStore } from "@/stores";
import { Product } from "@/types";
import { MouseEvent, useMemo, useState } from "react";
import { BiChevronRight, BiPlus, BiSolidCircle } from "react-icons/bi";

import { ProductDialog } from "./ProductDialog";
import styles from "./styles.module.scss";

export const MenuEditor = () => {
  const { setProductDialogOpenState } = useAdminStore();
  const { data: products } = useProductsQuery();
  const { data: categories } = useCategoriesQuery();

  const [selectedCategoryId, setSelectedCategoryId] = useState<number | null>(null);

  const productsByCategory = useMemo(() => {
    const map: Record<number, Product[]> = {};
    categories.forEach((c) => {
      map[c.id] = products.filter((p) => p.categoryId === c.id);
    });
    return map;
  }, [categories, products]);

  const variantsByParent = useMemo(() => {
    const map: Record<number, Product[]> = {};
    products.forEach((p) => {
      if (p.parentId) {
        if (!map[p.parentId]) map[p.parentId] = [];
        map[p.parentId].push(p);
      }
    });
    return map;
  }, [products]);

  const rootProducts = products.filter((p) => !!p.categoryId);

  const selectedCategory = categories.find((c) => c.id === selectedCategoryId) ?? null;
  const visibleProducts = selectedCategoryId !== null ? (productsByCategory[selectedCategoryId] ?? []) : rootProducts;

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h2>Menú</h2>
        <button className={styles.newBtn} onClick={() => setProductDialogOpenState(null)}>
          <BiPlus size={15} />
          Nuevo Producto
        </button>
      </div>

      <div className={styles.body}>
        <aside className={styles.sidebar}>
          <div className={styles.sidebarLabel}>Categorías</div>
          <button
            className={styles.categoryItem}
            data-active={selectedCategoryId === null}
            onClick={() => setSelectedCategoryId(null)}
          >
            <span>Todas</span>
            <span className={styles.categoryCount}>{rootProducts.length}</span>
          </button>
          {categories.map((cat) => (
            <button
              key={cat.id}
              className={styles.categoryItem}
              data-active={selectedCategoryId === cat.id}
              onClick={() => setSelectedCategoryId(cat.id)}
            >
              <span>{cat.name}</span>
              <span className={styles.categoryCount}>{productsByCategory[cat.id]?.length ?? 0}</span>
            </button>
          ))}
        </aside>
        <div className={styles.productList}>
          {selectedCategoryId === null ? (
            categories.map((cat) => (
              <section key={cat.id}>
                <div className={styles.groupHeader}>
                  <span>{cat.name}</span>
                  <span className={styles.groupCount}>{productsByCategory[cat.id]?.length ?? 0} productos</span>
                </div>
                {(productsByCategory[cat.id] ?? []).length > 0 ? (
                  (productsByCategory[cat.id] ?? []).map((product) => (
                    <ProductRow
                      key={product.id}
                      product={product}
                      variants={variantsByParent[product.id] ?? []}
                      onClick={() => setProductDialogOpenState(product)}
                      onVariantClick={(v) => setProductDialogOpenState(v)}
                    />
                  ))
                ) : (
                  <div className={styles.empty}>Sin productos en esta categoría</div>
                )}
              </section>
            ))
          ) : (
            <section className={styles.group}>
              <div className={styles.groupHeader}>
                <span>{selectedCategory?.name}</span>
                <span className={styles.groupCount}>{visibleProducts.length} productos</span>
              </div>
              {visibleProducts.length > 0 ? (
                visibleProducts.map((product) => (
                  <ProductRow
                    key={product.id}
                    product={product}
                    variants={variantsByParent[product.id] ?? []}
                    onClick={() => setProductDialogOpenState(product)}
                    onVariantClick={(v) => setProductDialogOpenState(v)}
                  />
                ))
              ) : (
                <div className={styles.empty}>Sin productos en esta categoría</div>
              )}
            </section>
          )}
        </div>
      </div>

      <ProductDialog />
    </div>
  );
};

type ProductRowProps = {
  product: Product;
  variants: Product[];
  onClick: () => void;
  onVariantClick: (variant: Product) => void;
};

const ProductRow = ({ product, variants, onClick, onVariantClick }: ProductRowProps) => {
  const handleVariantClick = (e: MouseEvent, variant: Product) => {
    e.stopPropagation();
    onVariantClick(variant);
  };

  return (
    <div className={styles.productRow} data-enabled={product.enabled} onClick={onClick}>
      <BiSolidCircle className={styles.statusDot} data-enabled={product.enabled} />
      <div className={styles.productInfo}>
        <span className={styles.productName}>{product.name}</span>
        {product.description && <span className={styles.productDesc}>{product.description}</span>}
        {variants.length > 0 && (
          <div className={styles.variantChips}>
            {variants.map((v) => (
              <span
                key={v.id}
                className={styles.variantChip}
                data-enabled={v.enabled}
                onClick={(e) => handleVariantClick(e, v)}
              >
                {v.name}
              </span>
            ))}
          </div>
        )}
      </div>
      <div className={styles.productMeta}>
        {product.price !== undefined && <span className={styles.price}>${product.price.toFixed(2)}</span>}
        <BiChevronRight className={styles.chevron} size={18} />
      </div>
    </div>
  );
};
