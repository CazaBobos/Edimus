import colors from "@/common/colors.module.scss";
import { TopProduct } from "@/types/stats.types";
import { Bar, BarChart, CartesianGrid, ResponsiveContainer, Tooltip, XAxis, YAxis } from "recharts";

import styles from "./styles.module.scss";

type TopProductsChartProps = {
  data: TopProduct[];
  isLoading: boolean;
};

export const TopProductsChart = ({ data, isLoading }: TopProductsChartProps) => {
  const chartData = data.map((p) => ({
    label: p.productName.length > 14 ? p.productName.slice(0, 13) + "…" : p.productName,
    amount: p.totalAmount,
    revenue: p.totalRevenue,
  }));

  return (
    <section className={styles.chartCard}>
      <h3>Productos más pedidos</h3>
      {isLoading ? (
        <div className={styles.placeholder}>Cargando…</div>
      ) : chartData.length === 0 ? (
        <div className={styles.placeholder}>Sin datos para el período seleccionado.</div>
      ) : (
        <ResponsiveContainer width="100%" height={260}>
          <BarChart data={chartData} layout="vertical" margin={{ top: 8, right: 48, left: 8, bottom: 0 }}>
            <CartesianGrid strokeDasharray="3 3" stroke={colors.borderLight} horizontal={false} />
            <XAxis type="number" tick={{ fill: colors.lightGrey, fontSize: 11 }} />
            <YAxis type="category" dataKey="label" width={110} tick={{ fill: colors.lightGrey, fontSize: 11 }} />
            <Tooltip
              contentStyle={{
                background: colors.tooltipBg,
                border: "1px solid rgba(255,255,255,0.1)",
                borderRadius: 6,
              }}
              labelStyle={{ color: colors.lightText }}
              formatter={(val) => [Number(val), "Unidades"]}
            />
            <Bar dataKey="amount" fill={colors.blue} radius={[0, 4, 4, 0]} />
          </BarChart>
        </ResponsiveContainer>
      )}
    </section>
  );
};
