export type HourlyOccupancy = {
  hour: number;
  occupancyRate: number;
};

export type SalesPeriod = {
  periodStart: string;
  revenue: number;
};

export type TopProduct = {
  productId: number;
  productName: string;
  totalAmount: number;
  totalRevenue: number;
};

export type AttentionTimes = {
  averageArrivalSeconds: number | null;
  averageCallingSeconds: number | null;
};

export type StatsParams = {
  from: string;
  to: string;
};

export type SalesParams = StatsParams & {
  groupBy?: "day" | "week" | "month";
};

export type RotationData = { averageMinutes: number | null };

export type SpendingData = { averagePerSession: number | null };
