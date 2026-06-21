import { AuditLogsParams, AuditLogsResponse } from "@/types";

import { axiosClient } from "./axios";

export const auditLogsApi = {
  findMany: async (params: AuditLogsParams = {}) => {
    const response = await axiosClient.get<AuditLogsResponse>("audit-logs", { params });
    return response.data;
  },
};
