import { useIngredientsQuery } from "@/hooks/queries/useIngredientsQuery";
import { useAdminStore } from "@/stores";
import { BiPlus } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import Table from "@/components/ui/Table";

import { useIngredientsColumnDefs } from "./columnDefs";
import { IngredientDialog } from "./IngredientDialog";
import styles from "./styles.module.scss";

export const IngredientsManager = () => {
  const { data: ingredients } = useIngredientsQuery();
  const columnDefs = useIngredientsColumnDefs();

  const setIngredientDialogOpenState = useAdminStore((store) => store.setIngredientDialogOpenState);

  return (
    <div className={styles.container}>
      <h2>
        Gestión de Ingredientes
        <Button label="Nuevo" icon={<BiPlus />} onClick={() => setIngredientDialogOpenState(null)} />
      </h2>
      <Table data={ingredients} columns={columnDefs} />
      <IngredientDialog />
    </div>
  );
};
