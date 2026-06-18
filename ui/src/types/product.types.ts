import { Consumption } from "./consumption.types";
import { Tag } from "./tag.types";

export type Product = {
  id: number;
  parentId?: number;
  categoryId?: number;
  name: string;
  description?: string;
  price?: number;
  tags?: Tag[];
  imageId?: number | null;
  enabled: boolean;
  consumptions: Consumption[];
};

export type GetProductsParams = {
  limit?: number;
  page?: number;
  companyId?: number;
  name?: string;
  description?: string;
  categoryIds?: number[];
  minPrice?: number;
  maxPrice?: number;
  tags?: number[];
  enabled?: boolean;
};

export type ProductRequest = {
  companyId?: number;
  parentId?: number;
  categoryId?: number;
  name?: string;
  description?: string;
  price?: number;
  tagIds?: number[];
  consumptions?: Consumption[];
};
