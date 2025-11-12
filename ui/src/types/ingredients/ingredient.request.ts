import { MeasurementUnit } from "./ingredient.type";

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
