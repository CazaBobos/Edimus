export * from "./api.companies";
export * from "./api.categories";
export * from "./api.ingredients";
export * from "./api.products";
export * from "./api.sectors";
export * from "./api.stats";
export * from "./api.tables";

export type ExceptionResponse = {
  status: number;
  errorMessages: string[];
  innerErrorMessages?: string[];
};
