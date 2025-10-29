import { Product } from "./product.type";

export type GetProductsParams = {
  limit?: number;
  page?: number;
  companyId?: number;
  name?: string;
  description?: string;
  categoryIds?: number[];
  minPrice?: number;
  maxPrice?: number;
  tags?: string[];
  enabled?: boolean;
};

export type ProductRequest = {
  name?: string;
  description?: string;
  categoryId?: number;
  price?: number;
  tags?: string[];
  variants?: Pick<Product, "name" | "description" | "price">[];
};

export type CreateProductRequest = {
  companyId: number;
} & Required<ProductRequest>;

export type UpdateProductRequest = {
  id: number;
} & ProductRequest;
