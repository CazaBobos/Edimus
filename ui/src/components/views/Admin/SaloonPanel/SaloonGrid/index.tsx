import { useCompanyQuery } from "@/hooks/queries/useCompanyQuery";
import { useSectorsQuery } from "@/hooks/queries/useSectorsQuery";
import { useTablesQuery } from "@/hooks/queries/useTablesQuery";
import { useAdminStore } from "@/stores";
import { TableStatus, BoundaryType } from "@/types";

import { SectorTag } from "./SectorTag";
import { Square } from "./Square";
import styles from "./styles.module.scss";

export const SaloonGrid = () => {
  const { data: company } = useCompanyQuery(1);
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
          {sector.surface.map((coord) => (
            <Square key={[coord.x, coord.y].join(",")} position={coord} color={sector.color} />
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
          t.surface.map((coord) => (
            <Square
              onClick={() => setTableDialogOpenState(t.id)}
              key={[coord.x, coord.y].join(",")}
              position={coord}
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
        {company?.premises[0].layouts[0].boundaries.map((b) => (
          <Square
            key={[b.x, b.y].join(",")}
            position={b}
            filled
            color={
              {
                [BoundaryType.Wall]: "grey",
                [BoundaryType.Doorway]: "blue",
              }[b.type]
            }
          />
        ))}
      </div>
    </div>
  );
};
