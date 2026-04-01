import { colors } from "@/common/colors";
import { useCompanyQuery } from "@/hooks/queries/useCompanyQuery";
import { useSectorsQuery } from "@/hooks/queries/useSectorsQuery";
import { useTablesQuery } from "@/hooks/queries/useTablesQuery";
import { useTablesHub } from "@/hooks/useTablesHub";
import { useAdminStore } from "@/stores";
import { BoundaryType, tableStatusColorMap } from "@/types";
import React from "react";

import { Area } from "./Area";
import { SectorTag } from "./SectorTag";
import { Square } from "./Square";
import styles from "./styles.module.scss";

export const SaloonGrid = ({ layoutId }: { layoutId: number | undefined }) => {
  const { data: company } = useCompanyQuery(1);
  const { data: tables } = useTablesQuery();
  const { data: sectors } = useSectorsQuery();
  const squareSize = useAdminStore((store) => store.squareSize);
  const setTableDialogOpenState = useAdminStore((store) => store.setTableDialogOpenState);
  const previewPosition = useAdminStore((store) => store.previewPosition);

  const currentLayout = company?.premises[0].layouts.find((l) => l.id === layoutId);

  useTablesHub(currentLayout?.id);

  if (!currentLayout) return null;

  const gridWidth = currentLayout?.width;
  const gridHeight = currentLayout?.height;

  return (
    <div
      className={styles.saloonGrid}
      style={{
        width: `${gridWidth * squareSize}px`,
        height: `${gridHeight * squareSize}px`,
        marginTop: `${squareSize}px`,
        marginLeft: `${squareSize}px`,
      }}
    >
      {[...Array(gridWidth).keys()].map((_, x) =>
        [...Array(gridHeight).keys()].map((_, y) => (
          <React.Fragment key={[x, y].join(",")}>
            {y === 0 && <Square position={{ x, y: -1 }} color={colors.darkText} content={x} />}
            {x === 0 && <Square position={{ x: -1, y }} color={colors.darkText} content={y} />}
            <Square position={{ x, y }} color={colors.grey} />
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
              [BoundaryType.Wall]: colors.grey,
              [BoundaryType.Doorway]: colors.blue,
            }[b.type]
          }
        />
      ))}
      {sectors.map((sector, i) => {
        const posX = previewPosition?.sectorId === sector.id ? previewPosition.x : sector.positionX;
        const posY = previewPosition?.sectorId === sector.id ? previewPosition.y : sector.positionY;
        return (
          <Area key={`${sector.name} ${i}`} positionX={posX} positionY={posY}>
            {sector.name && <SectorTag sector={sector} />}
            {sector.surface.map((coord) => (
              <Square key={[coord.x, coord.y].join(",")} position={coord} color={sector.color} />
            ))}
          </Area>
        );
      })}
      {tables?.map((table, i) => {
        const posX = previewPosition?.tableId === table.id ? previewPosition.x : table.positionX;
        const posY = previewPosition?.tableId === table.id ? previewPosition.y : table.positionY;
        return (
          <Area key={`${table.layoutId} ${i}`} positionX={posX} positionY={posY}>
            {table.surface.map((coord) => (
              <Square
                onClick={() => setTableDialogOpenState(table)}
                key={[coord.x, coord.y].join(",")}
                position={coord}
                color={tableStatusColorMap[table.status]}
                content={table.id}
                filled
              />
            ))}
          </Area>
        );
      })}
    </div>
  );
};
