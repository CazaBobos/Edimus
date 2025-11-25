import { useSectorMutations } from "@/hooks/mutations/useSectorMutations";
import { useAdminStore } from "@/stores";
import { Coords, CreateSectorRequest, UpdateSectorRequest } from "@/types";
import { useState } from "react";
import { BiSave, BiTrash, BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { ColorPicker } from "@/components/ui/ColorPicker";
import { ControlState } from "@/components/ui/common";
import { Dialog } from "@/components/ui/Dialog";
import { Input } from "@/components/ui/Input";

import { Positioner } from "../Positioner";
import { SurfaceEditor } from "../SurfaceEditor";
import styles from "./styles.module.scss";

export const SectorDialog = () => {
  const sector = useAdminStore((store) => store.sectorDialogOpenState);
  const setSectorDialogOpenState = useAdminStore((store) => store.setSectorDialogOpenState);

  const handleClose = () => {
    setRequest({});
    setSectorDialogOpenState(undefined);
  };

  const [request, setRequest] = useState<UpdateSectorRequest>({});
  const handleSetRequest = (state: ControlState) => {
    const { name, value } = state;

    setRequest((prev) => ({ ...prev, [name]: value }));
  };
  const handleSetCoords = (coords: Coords) => {
    setRequest((prev) => ({ ...prev, positionX: coords.x, positionY: coords.y }));
  };
  const handleSetSurface = (surface: Coords[]) => {
    setRequest((prev) => ({ ...prev, surface }));
  };

  const { createSectorMutation, updateSectorMutation, removeSectorMutation } = useSectorMutations();

  const mutationOptions = { onSuccess: handleClose };

  const handleSave = () => {
    if (sector) updateSectorMutation.mutate({ id: sector.id, request }, mutationOptions);
    else createSectorMutation.mutate({ layoutId: 1, ...request } as CreateSectorRequest, mutationOptions);
  };
  const handleRemove = () => {
    if (sector) removeSectorMutation.mutate(sector.id, mutationOptions);
  };

  return (
    <Dialog open={sector !== undefined} onClose={handleClose}>
      <div className={styles.header}>
        <h2>
          <span>{sector ? `Sector "${sector?.name}"` : "Nuevo Sector"}</span>
          <BiX onClick={handleClose} />
        </h2>
      </div>
      <div className={styles.content}>
        <div className={styles.column}>
          <Input title="Nombre" name="name" defaultValue={sector?.name} onChange={handleSetRequest} />
          <ColorPicker name="color" defaultValue={sector?.color} onChange={handleSetRequest} />
          <Positioner positionX={sector?.positionX} positionY={sector?.positionY} onChange={handleSetCoords} />
          <Button label="Guardar Cambios" icon={<BiSave />} onClick={handleSave} />
          {sector && <Button label="Eliminar" icon={<BiTrash />} onClick={handleRemove} />}
        </div>
        <SurfaceEditor content="X" height={15} width={15} defaultValue={sector?.surface} onChange={handleSetSurface} />
      </div>
    </Dialog>
  );
};
