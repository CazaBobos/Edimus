import { Coords } from ".";

export type Layout = {
  id: number;
  name: string;
  height: number;
  width: number;
  boundaries: (Coords & { type: BoundaryType })[];
  enabled: boolean;
};

export enum BoundaryType {
  Wall,
  Doorway,
}

export type GetLayoutsParams = {
  premiseId?: number;
  enabled?: boolean;
  limit?: number;
  page?: number;
};

export type CreateLayoutRequest = {
  premiseId: number;
  name: string;
  height: number;
  width: number;
};

export type UpdateLayoutRequest = {
  name?: string;
  height?: number;
  width?: number;
};
