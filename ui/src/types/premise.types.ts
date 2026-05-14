import { Layout } from "./layout.types";

export type Premise = {
  id: number;
  companyId: number;
  name: string;
  layouts: Layout[];
  enabled: boolean;
};

export type GetPremisesParams = {
  companyId?: number;
  enabled?: boolean;
  limit?: number;
  page?: number;
};

export type CreatePremiseRequest = {
  companyId: number;
  name: string;
};

export type UpdatePremiseRequest = {
  name?: string;
};
