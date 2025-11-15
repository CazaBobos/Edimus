export type Product = {
  id: number;
  parentId?: number;
  categoryId?: number;
  name: string;
  description?: string;
  price?: number;
  tags?: number[];
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
  tags?: number[];
};
