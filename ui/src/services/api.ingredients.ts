import { Ingredient, IngredientRequest } from "@/types";

export const ingredientsApi = {
  create: (request: Required<IngredientRequest>) => {
    return 1;
  },
  update: (id: number, request: IngredientRequest) => {},
  findMany: (): Ingredient[] => {
    return [
      {
        id: 1,
        name: "Harina 000",
        stock: 50,
        unit: "kg",
        alert: 10,
        enabled: true,
      },
      {
        id: 2,
        name: "Tomate fresco",
        stock: 30,
        unit: "kg",
        alert: 5,
        enabled: true,
      },
      {
        id: 3,
        name: "Queso mozzarella",
        stock: 20,
        unit: "kg",
        alert: 3,
        enabled: true,
      },
      {
        id: 4,
        name: "Aceite de oliva",
        stock: 15,
        unit: "litro",
        alert: 2,
        enabled: true,
      },
      {
        id: 5,
        name: "Cebolla",
        stock: 3,
        unit: "kg",
        alert: 4,
        enabled: true,
      },
      {
        id: 6,
        name: "Pollo",
        stock: 10,
        unit: "kg",
        alert: 2,
        enabled: true,
      },
      {
        id: 7,
        name: "Pasta seca",
        stock: 40,
        unit: "kg",
        alert: 8,
        enabled: true,
      },
      {
        id: 8,
        name: "Salsa de tomate",
        stock: 12,
        unit: "litro",
        alert: 2,
        enabled: true,
      },
      {
        id: 9,
        name: "Albaca",
        stock: 200,
        unit: "gr",
        alert: 50,
        enabled: true,
      },
      {
        id: 10,
        name: "Vino tinto",
        stock: 8,
        unit: "litro",
        alert: 1,
        enabled: false,
      },
    ];
  },
};
