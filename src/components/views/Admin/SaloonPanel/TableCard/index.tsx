import { useAdminStore } from "@/stores";
import { BiArrowBack, BiMove, BiPlus, BiQr, BiSolidCircle, BiTrash, BiX } from "react-icons/bi";
import { HiDocumentText } from "react-icons/hi";
import { HiDocumentCurrencyDollar } from "react-icons/hi2";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";

import styles from "./styles.module.scss";

export const TableCard = () => {
  const { setTableDialogOpenState } = useAdminStore();

  const order = [
    {
      name: "Café - Flat White",
      price: 2300,
    },
    {
      name: "Roll de canela",
      price: 2800,
    },
    { name: "Café - Latte", price: 2600 },
    { name: "Café - Cortado", price: 2000 },
    { name: "Sandwich - Croque Madame", price: 8500 },
    { name: "Tostón - de pan de campo", price: 4500 },
  ];

  return (
    <Card className={styles.saloonPanel}>
      <div className={styles.header}>
        <h2>
          <BiArrowBack size={28} onClick={() => setTableDialogOpenState(0)} />
          Mesa: 1 <BiSolidCircle size={32} fill="red" />
        </h2>
        <div>
          <Button icon={<BiQr size={28} />} />
          <Button icon={<BiMove size={28} />} />
          <Button icon={<BiTrash size={28} />} />
        </div>
      </div>
      <div className={styles.content}>
        <div className={styles.order}>
          <div className={styles.row}>
            <h3>Pedidos:&nbsp;</h3>
            <Button label="Añadir" icon={<BiPlus size={24} />} />
          </div>
          <ul>
            {order.map((o) => (
              <li key={o.name}>
                {o.name}
                <div className={styles.row}>
                  <b>${o.price}</b>
                  <BiX size={32} color="red" />
                </div>
              </li>
            ))}
          </ul>
        </div>
        <b>Total de la Mesa: ${order.reduce((acum, curr) => acum + curr.price, 0)}</b>
        <Button label="Emitir Control de Pedido" icon={<HiDocumentText size={24} />} />
        <Button label="Emitir Comprobante" icon={<HiDocumentCurrencyDollar size={24} />} />
      </div>
    </Card>
  );
};
