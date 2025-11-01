import { useIngredientMutations } from "@/hooks/mutations/useIngredientMutations";
import { useAdminStore } from "@/stores";
import { Ingredient } from "@/types";
import { ColumnDef } from "@tanstack/react-table";
import { BiPencil, BiSolidCircle, BiTrash, BiUpload } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

export const useIngredientsColumnDefs = (): ColumnDef<Ingredient>[] => {
  const { setIngredientDialogOpenState } = useAdminStore();

  const { updateIngredientMutation } = useIngredientMutations();

  const handleUpdate = (ingredient: Ingredient) => {
    const { id, enabled } = ingredient;

    updateIngredientMutation.mutate({ id, request: { enabled: !enabled } });
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
        <Button icon={<BiPencil size={24} />} onClick={() => setIngredientDialogOpenState(props.getValue<number>())} />
      ),
    },
    {
      header: " ",
      accessorFn: (row) => row,
      cell: (props) => {
        const ing = props.getValue<Ingredient>();

        return (
          <Button
            icon={ing.enabled ? <BiTrash size={24} /> : <BiUpload size={24} />}
            onClick={() => handleUpdate(ing)}
          />
        );
      },
    },
  ];
};
