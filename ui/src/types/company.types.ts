import { Premise } from "./premise.types";

export type Company = {
  id: number;
  name: string;
  slogan: string;
  acronym: string;
  premises: Premise[];
  enabled: boolean;
  reactiveStock: boolean;
  publicPrices: boolean;
  publicOrders: boolean;
};

export type UpdateCompanyRequest = {
  name?: string;
  slogan?: string;
  reactiveStock?: boolean;
  publicPrices?: boolean;
  publicOrders?: boolean;
};
