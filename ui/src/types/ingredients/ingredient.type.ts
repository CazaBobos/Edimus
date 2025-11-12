export type Ingredient = {
  id: number;
  name: string;
  stock: number;
  unit: MeasurementUnit;
  alert: number;
  enabled: boolean;
};

export enum MeasurementUnit {
  Kilogram,
  Gram,
  Unit,
  Pound,
  Liter,
  Mililiter,
}
