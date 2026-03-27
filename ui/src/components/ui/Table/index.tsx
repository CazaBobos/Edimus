import { Table as MantineTable } from "@mantine/core";
import { ColumnDef, flexRender, getCoreRowModel, useReactTable } from "@tanstack/react-table";

type TableProps<T> = {
  data: T[];
  columns: ColumnDef<T, unknown>[];
};

export default function Table<T>({ data, columns }: TableProps<T>) {
  const table = useReactTable({ data, columns, getCoreRowModel: getCoreRowModel() });

  return (
    <MantineTable striped highlightOnHover withTableBorder withColumnBorders>
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
        {table.getRowModel().rows.map((row) => (
          <MantineTable.Tr key={row.id}>
            {row.getVisibleCells().map((cell, i) => (
              <MantineTable.Td key={`${cell.id} ${i}`}>
                {flexRender(cell.column.columnDef.cell, cell.getContext())}
              </MantineTable.Td>
            ))}
          </MantineTable.Tr>
        ))}
      </MantineTable.Tbody>
    </MantineTable>
  );
}
