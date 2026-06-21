import { auditLogsApi } from "@/services";
import { AuditLogsParams } from "@/types";

import { useAxiosQuery } from "../axiosHooks";

export const useAuditLogsQuery = (params: AuditLogsParams = {}) => {
  const query = useAxiosQuery({
    queryKey: ["audit", params],
    queryFn: () => auditLogsApi.findMany(params),
  });

  return {
    data: query.data,
    isLoading: query.isLoading,
  };
};
