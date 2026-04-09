import { statsApi } from "@/services";
import { SalesParams, StatsParams } from "@/types";

import { useAxiosQuery } from "../axiosHooks";

export const useOccupancyQuery = (date: string) =>
  useAxiosQuery({
    queryKey: ["stats", "occupancy", date],
    queryFn: () => statsApi.getOccupancy(date),
  });

export const useSalesQuery = (params: SalesParams) =>
  useAxiosQuery({
    queryKey: ["stats", "sales", params],
    queryFn: () => statsApi.getSales(params),
  });

export const useRotationQuery = (params: StatsParams) =>
  useAxiosQuery({
    queryKey: ["stats", "rotation", params],
    queryFn: () => statsApi.getRotation(params),
  });

export const useSpendingQuery = (params: StatsParams) =>
  useAxiosQuery({
    queryKey: ["stats", "spending", params],
    queryFn: () => statsApi.getSpending(params),
  });

export const useTopProductsQuery = (params: StatsParams & { limit?: number }) =>
  useAxiosQuery({
    queryKey: ["stats", "products", params],
    queryFn: () => statsApi.getTopProducts(params),
  });

export const useAttentionTimesQuery = (params: StatsParams) =>
  useAxiosQuery({
    queryKey: ["stats", "attention", params],
    queryFn: () => statsApi.getAttentionTimes(params),
  });
