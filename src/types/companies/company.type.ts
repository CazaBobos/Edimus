export type Company = {
  id: number;
  name: string;
  slogan: string;
  acronym: string;
  publicPrices?: boolean;
  publicBill?: boolean;
  reactiveStock?: boolean;
  enabled: boolean;
};
