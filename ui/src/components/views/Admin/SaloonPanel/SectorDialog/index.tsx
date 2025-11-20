import { useSaloonMutations } from "@/hooks/mutations/useSaloonMutations";
import { useAdminStore } from "@/stores";
import { Coords, CreateSectorRequest, UpdateSectorRequest } from "@/types";
import { useState } from "react";
import { BiSave, BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { ColorPicker } from "@/components/ui/ColorPicker";
import { ControlState } from "@/components/ui/common";
import { Dialog } from "@/components/ui/Dialog";

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

  const { createSectorMutation, updateSectorMutation } = useSaloonMutations();
  const handleSave = () => {
    if (sector) updateSectorMutation.mutate({ id: sector.id, request });
    else createSectorMutation.mutate(request as CreateSectorRequest);
  };

  return (
    <Dialog open={sector !== undefined} onClose={handleClose}>
      <div className={styles.header}>
        <h2>
          <span>Sector &quot;{sector?.name}&quot;</span>
          <BiX onClick={handleClose} />
        </h2>
      </div>
      <div className={styles.content}>
        <div>
          <ColorPicker name="color" onChange={handleSetRequest} defaultValue={sector?.color} />
          <Positioner positionX={sector?.positionX} positionY={sector?.positionY} onChange={handleSetCoords} />
          <Button label="Guardar Cambios" icon={<BiSave />} onClick={handleSave} />
        </div>
        <SurfaceEditor content="X" height={15} width={15} defaultValue={sector?.surface} onChange={handleSetSurface} />
      </div>
    </Dialog>
  );
};
