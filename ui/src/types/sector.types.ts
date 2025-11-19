import { Coords } from ".";

export type Sector = {
  id: number;
  layoutId: number;
  name: string;
  positionX: number;
  positionY: number;
  color: string;
  surface: Coords[];
  enabled: boolean;
};

export type GetSectorsParams = {
  limit?: number;
  page?: number;
  layoutId?: number;
  name?: string;
  enabled?: boolean;
};

export type CreateSectorRequest = {
  layoutId: number;
} & Required<UpdateSectorRequest>;

export type UpdateSectorRequest = {
  name?: string;
  color?: string;
  positionX?: number;
  positionY?: number;
  //surface?: [number, number][];
};
