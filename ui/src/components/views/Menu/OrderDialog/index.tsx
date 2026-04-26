import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useSingleTableQuery } from "@/hooks/queries/useSingleTableQuery";
import { useMenuStore } from "@/stores";
import { Product } from "@/types";
import { BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import { Dialog } from "../../../ui/Dialog";
import styles from "./styles.module.scss";

export const OrderDialog = () => {
  const { isOrderDialogOpen, setIsOrderDialogOpen } = useMenuStore();
  const handleClose = () => setIsOrderDialogOpen(false);

  const { data: table } = useSingleTableQuery();
  const { data: products } = useProductsQuery();

  const productsMap = new Map<number, Product>(products.map((p) => [p.id, p]));

  const total =
    table?.orders.reduce((acc, o) => {
      const p = productsMap.get(o.productId);
      return acc + o.amount * (p?.price ?? 0);
    }, 0) ?? 0;

  return (
    <Dialog open={isOrderDialogOpen} onClose={handleClose}>
      <div className={styles.content}>
        <div className={styles.header}>
          <h2>Cuenta</h2>
          <Button variant="subtle" icon={<BiX size={18} />} onClick={handleClose} />
        </div>
        <div className={styles.orderList}>
          {!table?.orders.length ? (
            <p className={styles.empty}>La mesa no registra pedidos</p>
          ) : (
            table.orders.map((o) => {
              const p = productsMap.get(o.productId);
              if (!p) return null;

              let name = p.name;
              let price = p.price ?? 0;

              if (p.parentId) {
                const parent = productsMap.get(p.parentId);
                if (p.price === 0) price = parent?.price ?? 0;
                name = `${parent?.name} - ${name}`;
              }

              return <OrderCard key={o.productId} product={name} amount={o.amount} price={price} />;
            })
          )}
        </div>
        <div className={styles.footer}>
          <span>Total Mesa</span>
          <b>${total}</b>
        </div>
      </div>
    </Dialog>
  );
};

type OrderCardProps = {
  product: string;
  amount: number;
  price: number;
};
const OrderCard = ({ product, amount, price }: OrderCardProps) => (
  <div className={styles.card}>
    <span className={styles.cardName}>
      <b>{amount}×</b> {product}
    </span>
    <b>${amount * price}</b>
  </div>
);
