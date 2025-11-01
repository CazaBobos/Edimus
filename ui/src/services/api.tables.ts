import { Table, TableStatus } from "@/types";

export const tablesApi = {
  findMany: (): Table[] => {
    return [
      {
        id: 1,
        layoutId: 1,
        qr: "",
        positionX: 0,
        positionY: 0,
        surface: [[0, 0]],
        status: TableStatus.Occupied,
      },
      {
        id: 2,
        layoutId: 1,
        qr: "",
        positionX: 2,
        positionY: 2,
        surface: [[2, 2]],
        status: TableStatus.Free,
      },
      {
        id: 3,
        layoutId: 1,
        qr: "",
        positionX: 7,
        positionY: 0,
        surface: [
          [7, 0],
          [7, 1],
        ],
        status: TableStatus.Free,
      },
      {
        id: 4,
        layoutId: 1,
        qr: "",
        positionX: 9,
        positionY: 0,
        surface: [
          [9, 0],
          [9, 1],
        ],
        status: TableStatus.Occupied,
      },
      {
        id: 5,
        layoutId: 1,
        qr: "",
        positionX: 11,
        positionY: 0,
        surface: [[11, 0]],
        status: TableStatus.Calling,
      },
      {
        id: 6,
        layoutId: 1,
        qr: "",
        positionX: 13,
        positionY: 0,
        surface: [
          [13, 0],
          [13, 1],
        ],
        status: TableStatus.Occupied,
      },
      {
        id: 7,
        layoutId: 1,
        qr: "",
        positionX: 15,
        positionY: 1,
        surface: [[15, 1]],
        status: TableStatus.Calling,
      },
    ];
  },
};
