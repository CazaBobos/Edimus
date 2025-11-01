import { create } from "zustand";

type AdminStore = {
  tableDialogOpenState: number | undefined;
  setTableDialogOpenState: (value: number | undefined) => void;
  productDialogOpenState: number | undefined;
  setProductDialogOpenState: (value: number | undefined) => void;
  ingredientDialogOpenState: number | undefined;
  setIngredientDialogOpenState: (value: number | undefined) => void;
};

export const useAdminStore = create<AdminStore>()((set) => ({
  tableDialogOpenState: undefined,
  setTableDialogOpenState: (value: number | undefined) =>
    set(() => ({
      tableDialogOpenState: value,
    })),
  productDialogOpenState: undefined,
  setProductDialogOpenState: (value: number | undefined) =>
    set(() => ({
      productDialogOpenState: value,
    })),
  ingredientDialogOpenState: undefined,
  setIngredientDialogOpenState: (value: number | undefined) =>
    set(() => ({
      ingredientDialogOpenState: value,
    })),
}));
