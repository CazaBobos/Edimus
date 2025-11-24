import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { Product, Table, TableOrder } from "@/types";
import { useState } from "react";
import { BiPlus, BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Input } from "@/components/ui/Input";
import { Select, SelectOption } from "@/components/ui/Select";

import styles from "./styles.module.scss";

type OrdersListProps = {
  table: Table;
  onChange: (orders: TableOrder[]) => void;
};
export const OrdersList = (props: OrdersListProps) => {
  const { table, onChange } = props;
  const { data: products } = useProductsQuery();

  const productsMap = new Map<number, Product>(products.map((p) => [p.id, p]));

  const [orders, setOrders] = useState<TableOrder[]>(table?.orders);

  const handleAddOrder = (order: TableOrder) => {
    const newOrders = [order, ...orders];
    setSelectedProduct(-1);
    setCurrentAmount(1);

    setOrders(newOrders);
    onChange(newOrders);
  };

  const handleRemoveOrder = (productId: number) => {
    const newOrders = orders.filter((p) => p.productId !== productId);
    setSelectedProduct(-1);
    setCurrentAmount(1);

    setOrders(newOrders);
    onChange(newOrders);
  };

  const productOptions: SelectOption[] = products
    .filter((p) => p.price !== 0 || p.parentId)
    .map((p) => {
      let name = p.name;
      let price = p.price;

      if (p.parentId) {
        const parent = productsMap.get(p.parentId);

        if (p.price === 0) price = parent!.price;

        name = `${parent!.name} - ` + name;
      }

      return {
        value: p.id,
        label: `${name} - $${price}`,
        hidden: orders.some((r) => r.productId === p.id),
      };
    })
    .sort((a, b) => a.label.localeCompare(b.label));

  const [selectedProduct, setSelectedProduct] = useState<number>(-1);
  const [currentAmount, setCurrentAmount] = useState<number>(1);

  return (
    <div className={styles.order}>
      <ul>
        <li>
          <Select
            options={productOptions}
            value={selectedProduct.toString()}
            onChange={(s) => setSelectedProduct(Number(s.value))}
          />
          <Input
            type="number"
            placeholder="Cant."
            value={currentAmount}
            onChange={(s) => setCurrentAmount(Number(s.value))}
          />
          <Button
            icon={<BiPlus />}
            disabled={selectedProduct === -1}
            onClick={() =>
              handleAddOrder({
                productId: selectedProduct,
                amount: currentAmount,
              })
            }
          />
        </li>
        {!orders?.length && <strong>La mesa no registra pedidos</strong>}
        {orders.map((o) => {
          const product = productsMap.get(o.productId);

          return (
            <li key={product?.name}>
              <div className={styles.row}>
                {o.amount}x {product?.name}
              </div>
              <div className={styles.row}>
                <b>${o.amount * (product?.price ?? 0)}</b>
                <BiX onClick={() => handleRemoveOrder(o.productId)} />
              </div>
            </li>
          );
        })}
      </ul>
      <b>
        Total de la Mesa: $
        {orders?.reduce((acum, curr) => {
          const p = productsMap.get(curr.productId);

          return acum + curr.amount * (p?.price ?? 0);
        }, 0)}
      </b>
    </div>
  );
};
