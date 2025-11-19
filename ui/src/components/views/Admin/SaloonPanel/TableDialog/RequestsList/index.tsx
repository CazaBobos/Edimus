import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { Nullable, Product, Table } from "@/types";
import { BiPlus, BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import styles from "./styles.module.scss";

type RequestsListProps = {
  table: Nullable<Table>;
};
export const RequestsList = (props: RequestsListProps) => {
  const { table } = props;
  const { data: products } = useProductsQuery();

  const productsMap = new Map<number, Product>(products.map((p) => [p.id, p]));
  const requests = table?.requests.map((r) => {
    const product = productsMap.get(r.productId);
    return {
      name: product?.name ?? "",
      price: product?.price ?? 0,
      amount: r.amount,
    };
  });

  return (
    <div className={styles.order}>
      <div className={styles.row}>
        <h3>Pedidos:&nbsp;</h3>
        <Button label="Añadir" icon={<BiPlus />} />
      </div>
      <ul>
        {requests?.map((o) => (
          <li key={o.name}>
            {o.name}
            <div className={styles.row}>
              <b>${o.price}</b>
              <BiX size={32} color="red" />
            </div>
          </li>
        ))}
      </ul>
      <b>Total de la Mesa: ${requests?.reduce((acum, curr) => acum + curr.price, 0)}</b>
    </div>
  );
};
