import {
  AttentionTimes,
  HourlyOccupancy,
  SalesPeriod,
  SalesParams,
  StatsParams,
  TopProduct,
  RotationData,
  SpendingData,
} from "@/types";

import { axiosClient } from "./axios";

export const statsApi = {
  getOccupancy: async (date: string) => {
    const response = await axiosClient.get<HourlyOccupancy[]>("stats/occupancy", { params: { date } });
    return response.data;
  },
  getSales: async (params: SalesParams) => {
    const response = await axiosClient.get<SalesPeriod[]>("stats/sales", { params });
    return response.data;
  },
  getRotation: async (params: StatsParams) => {
    const response = await axiosClient.get<RotationData>("stats/rotation", { params });
    return response.data;
  },
  getSpending: async (params: StatsParams) => {
    const response = await axiosClient.get<SpendingData>("stats/spending", { params });
    return response.data;
  },
  getTopProducts: async (params: StatsParams & { limit?: number }) => {
    const response = await axiosClient.get<TopProduct[]>("stats/products", { params });
    return response.data;
  },
  getAttentionTimes: async (params: StatsParams) => {
    const response = await axiosClient.get<AttentionTimes>("stats/attention", { params });
    return response.data;
  },
};
