import { useAdminStore } from "@/stores";
import { Ingredient } from "@/types";
import { ColumnDef } from "@tanstack/react-table";
import { BiPencil, BiSolidCircle, BiTrash, BiUpload } from "react-icons/bi";

export const useIngredientsColumnDefs = (): ColumnDef<Ingredient>[] => {
  const { setIngredientDialogOpenState } = useAdminStore();

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
      accessorFn: (row) => row,
      cell: (props) => {
        const ing = props.cell.getValue<Ingredient>();

        return ing.stock < ing.alert ? (
          <>
            {ing.stock} - <span style={{ color: "red" }}>Stock bajo!</span>
          </>
        ) : (
          ing.stock
        );
      },
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
        <button onClick={() => setIngredientDialogOpenState(props.getValue<number>())}>
          <BiPencil size={24} />
        </button>
      ),
    },
    {
      header: " ",
      accessorKey: "enabled",
      cell: (props) => {
        const enabled = props.getValue<boolean>();

        return enabled ? <BiTrash size={24} /> : <BiUpload size={24} />;
      },
    },
  ];
};
