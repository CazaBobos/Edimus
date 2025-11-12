export type Product = {
  id: number;
  parentId?: number;
  categoryId: number;
  name: string;
  description?: string;
  price?: number;
  tags?: string[];
  image?: Buffer | null;
  enabled: boolean;
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
  tags?: string[];
  enabled?: boolean;
};

export type ProductRequest = {
  parentId?: number;
  categoryId?: number;
  name?: string;
  description?: string;
  price?: number;
  tags?: string[];
};

export type CreateProductRequest = {
  companyId: number;
} & Required<ProductRequest>;

export type UpdateProductRequest = {
  id: number;
} & ProductRequest;
