import { useTableMutations } from "@/hooks/mutations/useTableMutations";
import { useCompanyQuery } from "@/hooks/queries/useCompanyQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useTablesQuery } from "@/hooks/queries/useTablesQuery";
import { useAdminStore } from "@/stores";
import {
  Company,
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
import { useMemo, useRef, useState } from "react";
import { BiClipboard, BiDownload, BiLock, BiReceipt, BiSave, BiSolidCircle, BiTransfer, BiTrash } from "react-icons/bi";
import QRCode from "react-qr-code";

import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";
import { ControlState } from "@/components/ui/common";
import { Select } from "@/components/ui/Select";
import { Tabs } from "@/components/ui/Tabs";

import { Positioner } from "../Positioner";
import { SurfaceEditor } from "../SurfaceEditor";
import { OrderControl } from "./OrderControl";
import { OrdersList } from "./OrdersList";
import styles from "./styles.module.scss";
import { TransferModal } from "./TransferModal";

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

type TableDialogProps = { layoutId: number | undefined };

export const TableDialog = ({ layoutId }: TableDialogProps) => {
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

  const { data: company } = useCompanyQuery();

  const { data: products } = useProductsQuery();
  const productsMap = useMemo(() => new Map(products.map((p) => [p.id, p])), [products]);

  const { data: tables } = useTablesQuery({ layoutId });
  const [transferOpen, setTransferOpen] = useState(false);
  const [orderControlOpen, setOrderControlOpen] = useState(false);

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
    else createTableMutation.mutate({ layoutId, ...request } as CreateTableRequest, mutationOptions);
  };

  const handleRemove = () => {
    if (table) removeTableMutation.mutate(table.id, mutationOptions);
  };

  const handleOrderControl = () => {
    if (table) setOrderControlOpen(true);
  };

  const tabs = ["Pedidos", "Código QR"];
  const [activeTab, setActiveTab] = useState<number>(0);

  return (
    <>
      <Drawer
        opened={table !== undefined}
        onClose={handleClose}
        title={
          <div className={styles.drawerTitle}>
            <span>{table ? `Mesa #${table.id}` : "Nueva Mesa"}</span>
            {table && (
              <Select
                options={statusOptions}
                value={String(request.status ?? table.status)}
                onChange={handleSetStatus}
              />
            )}
          </div>
        }
        position="right"
        size="md"
        shadow="xl"
      >
        <div className={styles.dialogLayout}>
          <div className={styles.topControls}>
            <div className={styles.row}>
              <Positioner positionX={table?.positionX} positionY={table?.positionY} onChange={handleSetCoords} />
              <SurfaceEditor
                smallButtons
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
                    1: company ? <QRLink company={company} table={table} /> : null,
                  }[activeTab]
                }
              </Card>
            </div>
          )}
          <div className={styles.actions}>
            <Button label="Guardar" icon={<BiSave size={16} />} onClick={handleSave} />
            {table && (
              <>
                <Button danger label="Eliminar" icon={<BiTrash size={16} />} onClick={handleRemove} />
                <Button icon={<BiClipboard />} onClick={handleOrderControl} title="Emitir control de pedido" />
                <Button icon={<BiReceipt />} title="Generar comprobante" />
                <Button icon={<BiTransfer />} onClick={() => setTransferOpen(true)} title="Transferir Pedido" />
              </>
            )}
          </div>
        </div>
      </Drawer>
      {table && (
        <OrderControl
          opened={orderControlOpen}
          table={table}
          orders={request.orders ?? table.orders}
          productsMap={productsMap}
          onClose={() => setOrderControlOpen(false)}
        />
      )}
      <TransferModal
        opened={transferOpen}
        tables={tables}
        sourceTable={table}
        sourceOrders={request.orders ?? table?.orders ?? []}
        onClose={() => setTransferOpen(false)}
        onSuccess={() => {
          setTransferOpen(false);
          handleClose();
        }}
      />
    </>
  );
};

type QRLinkProps = {
  company: Company;
  table: Table;
};
const QRLink = ({ company, table }: QRLinkProps) => {
  const { protocol, hostname, port } = window.location;
  const link = `${protocol}//${hostname}:${port}?slug=${company.slug}&tableId=${table.qrId}`;
  const wrapperRef = useRef<HTMLDivElement>(null);

  const handleDownload = () => {
    const svg = wrapperRef.current?.querySelector("svg");
    if (!svg) return;

    const serialized = new XMLSerializer().serializeToString(svg);
    const blob = new Blob([serialized], { type: "image/svg+xml" });
    const url = URL.createObjectURL(blob);

    const img = new Image();
    img.onload = () => {
      const padding = 12;
      const labelHeight = 36;
      const canvas = document.createElement("canvas");
      canvas.width = img.width + padding * 2;
      canvas.height = img.height + padding * 2 + labelHeight;
      const ctx = canvas.getContext("2d")!;
      ctx.fillStyle = "#ffffff";
      ctx.fillRect(0, 0, canvas.width, canvas.height);
      ctx.drawImage(img, padding, padding);
      ctx.fillStyle = "#111111";
      ctx.font = `bold ${Math.round(canvas.width * 0.075)}px sans-serif`;
      ctx.textAlign = "center";
      ctx.fillText(`Mesa #${table.id}`, canvas.width / 2, img.height + padding * 2 + labelHeight * 0.65);
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
      <span className={styles.qrLabel}>
        Mesa&nbsp;#{table.id}
        <Button label="Descargar" variant="brand" icon={<BiDownload size={16} />} onClick={handleDownload} />
      </span>
      <div ref={wrapperRef}>
        <a target="_blank" href={link}>
          <QRCode value={link} />
        </a>
      </div>
    </>
  );
};
