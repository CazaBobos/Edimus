export type Tag = {
  id: number;
  name: string;
  enabled: boolean;
};

export type GetTagsParams = {
  name?: string;
  enabled?: boolean;
  companyId?: number;
  limit?: number;
  page?: number;
};
