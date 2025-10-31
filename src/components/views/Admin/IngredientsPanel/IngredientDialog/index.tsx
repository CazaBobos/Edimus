import { useIngredientMutations } from "@/hooks/mutations/useIngredientMutations";
import { useIngredientsQuery } from "@/hooks/queries/useIngredientsQuery";
import { useAdminStore } from "@/stores";
import { IngredientRequest } from "@/types";
import { useState } from "react";
import { BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { ControlState } from "@/components/ui/common";
import { Dialog } from "@/components/ui/Dialog";
import { Input } from "@/components/ui/Input";
import { Select } from "@/components/ui/Select";

import styles from "./styles.module.scss";

export const IngredientDialog = () => {
  const { ingredientDialogOpenState, setIngredientDialogOpenState } = useAdminStore();

  const handleClose = () => setIngredientDialogOpenState(undefined);

  const { data: ingredients } = useIngredientsQuery();

  const ingredient = ingredients.find((p) => p.id === ingredientDialogOpenState);

  const { createIngredientMutation, updateIngredientMutation } = useIngredientMutations();

  const [request, setRequest] = useState<IngredientRequest>({});
  const handleSetRequest = (state: ControlState) => {
    const { name, value } = state;
    setRequest((prev) => ({ ...prev, [name]: value }));
  };

  const isRequestValid = () => {
    return ![request.name, request.stock, request.stock, request.unit].includes(undefined);
  };

  const handleSave = () => {
    if (!!ingredient) {
      updateIngredientMutation.mutate({ id: ingredient.id, request });
    } else {
      createIngredientMutation.mutate(request as Required<IngredientRequest>);
    }
  };

  return (
    <Dialog open={ingredientDialogOpenState !== undefined} onClose={handleClose}>
      <h2 className={styles.header}>
        {ingredient ? `Editar Ingrediente #${ingredient.id}` : "Nuevo Ingrediente"}
        <BiX size={28} onClick={handleClose} />
      </h2>
      <div className={styles.content}>
        <Input title="Nombre" name="name" value={ingredient?.name} />
        <Select
          title="Unidad de medida"
          name="unit"
          onChange={handleSetRequest}
          value={ingredient?.unit}
          options={["unidad", "kg", "g", "bolsas"]}
        />
        <div className={styles.row}>
          <Input title="Cantidad Stock" name="stock" value={ingredient?.stock} />
          <Input title="Cantidad Alerta" name="alert" value={ingredient?.alert} />
        </div>
        <div className={styles.row}>
          <Button label="Cancelar" />
          <Button label="Guardar" disabled={!ingredient && !isRequestValid()} onClick={handleSave} />
        </div>
      </div>
    </Dialog>
  );
};
