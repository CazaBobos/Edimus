import { useIngredientsQuery } from "@/hooks/queries/useIngredientsQuery";
import { useAdminStore } from "@/stores";
import { BiX } from "react-icons/bi";

import { Dialog } from "@/components/ui/Dialog";

import styles from "./styles.module.scss";

export const IngredientDialog = () => {
  const { ingredientDialogOpenState, setIngredientDialogOpenState } = useAdminStore();

  const handleClose = () => setIngredientDialogOpenState(undefined);

  const { data: ingredients } = useIngredientsQuery();

  const ingredient = ingredients.find((p) => p.id === ingredientDialogOpenState);

  return (
    <Dialog open={ingredientDialogOpenState !== undefined} onClose={handleClose}>
      <h2 className={styles.header}>
        {ingredient ? `Editar Ingrediente #${ingredient.id}` : "Nuevo Ingrediente"}
        <BiX size={28} onClick={handleClose} />
      </h2>
      <div className={styles.content}>
        <span>Nombre</span>
        <input type="text" name="name" value={ingredient?.name} />
        <span>Unidad de medida</span>
        <select name="" id="">
          <option value={ingredient?.unit}>{ingredient?.unit}</option>
        </select>
        <div className={styles.row}>
          <div>
            <span>Cantidad Stock</span>
            <input type="text" name="stock" value={ingredient?.stock} />
          </div>
          <div>
            <span>Cantidad Alerta</span>
            <input type="text" name="alert" value={ingredient?.alert} />
          </div>
        </div>
        <div className={styles.row}>
          <button className={styles.actionButton}>Cancelar</button>
          <button className={styles.actionButton}>Guardar</button>
        </div>
      </div>
    </Dialog>
  );
};
