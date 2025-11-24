import { useSaloonMutations } from "@/hooks/mutations/useSaloonMutations";
import { useAdminStore } from "@/stores";
import { Coords, CreateTableRequest, TableOrder, TableStatus, tableStatusMap, UpdateTableRequest } from "@/types";
import { useState } from "react";
import { BiSave, BiSolidCircle, BiTrash, BiX } from "react-icons/bi";
import QRCode from "react-qr-code";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";
import { ControlState } from "@/components/ui/common";
import { Dialog } from "@/components/ui/Dialog";
import { Select, SelectOption } from "@/components/ui/Select";
import { Tabs } from "@/components/ui/Tabs";

import { Positioner } from "../Positioner";
import { SurfaceEditor } from "../SurfaceEditor";
import { OrdersList } from "./OrdersList";
import styles from "./styles.module.scss";

export const TableDialog = () => {
  const table = useAdminStore((store) => store.tableDialogOpenState);
  const setTableDialogOpenState = useAdminStore((store) => store.setTableDialogOpenState);

  const handleClose = () => {
    setRequest({});
    setTableDialogOpenState(undefined);
  };
  const [request, setRequest] = useState<UpdateTableRequest>({});

  const handleSetOrders = (orders: TableOrder[]) => {
    setRequest((prev) => ({ ...prev, orders }));
  };
  const handleSetStatus = (state: ControlState) => {
    setRequest((prev) => ({ ...prev, status: Number(state.value) }));
  };
  const handleSetCoords = (coords: Coords) => {
    setRequest((prev) => ({ ...prev, positionX: coords.x, positionY: coords.y }));
  };
  const handleSetSurface = (surface: Coords[]) => {
    setRequest((prev) => ({ ...prev, surface }));
  };

  const { createTableMutation, updateTableMutation, removeTableMutation } = useSaloonMutations();

  const mutationOptions = { onSuccess: handleClose };

  const handleSave = () => {
    if (table) updateTableMutation.mutate({ id: table.id, request }, mutationOptions);
    else createTableMutation.mutate({ layoutId: 1, ...request } as CreateTableRequest, mutationOptions);
  };

  const handleRemove = () => {
    if (table) removeTableMutation.mutate(table.id, mutationOptions);
  };

  const tableOptions: SelectOption[] = [
    {
      label: tableStatusMap[TableStatus.Free],
      value: TableStatus.Free,
    },
    {
      label: tableStatusMap[TableStatus.Calling],
      value: TableStatus.Calling,
      hidden: true,
    },
    {
      label: tableStatusMap[TableStatus.Occupied],
      value: TableStatus.Occupied,
    },
  ];

  const statusColor = {
    [TableStatus.Free]: "green",
    [TableStatus.Calling]: "orange",
    [TableStatus.Occupied]: "red",
  }[table?.status ?? 0];

  const tabs = ["Pedidos", "Código QR"];
  const [activeTab, setActiveTab] = useState<number>(0);

  return (
    <Dialog open={table !== undefined} onClose={handleClose}>
      <div className={styles.header}>
        <h2>
          <div>
            <BiSolidCircle size={28} fill={statusColor} />
            <span>{table ? `Mesa #${table.id}"` : "Nueva Mesa"}</span>
          </div>
          <BiX onClick={handleClose} />
        </h2>
      </div>
      <div className={styles.row}>
        <div className={styles.content}>
          <div className={styles.row}>
            <Positioner positionX={table?.positionX} positionY={table?.positionY} onChange={handleSetCoords} />
            <SurfaceEditor
              content={table?.id}
              offset={{ x: -1, y: -1 }}
              height={3}
              width={3}
              defaultValue={table?.surface}
              onChange={handleSetSurface}
            />
          </div>
          <Select
            title="Estado"
            options={tableOptions}
            defaultValue={table?.status.toString()}
            onChange={handleSetStatus}
          />
          <Button label="Guardar Cambios" icon={<BiSave />} onClick={handleSave} />
          {table && <Button label="Eliminar Mesa" icon={<BiTrash />} onClick={handleRemove} />}
        </div>
        {table && (
          <div>
            <Tabs source={tabs} active={activeTab} onChange={setActiveTab} />
            <Card className={styles.card}>
              {
                {
                  0: <OrdersList table={table} onChange={handleSetOrders} />,
                  1: <QRCode value={`${window.location.hostname}/${table.qrId}`} />,
                }[activeTab]
              }
            </Card>
          </div>
        )}
      </div>
    </Dialog>
  );
};
