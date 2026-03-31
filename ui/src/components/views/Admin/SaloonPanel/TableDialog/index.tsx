import { useTableMutations } from "@/hooks/mutations/useTableMutations";
import { useAdminStore } from "@/stores";
import {
  Coords,
  CreateTableRequest,
  Table,
  TableOrder,
  TableStatus,
  tableStatusMap,
  UpdateTableRequest,
} from "@/types";
import { Drawer } from "@mantine/core";
import { useRef, useState } from "react";
import { BiDownload, BiLock, BiSave, BiSolidCircle, BiTrash } from "react-icons/bi";
import QRCode from "react-qr-code";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";
import { ControlState } from "@/components/ui/common";
import { Select, SelectOption } from "@/components/ui/Select";
import { Tabs } from "@/components/ui/Tabs";

import { Positioner } from "../Positioner";
import { SurfaceEditor } from "../SurfaceEditor";
import { OrdersList } from "./OrdersList";
import styles from "./styles.module.scss";

export const TableDialog = () => {
  const table = useAdminStore((store) => store.tableDialogOpenState);
  const setTableDialogOpenState = useAdminStore((store) => store.setTableDialogOpenState);
  const setPreviewPosition = useAdminStore((store) => store.setPreviewPosition);

  const handleClose = () => {
    setRequest({});
    setTableDialogOpenState(undefined);
    setPreviewPosition(null);
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
    if (table) setPreviewPosition({ tableId: table.id, x: coords.x, y: coords.y });
  };
  const handleSetSurface = (surface: Coords[]) => {
    setRequest((prev) => ({ ...prev, surface }));
  };

  const { createTableMutation, updateTableMutation, removeTableMutation } = useTableMutations();

  const mutationOptions = { onSuccess: handleClose };

  const handleSave = () => {
    if (table) updateTableMutation.mutate({ id: table.id, request }, mutationOptions);
    else createTableMutation.mutate({ layoutId: 1, ...request } as CreateTableRequest, mutationOptions);
  };

  const handleRemove = () => {
    if (table) removeTableMutation.mutate(table.id, mutationOptions);
  };

  const tableOptions: SelectOption[] = [
    { label: tableStatusMap[TableStatus.Free], value: TableStatus.Free },
    { label: tableStatusMap[TableStatus.Calling], value: TableStatus.Calling, hidden: true },
    { label: tableStatusMap[TableStatus.Occupied], value: TableStatus.Occupied },
  ];

  const statusColor = {
    [TableStatus.Free]: "green",
    [TableStatus.Calling]: "orange",
    [TableStatus.Occupied]: "red",
  }[table?.status ?? 0];

  const tabs = ["Pedidos", "Código QR"];
  const [activeTab, setActiveTab] = useState<number>(0);

  return (
    <Drawer
      opened={table !== undefined}
      onClose={handleClose}
      title={
        <div className={styles.drawerTitle}>
          <BiSolidCircle size={16} fill={statusColor} />
          <span>{table ? `Mesa #${table.id}` : "Nueva Mesa"}</span>
        </div>
      }
      position="right"
      size="xl"
      withOverlay={false}
      shadow="xl"
    >
      <div className={styles.row}>
        <div className={styles.content}>
          <div className={styles.row}>
            <Positioner positionX={table?.positionX} positionY={table?.positionY} onChange={handleSetCoords} />
            <SurfaceEditor
              content={<BiLock size={14} />}
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
                  1: <QRLink table={table} />,
                }[activeTab]
              }
            </Card>
          </div>
        )}
      </div>
    </Drawer>
  );
};

type QRLinkProps = { table: Table };
const QRLink = ({ table }: QRLinkProps) => {
  const { protocol, hostname, port } = window.location;
  const link = `${protocol}//${hostname}:${port}?tableId=${table.qrId}`;
  const wrapperRef = useRef<HTMLDivElement>(null);

  const handleDownload = () => {
    const svg = wrapperRef.current?.querySelector("svg");
    if (!svg) return;

    const serialized = new XMLSerializer().serializeToString(svg);
    const blob = new Blob([serialized], { type: "image/svg+xml" });
    const url = URL.createObjectURL(blob);

    const img = new Image();
    img.onload = () => {
      const canvas = document.createElement("canvas");
      canvas.width = img.width;
      canvas.height = img.height;
      canvas.getContext("2d")!.drawImage(img, 0, 0);
      URL.revokeObjectURL(url);

      const a = document.createElement("a");
      a.download = `mesa-${table.id}.png`;
      a.href = canvas.toDataURL("image/png");
      a.click();
    };
    img.src = url;
  };

  return (
    <>
      <div ref={wrapperRef}>
        <a target="_blank" href={link}>
          <QRCode value={link} />
        </a>
      </div>
      <Button label="Descargar" icon={<BiDownload />} onClick={handleDownload} />
    </>
  );
};
