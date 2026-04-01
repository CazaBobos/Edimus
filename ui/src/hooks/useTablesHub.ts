import { Table, TableStatus } from "@/types";
import * as signalR from "@microsoft/signalr";
import { useQueryClient } from "@tanstack/react-query";
import { useEffect } from "react";

// Strip the "/api" suffix to reach the root where hubs are mounted
const HUB_URL = `${process.env.NEXT_PUBLIC_API_BASE_URL!.replace(/\/api\/?$/, "")}/hubs/tables`;

const statusMap: Record<string, TableStatus> = {
  Free: TableStatus.Free,
  Calling: TableStatus.Calling,
  Occupied: TableStatus.Occupied,
};

export const useTablesHub = (layoutId: number | undefined) => {
  const queryClient = useQueryClient();

  useEffect(() => {
    if (!layoutId) return;

    const connection = new signalR.HubConnectionBuilder().withUrl(HUB_URL).withAutomaticReconnect().build();

    connection.on("TableStatusChanged", (data: { tableId: number; status: string }) => {
      const { tableId, status } = data;

      queryClient.setQueriesData<Table[]>({ queryKey: ["tables"] }, (tables) => {
        if (!tables) return tables;
        return tables.map((t) => (t.id === tableId ? { ...t, status: statusMap[status] } : t));
      });
    });

    connection.start().then(() => connection.invoke("JoinLayout", layoutId));

    return () => {
      connection.invoke("LeaveLayout", layoutId).finally(() => connection.stop());
    };
  }, [layoutId, queryClient]);
};
