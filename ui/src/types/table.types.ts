import { Coords } from ".";

export type Table = {
  id: number;
  layoutId: number;
  status: TableStatus;
  qrId: string;
  positionX: number;
  positionY: number;
  surface: Coords[];
  requests: {
    productId: number;
    amount: number;
  }[];
};

export type GetTablesParams = {
  limit?: number;
  page?: number;
  layoutId?: number;
  status?: TableStatus;
  enabled?: boolean;
};

export type CreateTableRequest = {
  layoutId: number;
} & Required<UpdateTableRequest>;

export type UpdateTableRequest = {
  status?: TableStatus;
  positionX?: number;
  positionY?: number;
  surface?: Coords[];
};

export enum TableStatus {
  Free,
  Calling,
  Occupied,
}
