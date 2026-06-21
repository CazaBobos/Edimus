import { Table as MantineTable } from "@mantine/core";
import { ColumnDef, flexRender, getCoreRowModel, useReactTable } from "@tanstack/react-table";

import styles from "./styles.module.scss";

type TableProps<T> = {
  data: T[];
  columns: ColumnDef<T, unknown>[];
  onRowClick?: (row: T) => void;
  isRowClickable?: (row: T) => boolean;
};

export default function Table<T>({ data, columns, onRowClick, isRowClickable }: TableProps<T>) {
  const table = useReactTable({ data, columns, getCoreRowModel: getCoreRowModel() });

  return (
    <MantineTable
      withTableBorder
      withColumnBorders
      classNames={{ table: styles.table, thead: styles.thead, th: styles.th, td: styles.td }}
    >
      <MantineTable.Thead>
        {table.getHeaderGroups().map((hg) => (
          <MantineTable.Tr key={hg.id}>
            {hg.headers.map((header, i) => (
              <MantineTable.Th key={`${header.id} ${i}`}>
                {flexRender(header.column.columnDef.header, header.getContext())}
              </MantineTable.Th>
            ))}
          </MantineTable.Tr>
        ))}
      </MantineTable.Thead>
      <MantineTable.Tbody>
        {table.getRowModel().rows.map((row) => {
          const clickable = isRowClickable ? isRowClickable(row.original) : !!onRowClick;
          return (
            <MantineTable.Tr
              key={row.id}
              className={clickable ? styles.clickable : undefined}
              onClick={clickable ? () => onRowClick?.(row.original) : undefined}
            >
              {row.getVisibleCells().map((cell, i) => (
                <MantineTable.Td key={`${cell.id} ${i}`}>
                  {flexRender(cell.column.columnDef.cell, cell.getContext())}
                </MantineTable.Td>
              ))}
            </MantineTable.Tr>
          );
        })}
      </MantineTable.Tbody>
    </MantineTable>
  );
}
