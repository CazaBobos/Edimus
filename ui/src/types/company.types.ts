import { Premise } from "./premise.types";

export type Company = {
  id: number;
  name: string;
  slogan: string;
  slug: string;
  premises: Premise[];
  enabled: boolean;
  reactiveStock: boolean;
  publicPrices: boolean;
  publicOrders: boolean;
};

export type UpdateCompanyRequest = {
  name?: string;
  slogan?: string;
  slug?: string;
  reactiveStock?: boolean;
  publicPrices?: boolean;
  publicOrders?: boolean;
};
