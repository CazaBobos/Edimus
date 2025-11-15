import { useIngredientMutations } from "@/hooks/mutations/useIngredientMutations";
import { useAdminStore } from "@/stores";
import { Ingredient, measurementUnitsMap } from "@/types";
import { ColumnDef } from "@tanstack/react-table";
import { BiPencil, BiSolidCircle, BiTrash, BiUpload } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

export const useIngredientsColumnDefs = (): ColumnDef<Ingredient>[] => {
  const { setIngredientDialogOpenState } = useAdminStore();

  const { removeIngredientMutation, restoreIngredientMutation } = useIngredientMutations();

  const handleUpdate = (ingredient: Ingredient) => {
    const { id, enabled } = ingredient;

    if (enabled) removeIngredientMutation.mutate(id);
    else restoreIngredientMutation.mutate(id);
  };

  return [
    {
      header: "ID",
      accessorKey: "id",
    },
    {
      header: "Nombre",
      accessorKey: "name",
    },
    {
      header: "Cantidad",
      accessorKey: "stock",
    },
    {
      header: "Unidad",
      accessorKey: "unit",
      accessorFn: (row) => {
        const unit = row.unit;

        return measurementUnitsMap[unit];
      },
    },
    {
      header: "Alerta",
      accessorKey: "alert",
    },
    {
      header: "Observaciones",
      accessorFn: (row) => row,
      cell: (props) => {
        const ing = props.getValue<Ingredient>();

        return <span style={{ color: "red" }}>{ing.stock < ing.alert ? "Stock bajo!" : ""}</span>;
      },
    },
    {
      header: " ",
      accessorKey: "enabled",
      cell: (props) => {
        const enabled = props.getValue<boolean>();
        return <BiSolidCircle size={24} fill={enabled ? "green" : "red"} />;
      },
    },
    {
      header: " ",
      accessorKey: "id",
      cell: (props) => (
        <Button icon={<BiPencil />} onClick={() => setIngredientDialogOpenState(props.getValue<number>())} />
      ),
    },
    {
      header: " ",
      accessorFn: (row) => row,
      cell: (props) => {
        const ing = props.getValue<Ingredient>();

        return <Button icon={ing.enabled ? <BiTrash /> : <BiUpload />} onClick={() => handleUpdate(ing)} />;
      },
    },
  ];
};
