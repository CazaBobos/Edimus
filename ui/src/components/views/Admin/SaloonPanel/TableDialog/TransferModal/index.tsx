import { useTableMutations } from "@/hooks/mutations/useTableMutations";
import { Nullable, Table, TableOrder, TableStatus, tableStatusColorMap, tableStatusNameMap } from "@/types";
import { Modal, Radio, Text } from "@mantine/core";
import { useEffect, useState } from "react";
import { BiSolidCircle } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Select } from "@/components/ui/Select";

import styles from "./styles.module.scss";

type TransferMode = "move" | "merge";

type TransferModalProps = {
  opened: boolean;
  tables: Table[];
  sourceTable: Nullable<Table>;
  sourceOrders: TableOrder[];
  onClose: () => void;
  onSuccess: () => void;
};

export const TransferModal = (props: TransferModalProps) => {
  const { opened, tables, sourceTable, sourceOrders, onClose, onSuccess } = props;

  const [targetTableId, setTargetTableId] = useState(-1);
  const [mode, setMode] = useState<TransferMode>("move");

  const { updateTableMutation } = useTableMutations();

  useEffect(() => {
    if (opened) {
      setTargetTableId(-1);
      setMode("move");
    }
  }, [opened]);

  const options = tables
    .filter((t) => t.id !== sourceTable?.id)
    .map((t) => ({
      value: String(t.id),
      label: `Mesa #${t.id} (${tableStatusNameMap[t.status]})`,
      icon: <BiSolidCircle size={12} fill={tableStatusColorMap[t.status]} />,
    }));

  const handleTransfer = () => {
    if (!sourceTable) return;

    const targetTable = tables.find((t) => t.id === targetTableId);
    if (!targetTable) return;

    const newTargetOrders = mode === "merge" ? [...targetTable.orders, ...sourceOrders] : sourceOrders;

    updateTableMutation.mutate(
      { id: targetTableId, request: { orders: newTargetOrders, status: TableStatus.Occupied } },
      {
        onSuccess: () => {
          updateTableMutation.mutate(
            { id: sourceTable.id, request: { orders: [], status: TableStatus.Free } },
            { onSuccess },
          );
        },
      },
    );
  };

  return (
    <Modal opened={opened} onClose={onClose} title="Transferir Pedido" centered>
      <div className={styles.container}>
        <Select options={options} value={String(targetTableId)} onChange={(s) => setTargetTableId(Number(s.value))} />
        <Radio.Group value={mode} onChange={(v) => setMode(v as TransferMode)}>
          <div className={styles.radioGroup}>
            <Radio value="move" label="Mover pedido" description="Se reemplazan los pedidos en la mesa destino" />
            <Radio value="merge" label="Combinar pedidos" description="Se suman los pedidos en la mesa destino" />
          </div>
        </Radio.Group>
        <Text size="xs" c="dimmed">
          Advertencia: La mesa de origen quedará libre y la mesa destino pasará a Ocupada en caso de no estarlo.
        </Text>
        <div className={styles.actions}>
          <Button label="Cancelar" onClick={onClose} />
          <Button
            variant="brand"
            label="Confirmar"
            onClick={handleTransfer}
            disabled={targetTableId === -1 || updateTableMutation.isPending}
          />
        </div>
      </div>
    </Modal>
  );
};
