import { useIngredientMutations } from "@/hooks/mutations/useIngredientMutations";
import { useAdminStore } from "@/stores";
import { IngredientRequest, measurementUnitsMap } from "@/types";
import { useState } from "react";
import { BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { ControlState } from "@/components/ui/common";
import { Dialog } from "@/components/ui/Dialog";
import { Input } from "@/components/ui/Input";
import { Select } from "@/components/ui/Select";

import styles from "./styles.module.scss";

export const IngredientDialog = () => {
  const ingredient = useAdminStore((store) => store.ingredientDialogOpenState);
  const setIngredientDialogOpenState = useAdminStore((store) => store.setIngredientDialogOpenState);

  const handleClose = () => {
    setRequest({});
    setIngredientDialogOpenState(undefined);
  };

  const { createIngredientMutation, updateIngredientMutation } = useIngredientMutations();

  const [request, setRequest] = useState<IngredientRequest>({});
  const handleSetRequest = (state: ControlState) => {
    const { name, value } = state;

    setRequest((prev) => ({ ...prev, [name]: name === "name" ? value : Number(value) }));
  };

  const isRequestValid = () => {
    return ![request.name, request.stock, request.stock, request.unit].includes(undefined);
  };

  const handleSave = () => {
    if (!!ingredient) {
      updateIngredientMutation.mutate(
        { id: ingredient.id, request },
        {
          onSuccess: handleClose,
        },
      );
    } else if (isRequestValid()) {
      createIngredientMutation.mutate(request as Required<IngredientRequest>, {
        onSuccess: handleClose,
      });
    }
  };

  return (
    <Dialog open={ingredient !== undefined} onClose={handleClose}>
      <h2 className={styles.header}>
        {ingredient ? `Editar Ingrediente #${ingredient.id}` : "Nuevo Ingrediente"}
        <BiX size={28} onClick={handleClose} />
      </h2>
      <div className={styles.content}>
        <Input title="Nombre" name="name" defaultValue={ingredient?.name} onChange={handleSetRequest} />
        <div className={styles.row}>
          <Select
            title="Unidad"
            name="unit"
            onChange={handleSetRequest}
            defaultValue={ingredient?.unit.toString()}
            options={Object.entries(measurementUnitsMap).map(([value, label]) => ({ value, label }))}
          />
          <Input title="Cant. Stock" name="stock" defaultValue={ingredient?.stock} onChange={handleSetRequest} />
          <Input title="Cant. Alerta" name="alert" defaultValue={ingredient?.alert} onChange={handleSetRequest} />
        </div>
        <div className={styles.row}>
          <Button label="Cancelar" onClick={handleClose} />
          <Button label="Guardar" disabled={!ingredient && !isRequestValid()} onClick={handleSave} />
        </div>
      </div>
    </Dialog>
  );
};
