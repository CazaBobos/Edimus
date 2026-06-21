"use client";

import { useAuditLogsQuery } from "@/hooks/queries/useAuditLogsQuery";
import { AuditLog, AuditLogChange, AuditLogsParams, AuditOperation } from "@/types";
import { Drawer } from "@mantine/core";
import { ColumnDef } from "@tanstack/react-table";
import { useState } from "react";
import { BiChevronLeft, BiChevronRight } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { ControlState } from "@/components/ui/common";
import { Input } from "@/components/ui/Input";
import { Select, SelectOption } from "@/components/ui/Select";
import Table from "@/components/ui/Table";

import styles from "./styles.module.scss";

const ENTITY_TYPES = [
  "Company",
  "Premise",
  "Layout",
  "Sector",
  "Table",
  "Product",
  "Category",
  "Tag",
  "Ingredient",
  "User",
];

const OPERATION_ENTRIES = (Object.entries(AuditOperation) as [string, AuditOperation][]).filter(([k]) =>
  isNaN(Number(k)),
);

const OPERATION_LABELS: Record<AuditOperation, string> = {
  [AuditOperation.Created]: "Creado",
  [AuditOperation.Updated]: "Actualizado",
  [AuditOperation.Removed]: "Eliminado",
  [AuditOperation.Restored]: "Restaurado",
};

const OPERATION_STYLES: Record<AuditOperation, string> = {
  [AuditOperation.Created]: styles.badge_created,
  [AuditOperation.Updated]: styles.badge_updated,
  [AuditOperation.Removed]: styles.badge_removed,
  [AuditOperation.Restored]: styles.badge_restored,
};

const ENTITY_TYPE_OPTIONS: SelectOption[] = [
  { label: "Todas", value: "" },
  ...ENTITY_TYPES.map((t) => ({ label: t, value: t })),
];

const OPERATION_OPTIONS: SelectOption[] = [
  { label: "Todas", value: "" },
  ...OPERATION_ENTRIES.map(([, value]) => ({ label: OPERATION_LABELS[value], value })),
];

const PAGE_SIZE_OPTIONS: SelectOption[] = [10, 20, 50, 100].map((n) => ({ label: String(n), value: n }));

const isExpandable = (log: AuditLog) => log.operation === AuditOperation.Updated && log.changes.length > 0;

const LOG_COLUMNS: ColumnDef<AuditLog>[] = [
  {
    id: "dateTime",
    header: "Fecha/Hora",
    cell: ({ row }) => {
      const date = new Date(row.original.dateTime);
      const dateStr = date.toLocaleDateString("es-AR", { day: "2-digit", month: "2-digit", year: "numeric" });
      const timeStr = date.toLocaleTimeString("es-AR", { hour: "2-digit", minute: "2-digit" });
      return `${dateStr} ${timeStr}`;
    },
  },
  { accessorKey: "username", header: "Usuario" },
  { accessorKey: "entityType", header: "Entidad" },
  {
    id: "operation",
    header: "Operación",
    cell: ({ row }) => (
      <span className={`${styles.badge} ${OPERATION_STYLES[row.original.operation]}`}>
        {OPERATION_LABELS[row.original.operation]}
      </span>
    ),
  },
  {
    id: "entity",
    header: "ID",
    cell: ({ row }) => (
      <span className={styles.idColumn}>
        #{row.original.id}
        {isExpandable(row.original) && <BiChevronRight />}
      </span>
    ),
  },
];

const CHANGE_COLUMNS: ColumnDef<AuditLogChange>[] = [
  { accessorKey: "propertyName", header: "Campo" },
  { id: "oldValue", header: "Valor anterior", cell: ({ row }) => row.original.oldValue ?? "—" },
  { id: "newValue", header: "Valor nuevo", cell: ({ row }) => row.original.newValue ?? "—" },
];

export const AuditLogs = () => {
  const [selectedLog, setSelectedLog] = useState<AuditLog | null>(null);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(20);
  const [filters, setFilters] = useState<Omit<AuditLogsParams, "limit" | "page">>({});
  const [draft, setDraft] = useState<Omit<AuditLogsParams, "limit" | "page">>({});

  const handleSetDraft = (state: ControlState) => {
    const { name, value } = state;

    if (name === "operation")
      setDraft((prev) => ({
        ...prev,
        operation: value === "" ? undefined : (Number(value) as AuditOperation),
      }));
    else setDraft((prev) => ({ ...prev, [name]: value }));
  };

  const params: AuditLogsParams = { ...filters, limit: pageSize, page };
  const { data, isLoading } = useAuditLogsQuery(params);

  const totalPages = data ? Math.ceil(data.count / pageSize) : 0;

  const handleSearch = () => {
    setFilters(draft);
    setPage(1);
    setSelectedLog(null);
  };

  const handleClear = () => {
    setDraft({});
    setFilters({});
    setPage(1);
    setSelectedLog(null);
  };

  return (
    <div className={styles.container}>
      <h2 className={styles.title}>Registro de auditoría</h2>
      <div className={styles.filters}>
        <Input title="Desde" type="date" value={draft.dateFrom ?? ""} name="dateFrom" onChange={handleSetDraft} />
        <Input title="Hasta" type="date" value={draft.dateTo ?? ""} name="dateTo" onChange={handleSetDraft} />
        <Input title="Usuario" value={draft.username ?? ""} name="username" onChange={handleSetDraft} />
        <Select
          title="Entidad"
          options={ENTITY_TYPE_OPTIONS}
          value={draft.entityType ?? ""}
          name="entityType"
          onChange={handleSetDraft}
        />
        <Select
          title="Operación"
          options={OPERATION_OPTIONS}
          value={draft.operation !== undefined ? String(draft.operation) : ""}
          name="operation"
          onChange={handleSetDraft}
        />
        <Button label="Buscar" variant="brand" onClick={handleSearch} />
        <Button label="Limpiar" variant="subtle" onClick={handleClear} />
      </div>

      {isLoading ? (
        <p className={styles.hint}>Cargando...</p>
      ) : !data || data.auditLogs.length === 0 ? (
        <p className={styles.hint}>Sin resultados</p>
      ) : (
        <>
          <div className={styles.tableWrapper}>
            <Table
              data={data.auditLogs}
              columns={LOG_COLUMNS}
              onRowClick={setSelectedLog}
              isRowClickable={isExpandable}
            />
          </div>
          <div className={styles.pagination}>
            <Button icon={<BiChevronLeft />} onClick={() => setPage((p) => p - 1)} disabled={page <= 1} />
            <span className={styles.pageInfo}>
              {page} / {totalPages}
            </span>
            <Button icon={<BiChevronRight />} onClick={() => setPage((p) => p + 1)} disabled={page >= totalPages} />
            <Select
              options={PAGE_SIZE_OPTIONS}
              value={String(pageSize)}
              onChange={(state) => {
                setPageSize(Number(state.value));
                setPage(1);
              }}
            />
          </div>
        </>
      )}
      <Drawer
        opened={selectedLog !== null}
        onClose={() => setSelectedLog(null)}
        title={selectedLog ? `${selectedLog.entityType} · #${selectedLog.entityId}` : ""}
        position="right"
        size="md"
        shadow="xl"
      >
        {selectedLog && <Table data={selectedLog.changes} columns={CHANGE_COLUMNS} />}
      </Drawer>
    </div>
  );
};
