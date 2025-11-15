import { create } from "zustand";

type HeaderPanelState = {
  open: boolean;
  selectedTab: number;
};

type AdminStore = {
  headerPanelState: HeaderPanelState;
  setHeaderPanelState: (state: Partial<HeaderPanelState>) => void;
  tableDialogOpenState: number | undefined;
  setTableDialogOpenState: (value: number | undefined) => void;
  productDialogOpenState: number | undefined;
  setProductDialogOpenState: (value: number | undefined) => void;
  ingredientDialogOpenState: number | undefined;
  setIngredientDialogOpenState: (value: number | undefined) => void;
};

export const useAdminStore = create<AdminStore>()((set) => ({
  headerPanelState: {
    open: false,
    selectedTab: 0,
  },
  setHeaderPanelState: (state: Partial<HeaderPanelState>) =>
    set((store) => ({
      headerPanelState: { ...store.headerPanelState, ...state },
    })),
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
