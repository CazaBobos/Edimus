export type Category = {
  id: number;
  companyId: number;
  name: string;
  enabled: boolean;
};

export type GetCategoriesParams = {
  limit?: number;
  page?: number;
  companyId?: number;
  name?: string;
  enabled?: boolean;
};

export type CreateCategoryRequest = {
  companyId: number;
  name: string;
};

export type UpdateCategoryRequest = {
  name?: string;
};
