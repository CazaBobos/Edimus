import { Category, GetCategoriesParams } from "@/types";

export const categoriesApi = {
  findMany: (params: GetCategoriesParams): Category[] => {
    return [
      { id: 1, companyId: 1, name: "Cafetería", enabled: true },
      { id: 2, companyId: 1, name: "Pastelería", enabled: true },
      { id: 3, companyId: 1, name: "Especialidades", enabled: true },
      { id: 4, companyId: 1, name: "Combos", enabled: true },
      { id: 5, companyId: 1, name: "Almuerzos", enabled: true },
      { id: 6, companyId: 1, name: "Bebidas", enabled: true },
      { id: 7, companyId: 1, name: "Coctelería", enabled: true },
    ];
  },
};
