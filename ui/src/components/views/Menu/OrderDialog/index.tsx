import { useMenuStore } from "@/stores";
import { BiX } from "react-icons/bi";

import { Dialog } from "../../../ui/Dialog";
import styles from "./styles.module.scss";

export const OrderDialog = () => {
  const { isOrderDialogOpen, setIsOrderDialogOpen } = useMenuStore();
  const handleClose = () => setIsOrderDialogOpen(false);

  return (
    <Dialog open={isOrderDialogOpen} onClose={handleClose}>
      <div className={styles.content}>
        <div className={styles.header}>
          <h2>Cuenta</h2>
          <BiX size={32} />
        </div>
        <h3 style={{ paddingTop: 16 }}>Su pedido:</h3>
        <OrderCard product="Café" variant="Flat White" price={2300} owned />
        <OrderCard product="" variant="Roll de canela" price={2800} owned />
        <h3 style={{ paddingTop: 16, color: "#bbb" }}>En mesa:</h3>
        <OrderCard product="Café" variant="Latte" price={2600} />
        <OrderCard product="Café" variant="Cortado" price={2000} />
        <OrderCard product="Sandwich" variant="Croque Madame" price={8500} />
        <OrderCard product="Tostón" variant="de pan de campo" price={4500} />

        <div className={styles.footer}>
          <b>Su Total: ${5100}</b>
          <b style={{ color: "#bbb" }}>Total Mesa: ${17600}</b>
        </div>
      </div>
    </Dialog>
  );
};

type OrderCardProps = {
  product: string;
  variant: string;
  price: number;
  owned?: boolean;
};
const OrderCard = (props: OrderCardProps) => {
  const { product, variant, price, owned } = props;
  return (
    <div className={styles.card} data-owned={owned}>
      <b>
        {product} - {variant}
      </b>
      <b>${price}</b>
    </div>
  );
};
