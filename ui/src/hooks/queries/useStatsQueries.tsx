import { statsApi } from "@/services";
import { SalesParams, StatsParams } from "@/types";
import { useQuery } from "@tanstack/react-query";

export const useOccupancyQuery = (date: string) =>
  useQuery({
    queryKey: ["stats", "occupancy", date],
    queryFn: () => statsApi.getOccupancy(date),
  });

export const useSalesQuery = (params: SalesParams) =>
  useQuery({
    queryKey: ["stats", "sales", params],
    queryFn: () => statsApi.getSales(params),
  });

export const useRotationQuery = (params: StatsParams) =>
  useQuery({
    queryKey: ["stats", "rotation", params],
    queryFn: () => statsApi.getRotation(params),
  });

export const useSpendingQuery = (params: StatsParams) =>
  useQuery({
    queryKey: ["stats", "spending", params],
    queryFn: () => statsApi.getSpending(params),
  });

export const useTopProductsQuery = (params: StatsParams & { limit?: number }) =>
  useQuery({
    queryKey: ["stats", "products", params],
    queryFn: () => statsApi.getTopProducts(params),
  });

export const useAttentionTimesQuery = (params: StatsParams) =>
  useQuery({
    queryKey: ["stats", "attention", params],
    queryFn: () => statsApi.getAttentionTimes(params),
  });
