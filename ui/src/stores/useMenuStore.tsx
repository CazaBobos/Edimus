import { create } from "zustand";

type MenuStore = {
  isFiltersDialogOpen: boolean;
  setIsFiltersDialogOpen: (value: boolean) => void;
  isOrderDialogOpen: boolean;
  setIsOrderDialogOpen: (value: boolean) => void;
};

export const useMenuStore = create<MenuStore>()((set) => ({
  isFiltersDialogOpen: false,
  setIsFiltersDialogOpen: (value: boolean) =>
    set(() => ({
      isFiltersDialogOpen: value,
    })),
  isOrderDialogOpen: false,
  setIsOrderDialogOpen: (value: boolean) =>
    set(() => ({
      isOrderDialogOpen: value,
    })),
}));
