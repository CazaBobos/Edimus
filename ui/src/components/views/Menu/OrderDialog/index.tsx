import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useSingleTableQuery } from "@/hooks/queries/useSingleTableQuery";
import { useMenuStore } from "@/stores";
import { Product } from "@/types";
import { BiX } from "react-icons/bi";

import { Dialog } from "../../../ui/Dialog";
import styles from "./styles.module.scss";

export const OrderDialog = () => {
  const { isOrderDialogOpen, setIsOrderDialogOpen } = useMenuStore();
  const handleClose = () => setIsOrderDialogOpen(false);

  const { data: table } = useSingleTableQuery();
  const { data: products } = useProductsQuery();

  const productsMap = new Map<number, Product>(products.map((p) => [p.id, p]));

  return (
    <Dialog open={isOrderDialogOpen} onClose={handleClose}>
      <div className={styles.content}>
        <div className={styles.header}>
          <h2>Cuenta</h2>
          <BiX size={32} />
        </div>
        {!table?.orders.length && <strong>La mesa no registra pedidos</strong>}
        {table?.orders.map((o) => {
          const p = productsMap.get(o.productId);
          if (!p) return null;

          let name = p.name;
          let price = p.price;

          if (p.parentId) {
            const parent = productsMap.get(p.parentId);

            if (p.price === 0) price = parent!.price;

            name = `${parent!.name} - ` + name;
          }

          return <OrderCard key={name} product={name} price={price ?? 0} owned />;
        })}
        <div className={styles.footer}>
          <b style={{ color: "#bbb" }}>
            Total Mesa: $
            {table?.orders.reduce((acum, curr) => {
              const p = productsMap.get(curr.productId);

              return acum + curr.amount * (p?.price ?? 0);
            }, 0)}
          </b>
        </div>
      </div>
    </Dialog>
  );
};

type OrderCardProps = {
  product: string;
  price: number;
  owned?: boolean;
};
const OrderCard = (props: OrderCardProps) => {
  const { product, price, owned } = props;
  return (
    <div className={styles.card} data-owned={owned}>
      <b>{product}</b>
      <b>${price}</b>
    </div>
  );
};
