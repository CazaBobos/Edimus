export type Company = {
  id: number;
  name: string;
  slogan: string;
  acronym: string;
  premises: Premise[];
  enabled: boolean;
};

export type Premise = {
  id: number;
  companyId: number;
  name: string;
  layouts: Layout[];
  enabled: boolean;
};

export type Layout = {
  id: number;
  name: string;
  boundaries: { x: number; y: number; type: BoundaryType }[];
};

export enum BoundaryType {
  Wall,
  Doorway,
}
