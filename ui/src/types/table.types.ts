import { colors } from "@/common/colors";

import { Coords } from ".";

export type Table = {
  id: number;
  layoutId: number;
  status: TableStatus;
  qrId: string;
  positionX: number;
  positionY: number;
  surface: Coords[];
  orders: TableOrder[];
};

export type TableOrder = {
  productId: number;
  amount: number;
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
} & Required<TableRequest>;

export type UpdateTableRequest = {
  orders?: TableOrder[];
} & TableRequest;

type TableRequest = {
  status?: TableStatus;
  positionX?: number;
  positionY?: number;
  surface?: Coords[];
};

export enum TableStatus {
  Free,
  Calling,
  Occupied,
  Arrived,
}

export const tableStatusNameMap = {
  [TableStatus.Free]: "Libre",
  [TableStatus.Calling]: "Llamando",
  [TableStatus.Occupied]: "Ocupada",
  [TableStatus.Arrived]: "Ingresado",
};

export const tableStatusColorMap = {
  [TableStatus.Free]: colors.green,
  [TableStatus.Calling]: colors.yellow,
  [TableStatus.Occupied]: colors.red,
  [TableStatus.Arrived]: colors.blue,
};
