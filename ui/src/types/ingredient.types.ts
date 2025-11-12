export type Ingredient = {
  id: number;
  name: string;
  stock: number;
  unit: MeasurementUnit;
  alert: number;
  enabled: boolean;
};

export type IngredientRequest = {
  name?: string;
  stock?: number;
  unit?: MeasurementUnit;
  alert?: number;
  enabled?: boolean;
};

export type GetIngredientsParams = {
  limit?: number;
  page?: number;
  name?: string;
  minStock?: number;
  maxStock?: number;
  minAlert?: number;
  maxAlert?: number;
  unit?: MeasurementUnit;
  enabled?: boolean;
};

export enum MeasurementUnit {
  Kilogram,
  Gram,
  Unit,
  Pound,
  Liter,
  Mililiter,
}
