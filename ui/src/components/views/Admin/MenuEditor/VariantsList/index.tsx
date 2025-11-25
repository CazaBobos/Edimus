import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useAdminStore } from "@/stores";
import { Product } from "@/types";
import { useMemo, MouseEvent, useState } from "react";
import { BiChevronDown, BiChevronUp, BiSolidCircle } from "react-icons/bi";

import styles from "./styles.module.scss";

type VariantsListProps = {
  product: Product;
};
export const VariantsList = ({ product }: VariantsListProps) => {
  const { data: products } = useProductsQuery();
  const setProductDialogOpenState = useAdminStore((store) => store.setProductDialogOpenState);

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

  const amount = variantsMap[product.id].length;

  const handleClick = (e: MouseEvent<HTMLLIElement>, variant: Product) => {
    e.stopPropagation();
    setProductDialogOpenState(variant);
  };

  const [open, setOpen] = useState<boolean>(false);
  const toggleOpen = (e: MouseEvent<HTMLElement>) => {
    e.stopPropagation();
    setOpen(!open);
  };

  return (
    <div className={styles.container}>
      <strong onClick={toggleOpen}>
        {amount !== 0 ? amount : "Sin "} Variantes {!!amount && (open ? <BiChevronUp /> : <BiChevronDown />)}
      </strong>
      {!!amount && open && (
        <ul className={styles.variants}>
          {variantsMap[product.id].map((v) => (
            <li key={v.id} onClick={(e) => handleClick(e, v)}>
              {v.name}
              <BiSolidCircle className={styles.status} data-enabled={v.enabled} />
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};
