import { useCompanyQuery } from "@/hooks/queries/useCompanyQuery";
import { useSectorsQuery } from "@/hooks/queries/useSectorsQuery";
import { useTablesQuery } from "@/hooks/queries/useTablesQuery";
import { useAdminStore } from "@/stores";
import { TableStatus, BoundaryType } from "@/types";
import React from "react";

import { Area } from "./Area";
import { SectorTag } from "./SectorTag";
import { Square } from "./Square";
import styles from "./styles.module.scss";

export const SaloonGrid = () => {
  const { data: company } = useCompanyQuery(1);
  const { data: tables } = useTablesQuery();
  const { data: sectors } = useSectorsQuery();
  const { setTableDialogOpenState } = useAdminStore();

  const currentLayout = company?.premises[0].layouts[0];

  if (!currentLayout) return null;

  const gridWidth = currentLayout?.width;
  const gridHeight = currentLayout?.height;
  return (
    <div
      className={styles.saloonGrid}
      style={{
        width: `${gridWidth * 32}px`,
        height: `${gridHeight * 32}px`,
        marginTop: "32px",
        marginLeft: "32px",
      }}
    >
      {[...Array(gridWidth).keys()].map((_, x) =>
        [...Array(gridHeight).keys()].map((_, y) => (
          <React.Fragment key={[x, y].join(",")}>
            {y === 0 && <Square position={{ x, y: -1 }} color="#383838" content={x} />}
            {x === 0 && <Square position={{ x: -1, y }} color="#383838" content={y} />}
            <Square position={{ x, y }} color="grey" />
          </React.Fragment>
        )),
      )}
      {currentLayout.boundaries.map((b) => (
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
      {sectors.map((sector, i) => (
        <Area key={`${sector.name} ${i}`} positionX={sector.positionX} positionY={sector.positionY}>
          {sector.name && <SectorTag sector={sector} />}
          {sector.surface.map((coord) => (
            <Square key={[coord.x, coord.y].join(",")} position={coord} color={sector.color} />
          ))}
        </Area>
      ))}
      {tables?.map((table, i) => (
        <Area key={`${table.layoutId} ${i}`} positionX={table.positionX} positionY={table.positionY}>
          {table.surface.map((coord) => (
            <Square
              onClick={() => setTableDialogOpenState(table)}
              key={[coord.x, coord.y].join(",")}
              position={coord}
              color={
                {
                  [TableStatus.Free]: "green",
                  [TableStatus.Calling]: "orange",
                  [TableStatus.Occupied]: "red",
                }[table.status]
              }
              content={table.id}
              filled
            />
          ))}
        </Area>
      ))}
    </div>
  );
};
