import { useTableMutations } from "@/hooks/mutations/useTableMutations";
import { useAdminStore } from "@/stores";
import {
  Coords,
  CreateTableRequest,
  Table,
  TableOrder,
  TableStatus,
  tableStatusColorMap,
  tableStatusNameMap,
  UpdateTableRequest,
} from "@/types";
import { Drawer } from "@mantine/core";
import { useRef, useState } from "react";
import { BiDownload, BiLock, BiSave, BiSolidCircle, BiTrash } from "react-icons/bi";
import QRCode from "react-qr-code";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";
import { ControlState } from "@/components/ui/common";
import { Select } from "@/components/ui/Select";
import { Tabs } from "@/components/ui/Tabs";

import { Positioner } from "../Positioner";
import { SurfaceEditor } from "../SurfaceEditor";
import { OrdersList } from "./OrdersList";
import styles from "./styles.module.scss";

const statusOptions = [
  {
    value: String(TableStatus.Free),
    label: tableStatusNameMap[TableStatus.Free],
    icon: <BiSolidCircle size={12} fill={tableStatusColorMap[TableStatus.Free]} />,
  },
  {
    value: String(TableStatus.Occupied),
    label: tableStatusNameMap[TableStatus.Occupied],
    icon: <BiSolidCircle size={12} fill={tableStatusColorMap[TableStatus.Occupied]} />,
  },
  {
    value: String(TableStatus.Calling),
    label: tableStatusNameMap[TableStatus.Calling],
    icon: <BiSolidCircle size={12} fill={tableStatusColorMap[TableStatus.Calling]} />,
    disabled: true,
  },
  {
    value: String(TableStatus.Arrived),
    label: tableStatusNameMap[TableStatus.Arrived],
    icon: <BiSolidCircle size={12} fill={tableStatusColorMap[TableStatus.Arrived]} />,
    disabled: true,
  },
];

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

  const effectiveStatus = request.status ?? table?.status;

  const handleSetOrders = (orders: TableOrder[]) => {
    setRequest((prev) => ({ ...prev, orders }));
  };
  const handleSetStatus = (state: ControlState) => {
    if (!!state) setRequest((prev) => ({ ...prev, status: Number(state.value) }));
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

  const tabs = ["Pedidos", "Código QR"];
  const [activeTab, setActiveTab] = useState<number>(0);

  return (
    <Drawer
      opened={table !== undefined}
      onClose={handleClose}
      title={
        <div className={styles.drawerTitle}>
          <span>{table ? `Mesa #${table.id}` : "Nueva Mesa"}</span>
          {table && (
            <Select options={statusOptions} value={String(request.status ?? table.status)} onChange={handleSetStatus} />
          )}
        </div>
      }
      position="right"
      size="xl"
      withOverlay={false}
      shadow="xl"
    >
      <div className={styles.dialogLayout}>
        <div className={styles.topControls}>
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
        </div>
        {table && (
          <div className={styles.tabSection}>
            <Tabs source={tabs} active={activeTab} onChange={setActiveTab} />
            <Card className={styles.card}>
              {
                {
                  0: (
                    <OrdersList
                      table={table}
                      onChange={handleSetOrders}
                      disabled={effectiveStatus === TableStatus.Free}
                    />
                  ),
                  1: <QRLink table={table} />,
                }[activeTab]
              }
            </Card>
          </div>
        )}
        <div className={styles.actions}>
          <Button label="Guardar Cambios" icon={<BiSave />} onClick={handleSave} />
          {table && <Button label="Eliminar Mesa" icon={<BiTrash />} onClick={handleRemove} />}
        </div>
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
