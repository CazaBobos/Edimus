export type Table = {
  id: number;
  layoutId: number;
  status: TableStatus;
  qr: string;
  positionX: number;
  positionY: number;
  surface: [number, number][];
  enabled?: boolean;
};

export enum TableStatus {
  Free,
  Calling,
  Occupied,
}
