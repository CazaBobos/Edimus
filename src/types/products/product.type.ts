export type Product = {
  id: number;
  parentId?: number;
  categoryId: number;
  name: string;
  description?: string;
  price?: number;
  tags?: string[];
  image?: Buffer | null;
  variants?: Pick<Product, "name" | "description" | "price">[];
  enabled: boolean;
};
