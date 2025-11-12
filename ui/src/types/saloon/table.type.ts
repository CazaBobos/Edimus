export type Table = {
  id: number;
  layoutId: number;
  status: TableStatus;
  qr: string;
  positionX: number;
  positionY: number;
  surface: { x: number; y: number }[];
  enabled?: boolean;
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
  Status?: TableStatus;
  positionX?: number;
  positionY?: number;
  surface?: [number, number][];
};

export enum TableStatus {
  Free,
  Calling,
  Occupied,
}
