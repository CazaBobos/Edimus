export enum AuditOperation {
  Created,
  Updated,
  Removed,
  Restored,
}

export type AuditLogChange = {
  propertyName: string;
  oldValue: string | null;
  newValue: string | null;
};

export type AuditLog = {
  id: number;
  entityType: string;
  entityId: string;
  operation: AuditOperation;
  userId: number;
  username: string;
  dateTime: string;
  changes: AuditLogChange[];
};

export type AuditLogsResponse = {
  count: number;
  limit: number | null;
  page: number | null;
  auditLogs: AuditLog[];
};

export type AuditLogsParams = {
  dateFrom?: string;
  dateTo?: string;
  entityType?: string;
  username?: string;
  operation?: AuditOperation;
  limit?: number;
  page?: number;
};
