import { useBoundaryMutation } from "@/hooks/mutations/useBoundaryMutation";
import { BoundaryType, Layout } from "@/types";
import { Drawer, SegmentedControl, Slider } from "@mantine/core";
import { useEffect, useRef, useState } from "react";
import { BiEraser, BiSave } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import styles from "./styles.module.scss";

const DEFAULT_CELL = 24;

type BoundaryCell = { x: number; y: number; type: BoundaryType };
type PaintMode = "add" | "remove" | "change";

type Props = {
  layout: Layout | null;
  opened: boolean;
  onClose: () => void;
};

export const BoundaryEditorDrawer = ({ layout, opened, onClose }: Props) => {
  const { saveBoundariesMutation } = useBoundaryMutation();

  const [draft, setDraft] = useState<BoundaryCell[]>([]);
  const [activeTool, setActiveTool] = useState<BoundaryType>(BoundaryType.Wall);
  const [cellSize, setCellSize] = useState(DEFAULT_CELL);

  const draftRef = useRef<BoundaryCell[]>([]);
  const isPainting = useRef(false);
  const paintMode = useRef<PaintMode>("add");

  useEffect(() => {
    if (opened && layout) {
      const initial = layout.boundaries.map((b) => ({ x: b.x, y: b.y, type: b.type }));
      draftRef.current = initial;
      setDraft(initial);
    }
  }, [opened, layout]);

  useEffect(() => {
    const stop = () => {
      isPainting.current = false;
    };
    window.addEventListener("mouseup", stop);
    return () => window.removeEventListener("mouseup", stop);
  }, []);

  const getCell = (x: number, y: number) => draftRef.current.find((c) => c.x === x && c.y === y);

  const applyPaint = (x: number, y: number) => {
    const existing = getCell(x, y);
    let next: BoundaryCell[];

    if (paintMode.current === "remove") {
      if (!existing) return;
      next = draftRef.current.filter((c) => !(c.x === x && c.y === y));
    } else if (paintMode.current === "change") {
      if (!existing || existing.type === activeTool) return;
      next = draftRef.current.map((c) => (c.x === x && c.y === y ? { ...c, type: activeTool } : c));
    } else {
      if (existing) return;
      next = [...draftRef.current, { x, y, type: activeTool }];
    }

    draftRef.current = next;
    setDraft(next);
  };

  const handleMouseDown = (x: number, y: number) => {
    const existing = getCell(x, y);
    if (!existing) {
      paintMode.current = "add";
    } else if (existing.type === activeTool) {
      paintMode.current = "remove";
    } else {
      paintMode.current = "change";
    }
    isPainting.current = true;
    applyPaint(x, y);
  };

  const handleMouseEnter = (x: number, y: number) => {
    if (!isPainting.current) return;
    applyPaint(x, y);
  };

  const handleClear = () => {
    draftRef.current = [];
    setDraft([]);
  };

  const handleSave = () => {
    if (!layout) return;
    saveBoundariesMutation.mutate({ id: layout.id, boundaries: draftRef.current }, { onSuccess: onClose });
  };

  if (!layout) return null;

  return (
    <Drawer opened={opened} onClose={onClose} title={`Límites: ${layout.name}`} position="right" size="xl" shadow="xl">
      <div className={styles.content}>
        <div className={styles.toolbar}>
          <div className={styles.toolGroup}>
            <SegmentedControl
              value={String(activeTool)}
              onChange={(v) => setActiveTool(Number(v) as BoundaryType)}
              data={[
                { value: String(BoundaryType.Wall), label: "Pared" },
                { value: String(BoundaryType.Doorway), label: "Puerta" },
              ]}
            />
            <div className={styles.zoomControl}>
              <span className={styles.zoomLabel}>Zoom</span>
              <Slider
                value={cellSize}
                onChange={setCellSize}
                min={12}
                max={48}
                step={4}
                w={100}
                label={(v) => `${Math.round((v / DEFAULT_CELL) * 100)}%`}
              />
            </div>
          </div>
          <div className={styles.actionGroup}>
            <Button icon={<BiEraser />} label="Limpiar" onClick={handleClear} />
            <Button
              variant="brand"
              icon={<BiSave />}
              label="Guardar"
              onClick={handleSave}
              disabled={saveBoundariesMutation.isPending}
            />
          </div>
        </div>

        <div className={styles.gridWrapper}>
          <div
            className={styles.grid}
            style={{ width: layout.width * cellSize, height: layout.height * cellSize }}
            onMouseLeave={() => {
              isPainting.current = false;
            }}
            draggable={false}
          >
            {Array.from({ length: layout.height }, (_, y) =>
              Array.from({ length: layout.width }, (_, x) => {
                const cell = draft.find((c) => c.x === x && c.y === y);
                return (
                  <div
                    key={`${x},${y}`}
                    className={styles.cell}
                    data-type={cell !== undefined ? (cell.type === BoundaryType.Wall ? "wall" : "doorway") : "empty"}
                    style={{ top: y * cellSize, left: x * cellSize, width: cellSize, height: cellSize }}
                    onMouseDown={() => handleMouseDown(x, y)}
                    onMouseEnter={() => handleMouseEnter(x, y)}
                  />
                );
              }),
            )}
          </div>
        </div>

        <div className={styles.legend}>
          <span className={styles.legendItem} data-type="wall">
            Pared
          </span>
          <span className={styles.legendItem} data-type="doorway">
            Puerta
          </span>
        </div>
      </div>
    </Drawer>
  );
};
