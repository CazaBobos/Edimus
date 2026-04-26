import { useIngredientsQuery } from "@/hooks/queries/useIngredientsQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { Product, Table, TableOrder } from "@/types";
import { useMemo, useState } from "react";
import { BiPlus, BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Input } from "@/components/ui/Input";
import { Select, SelectOption } from "@/components/ui/Select";

import styles from "./styles.module.scss";

type OrdersListProps = {
  table: Table;
  disabled?: boolean;
  onChange: (orders: TableOrder[]) => void;
};
export const OrdersList = (props: OrdersListProps) => {
  const { table, disabled = false, onChange } = props;
  const { data: products } = useProductsQuery();
  const { ingredientsMap } = useIngredientsQuery();

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

  const productsMap = useMemo(() => new Map<number, Product>(products.map((p) => [p.id, p])), [products]);
  const originalProductIds = useMemo(() => new Set(table.orders.map((o) => o.productId)), [table.orders]);

  const projectedStock = useMemo(() => {
    const reduction = new Map<number, number>();

    for (const order of orders) {
      if (originalProductIds.has(order.productId)) continue;
      const product = productsMap.get(order.productId);
      if (!product) continue;
      for (const c of product.consumptions) {
        reduction.set(c.ingredientId, (reduction.get(c.ingredientId) ?? 0) + order.amount * c.amount);
      }
    }

    const stock = new Map<number, number>();
    ingredientsMap?.forEach((ing) => {
      stock.set(ing.id, ing.stock - (reduction.get(ing.id) ?? 0));
    });
    return stock;
  }, [orders, originalProductIds, productsMap, ingredientsMap]);

  const checkStock = (productId: number): string | null => {
    const product = productsMap.get(productId);
    if (!product) return null;
    for (const c of product.consumptions) {
      const available = projectedStock.get(c.ingredientId) ?? 0;
      if (available < c.amount) {
        return ingredientsMap?.get(c.ingredientId)?.name ?? "ingrediente";
      }
    }
    return null;
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

      const lacking = checkStock(p.id);

      return {
        value: p.id,
        label: lacking ? `${name} - $${price} (sin stock: ${lacking})` : `${name} - $${price}`,
        hidden: orders.some((r) => r.productId === p.id),
        disabled: !!lacking,
      };
    })
    .sort((a, b) => a.label.localeCompare(b.label));

  const [selectedProduct, setSelectedProduct] = useState<number>(-1);
  const [currentAmount, setCurrentAmount] = useState<number>(1);

  return (
    <div className={styles.order}>
      {disabled ? (
        <strong className={styles.disabledNotice}>La mesa debe estar ocupada para registrar pedidos</strong>
      ) : (
        <>
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
            {orders.map((o, i) => {
              const product = productsMap.get(o.productId);

              return (
                <li key={`order_${i}`}>
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
        </>
      )}
    </div>
  );
};
