import { Product, Table, TableOrder } from "@/types";
import { Divider, Modal } from "@mantine/core";
import { BiPrinter } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import styles from "./styles.module.scss";

type OrderControlProps = {
  opened: boolean;
  table: Table;
  orders: TableOrder[];
  productsMap: Map<number, Product>;
  onClose: () => void;
};

export const OrderControl = ({ opened, table, orders, productsMap, onClose }: OrderControlProps) => {
  const rows = orders.map((o) => {
    const p = productsMap.get(o.productId);
    return {
      name: p?.name ?? "?",
      amount: o.amount,
      subtotal: o.amount * (p?.price ?? 0),
    };
  });
  const total = rows.reduce((s, r) => s + r.subtotal, 0);

  const handlePrint = () => {
    const lines = rows.map((r) => `  ${r.amount}x ${r.name} — $${r.subtotal.toFixed(2)}`).join("\n");
    const html = `<!DOCTYPE html><html><head>
      <title>Control de pedido — Mesa #${table.id}</title>
      <style>body{font-family:monospace;padding:24px;max-width:420px}hr{margin:12px 0}pre{margin:0;white-space:pre-wrap}</style>
    </head><body>
      <h2 style="margin:0 0 4px">Control de Pedido</h2>
      <p style="margin:0 0 12px;color:#666">Mesa #${table.id}</p>
      <hr><pre>${lines}</pre><hr>
      <strong>Total: $${total.toFixed(2)}</strong>
    </body></html>`;

    const iframe = document.createElement("iframe");
    iframe.style.display = "none";
    iframe.srcdoc = html;
    iframe.onload = () => {
      iframe.contentWindow!.onafterprint = () => iframe.remove();
      iframe.contentWindow!.print();
    };
    document.body.appendChild(iframe);
  };

  return (
    <Modal opened={opened} onClose={onClose} title={`Control de Pedido — Mesa #${table.id}`} centered>
      <div className={styles.container}>
        {rows.length === 0 ? (
          <p className={styles.empty}>No hay pedidos registrados.</p>
        ) : (
          rows.map((r, i) => (
            <div key={i} className={styles.row}>
              <span className={styles.amount}>{r.amount}x</span>
              <span className={styles.name}>{r.name}</span>
              <span className={styles.subtotal}>${r.subtotal.toFixed(2)}</span>
            </div>
          ))
        )}
        <Divider />
        <div className={styles.total}>
          <span>Total</span>
          <strong>${total.toFixed(2)}</strong>
        </div>
        <Button label="Imprimir" icon={<BiPrinter />} onClick={handlePrint} />
      </div>
    </Modal>
  );
};
