import { useIngredientMutations } from "@/hooks/mutations/useIngredientMutations";
import { useAdminStore } from "@/stores";
import { IngredientRequest, measurementUnitsMap } from "@/types";
import { Drawer } from "@mantine/core";
import { useState } from "react";
import { BiSave, BiTrash, BiUpload } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { ControlState } from "@/components/ui/common";
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

  const { createIngredientMutation, updateIngredientMutation, removeIngredientMutation, restoreIngredientMutation } =
    useIngredientMutations();

  const handleToggle = () => {
    if (!ingredient) return;
    if (ingredient.enabled) removeIngredientMutation.mutate(ingredient.id, { onSuccess: handleClose });
    else restoreIngredientMutation.mutate(ingredient.id, { onSuccess: handleClose });
  };

  const [request, setRequest] = useState<IngredientRequest>({});
  const handleSetRequest = (state: ControlState) => {
    const { name, value } = state;

    setRequest((prev) => ({ ...prev, [name]: name === "name" ? value : Number(value) }));
  };

  const isRequestValid = () => {
    return ![request.name, request.stock, request.unit].includes(undefined);
  };

  const handleSave = () => {
    if (ingredient) {
      updateIngredientMutation.mutate({ id: ingredient.id, request }, { onSuccess: handleClose });
    } else if (isRequestValid()) {
      createIngredientMutation.mutate(request as Required<IngredientRequest>, { onSuccess: handleClose });
    }
  };

  const title = ingredient ? `Ingrediente: ${ingredient.name}` : "Nuevo Ingrediente";

  return (
    <Drawer
      opened={ingredient !== undefined}
      onClose={handleClose}
      title={title}
      position="right"
      size="md"
      shadow="xl"
    >
      <div className={styles.content}>
        <Input title="Nombre" name="name" defaultValue={ingredient?.name} onChange={handleSetRequest} />
        <Select
          title="Unidad de medida"
          name="unit"
          onChange={handleSetRequest}
          value={(request.unit ?? ingredient?.unit)?.toString()}
          options={Object.entries(measurementUnitsMap).map(([value, label]) => ({ value, label }))}
        />
        <div className={styles.row}>
          <Input
            title="Cantidad en stock"
            name="stock"
            type="number"
            defaultValue={ingredient?.stock}
            onChange={handleSetRequest}
          />
          <Input
            title="Alerta mínima"
            name="alert"
            type="number"
            defaultValue={ingredient?.alert}
            onChange={handleSetRequest}
          />
        </div>
        <Button
          label="Guardar Cambios"
          icon={<BiSave />}
          disabled={!ingredient && !isRequestValid()}
          onClick={handleSave}
        />
        {ingredient && (
          <Button
            label={ingredient.enabled ? "Deshabilitar" : "Restaurar"}
            icon={ingredient.enabled ? <BiTrash /> : <BiUpload />}
            onClick={handleToggle}
          />
        )}
      </div>
    </Drawer>
  );
};
