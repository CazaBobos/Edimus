import { Ingredient, Nullable, Product, Sector, Table } from "@/types";
import { create } from "zustand";

type HeaderPanelState = {
  open: boolean;
  selectedTab: number;
};

type AdminStore = {
  squareSize: number;
  headerPanelState: HeaderPanelState;
  setHeaderPanelState: (state: Partial<HeaderPanelState>) => void;
  tableDialogOpenState: Nullable<Table>;
  setTableDialogOpenState: (table: Nullable<Table>) => void;
  productDialogOpenState: Nullable<Product>;
  setProductDialogOpenState: (product: Nullable<Product>) => void;
  ingredientDialogOpenState: Nullable<Ingredient>;
  setIngredientDialogOpenState: (ingredient: Nullable<Ingredient>) => void;
  sectorDialogOpenState: Nullable<Sector>;
  setSectorDialogOpenState: (sector: Nullable<Sector>) => void;
};

export const useAdminStore = create<AdminStore>()((set) => ({
  squareSize: 32,
  headerPanelState: {
    open: false,
    selectedTab: 0,
  },
  setHeaderPanelState: (state: Partial<HeaderPanelState>) =>
    set((store) => ({
      headerPanelState: { ...store.headerPanelState, ...state },
    })),
  tableDialogOpenState: undefined,
  setTableDialogOpenState: (table: Nullable<Table>) =>
    set(() => ({
      tableDialogOpenState: table,
    })),
  productDialogOpenState: undefined,
  setProductDialogOpenState: (product: Nullable<Product>) =>
    set(() => ({
      productDialogOpenState: product,
    })),
  ingredientDialogOpenState: undefined,
  setIngredientDialogOpenState: (ingredient: Nullable<Ingredient>) =>
    set(() => ({
      ingredientDialogOpenState: ingredient,
    })),
  sectorDialogOpenState: undefined,
  setSectorDialogOpenState: (sector: Nullable<Sector>) =>
    set(() => ({
      sectorDialogOpenState: sector,
    })),
}));
