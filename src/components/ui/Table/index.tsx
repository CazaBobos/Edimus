import { useReactTable, getCoreRowModel, flexRender, ColumnDef } from "@tanstack/react-table";

import styles from "./styles.module.scss";

type TableProps<T> = {
  data: T[];
  columns: ColumnDef<T, unknown>[];
};
export default function Table<T>(props: TableProps<T>) {
  const { data, columns } = props;
  const table = useReactTable({ data, columns, getCoreRowModel: getCoreRowModel() });

  return (
    <table className={styles.table}>
      <thead>
        {table.getHeaderGroups().map((hg) => (
          <tr key={hg.id}>
            {hg.headers.map((header, i) => (
              <th key={`${header.id} ${i}`}>{flexRender(header.column.columnDef.header, header.getContext())}</th>
            ))}
          </tr>
        ))}
      </thead>
      <tbody>
        {table.getRowModel().rows.map((row) => (
          <tr key={row.id}>
            {row.getVisibleCells().map((cell, i) => (
              <td key={`${cell.id} ${i}`}>{flexRender(cell.column.columnDef.cell, cell.getContext())}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
}
