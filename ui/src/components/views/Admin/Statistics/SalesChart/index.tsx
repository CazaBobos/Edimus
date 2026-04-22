import colors from "@/common/colors.module.scss";
import { SalesPeriod } from "@/types/stats.types";
import { Bar, BarChart, CartesianGrid, ResponsiveContainer, Tooltip, XAxis, YAxis } from "recharts";

import styles from "./styles.module.scss";

type GroupBy = "day" | "week" | "month";

type SalesChartProps = {
  data: SalesPeriod[];
  isLoading: boolean;
  groupBy: GroupBy;
};

export const SalesChart = ({ data, isLoading, groupBy }: SalesChartProps) => {
  const chartData = data.map((s) => ({
    label: new Date(s.periodStart).toLocaleDateString("es-AR", {
      day: "2-digit",
      month: "short",
      ...(groupBy === "month" ? { month: "short", year: "2-digit" } : {}),
    }),
    revenue: s.revenue,
  }));

  return (
    <section className={styles.chartCard}>
      <h3>Ventas por período</h3>
      {isLoading ? (
        <div className={styles.placeholder}>Cargando…</div>
      ) : chartData.length === 0 ? (
        <div className={styles.placeholder}>Sin datos para el período seleccionado.</div>
      ) : (
        <ResponsiveContainer width="100%" height={240}>
          <BarChart data={chartData} margin={{ top: 8, right: 16, left: 0, bottom: 0 }}>
            <CartesianGrid strokeDasharray="3 3" stroke={colors.borderLight} />
            <XAxis dataKey="label" tick={{ fill: colors.lightGrey, fontSize: 11 }} />
            <YAxis tick={{ fill: colors.lightGrey, fontSize: 11 }} />
            <Tooltip
              contentStyle={{
                background: colors.tooltipBg,
                border: "1px solid rgba(255,255,255,0.1)",
                borderRadius: 6,
              }}
              labelStyle={{ color: colors.lightText }}
              formatter={(val) => [`$${Number(val).toFixed(2)}`, "Ventas"]}
            />
            <Bar dataKey="revenue" fill={colors.orange} radius={[4, 4, 0, 0]} />
          </BarChart>
        </ResponsiveContainer>
      )}
    </section>
  );
};
