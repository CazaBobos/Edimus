export type Wall = {
  type: WallType;
  surface: [number, number][];
};

export enum WallType {
  Solid,
  Doorway,
}
