import { useIngredientsQuery } from "@/hooks/queries/useIngredientsQuery";

import Table from "@/components/ui/Table";

import { useIngredientsColumnDefs } from "./columnDefs";
import { IngredientDialog } from "./IngredientDialog";

export const IngredientsPanel = () => {
  const { data: ingredients } = useIngredientsQuery();
  const columnDefs = useIngredientsColumnDefs();

  return (
    <>
      <Table data={ingredients} columns={columnDefs} />
      <IngredientDialog />
    </>
  );
};
