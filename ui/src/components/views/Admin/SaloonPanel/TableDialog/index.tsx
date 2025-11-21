import { useSaloonMutations } from "@/hooks/mutations/useSaloonMutations";
import { useAdminStore } from "@/stores";
import { Coords, CreateTableRequest, TableStatus, UpdateTableRequest } from "@/types";
import { useState } from "react";
import { BiSave, BiSolidCircle, BiTrash, BiX } from "react-icons/bi";
import { HiDocumentText } from "react-icons/hi";
import QRCode from "react-qr-code";

import { Button } from "@/components/ui/Button";
import { Dialog } from "@/components/ui/Dialog";

import { Positioner } from "../Positioner";
import { SurfaceEditor } from "../SurfaceEditor";
import { RequestsList } from "./RequestsList";
import styles from "./styles.module.scss";

export const TableDialog = () => {
  const table = useAdminStore((store) => store.tableDialogOpenState);
  const setTableDialogOpenState = useAdminStore((store) => store.setTableDialogOpenState);

  const handleClose = () => {
    setRequest({});
    setTableDialogOpenState(undefined);
  };
  const [request, setRequest] = useState<UpdateTableRequest>({});
  //const handleSetState = (status: TableStatus) => {
  //  setRequest((prev) => ({ ...prev, status }));
  //};
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
    else createTableMutation.mutate(request as CreateTableRequest, mutationOptions);
  };

  const handleRemove = () => {
    if (table) removeTableMutation.mutate(table.id, mutationOptions);
  };

  const statusColor = {
    [TableStatus.Free]: "green",
    [TableStatus.Calling]: "orange",
    [TableStatus.Occupied]: "red",
  }[table?.status ?? 0];

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
          <Button label="Guardar Cambios" icon={<BiSave />} onClick={handleSave} />
          <Button label="Emitir Control de Pedido" icon={<HiDocumentText size={24} />} />
          {table && <Button label="Eliminar Mesa" icon={<BiTrash />} onClick={handleRemove} />}
        </div>
        {table && <RequestsList table={table} />}
        {table?.qrId && <QRCode value={`${window.location.hostname}/${table.qrId}`} />}
      </div>
    </Dialog>
  );
};
