import { useSaloonMutations } from "@/hooks/mutations/useSaloonMutations";
import { useAdminStore } from "@/stores";
import { Coords, TableStatus, UpdateTableRequest } from "@/types";
import { useState } from "react";
import { BiQr, BiSave, BiSolidCircle, BiTrash, BiX } from "react-icons/bi";
import { HiDocumentText } from "react-icons/hi";
import { HiDocumentCurrencyDollar } from "react-icons/hi2";
import QRCode from "react-qr-code";

import { Button } from "@/components/ui/Button";
import { ControlState } from "@/components/ui/common";
import { Dialog } from "@/components/ui/Dialog";

import { Positioner } from "../Positioner";
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
  const handleSetRequest = (state: ControlState) => {
    const { name, value } = state;

    setRequest((prev) => ({ ...prev, [name]: value }));
  };
  const handleSetCoords = (coords: Coords) => {
    setRequest((prev) => ({ ...prev, positionX: coords.x, positionY: coords.y }));
  };
  const { updateTableMutation } = useSaloonMutations();
  const handleSave = () => {
    if (table) updateTableMutation.mutate({ id: table.id, request });
  };

  const statusColor = {
    [TableStatus.Free]: "green",
    [TableStatus.Calling]: "orange",
    [TableStatus.Occupied]: "red",
  }[table?.status ?? 0];

  const [isQROpen, setIsQROpen] = useState<boolean>(false);

  return (
    <Dialog open={table !== undefined} onClose={handleClose}>
      <div className={styles.header}>
        <h2>
          <span>Mesa: {table?.id}</span>
          <BiSolidCircle size={28} fill={statusColor} />
          <BiX onClick={handleClose} />
        </h2>
      </div>
      <div className={styles.content}>
        <div className={styles.buttons}>
          <Button label="Mostrar QR" icon={<BiQr />} onClick={() => setIsQROpen(true)} />
          <Button label="Eliminar Mesa" icon={<BiTrash />} />
          <Button label="Emitir Control de Pedido" icon={<HiDocumentText size={24} />} />
          <Button label="Emitir Comprobante" icon={<HiDocumentCurrencyDollar size={24} />} />
        </div>
        <Positioner
          positionX={request.positionX ?? table?.positionX}
          positionY={request.positionY ?? table?.positionY}
          onChange={handleSetCoords}
        />
        <Button label="Guardar Cambios" icon={<BiSave />} onClick={handleSave} />
      </div>
      <RequestsList table={table} />
      <Dialog open={isQROpen} onClose={() => setIsQROpen(false)}>
        <BiX onClick={handleClose} />
        {table?.qrId && <QRCode value={`${window.location.hostname}/${table.qrId}`} />}
      </Dialog>
    </Dialog>
  );
};
