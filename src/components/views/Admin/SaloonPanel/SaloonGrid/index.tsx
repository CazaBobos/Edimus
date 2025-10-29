import { useSectorsQuery } from "@/hooks/queries/useSectorsQuery";
import { useTablesQuery } from "@/hooks/queries/useTablesQuery";
import { useWallsQuery } from "@/hooks/queries/useWallsQuery";
import { useAdminStore } from "@/stores";
import { TableStatus, WallType } from "@/types";

import { SectorTag } from "./SectorTag";
import { Square } from "./Square";
import styles from "./styles.module.scss";

export const SaloonGrid = () => {
  const { data: walls } = useWallsQuery();
  const { data: tables } = useTablesQuery();
  const { data: sectors } = useSectorsQuery();
  const { setTableDialogOpenState } = useAdminStore();

  return (
    <div className={styles.saloonGrid}>
      {[...Array(24).keys()].map((_, x) =>
        [...Array(24).keys()].map((_, y) => <Square key={[x, y].join(",")} position={{ x: x, y: y }} color="grey" />),
      )}
      {sectors.map((sector, i) => (
        <div
          key={`${sector.name} ${i}`}
          style={{
            position: "relative",
            left: `${sector.positionX * 32}px`,
            top: `${sector.positionY * 32}px`,
            width: "fit-content",
            height: "fit-content",
          }}
        >
          {sector.name && <SectorTag {...sector} />}
          {sector.surface.map((s) => (
            <Square key={[s[0], s[1]].join(",")} position={{ x: s[0], y: s[1] }} color={sector.color} />
          ))}
        </div>
      ))}
      <div
        style={{
          position: "relative",
          left: `${4 * 32}px`,
          top: `${2 * 32}px`,
          width: "fit-content",
          height: "fit-content",
        }}
      >
        {tables?.map((t) =>
          t.surface.map((s) => (
            <Square
              onClick={() => setTableDialogOpenState(t.id)}
              key={[s[0], s[1]].join(",")}
              position={{ x: s[0], y: s[1] }}
              color={
                {
                  [TableStatus.Free]: "green",
                  [TableStatus.Calling]: "orange",
                  [TableStatus.Occupied]: "red",
                }[t.status]
              }
              content={t.id}
              filled
            />
          )),
        )}
      </div>
      <div
        style={{
          position: "relative",
          left: `${4 * 32}px`,
          top: `${2 * 32}px`,
          width: "fit-content",
          height: "fit-content",
        }}
      >
        {walls.map((e) =>
          e.surface.map((s) => (
            <Square
              key={[s[0], s[1]].join(",")}
              position={{ x: s[0], y: s[1] }}
              color={
                {
                  [WallType.Solid]: "grey",
                  [WallType.Doorway]: "blue",
                }[e.type]
              }
              filled
            />
          )),
        )}
      </div>
    </div>
  );
};
