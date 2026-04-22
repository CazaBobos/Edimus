import colors from "@/common/colors.module.scss";
import { useOccupancyQuery } from "@/hooks/queries/useStatsQueries";
import { useState } from "react";
import { Bar, BarChart, BarShapeProps, CartesianGrid, ResponsiveContainer, Tooltip, XAxis, YAxis } from "recharts";

import { Input } from "@/components/ui/Input";

import styles from "./styles.module.scss";

const today = () => new Date().toISOString().split("T")[0];

export const OccupancyChart = () => {
  const [date, setDate] = useState(today());
  const { data: occupancy = [], isLoading } = useOccupancyQuery(date);

  const chartData = occupancy.map((o) => ({
    label: `${o.hour}h`,
    rate: o.occupancyRate,
  }));

  return (
    <section className={styles.chartCard}>
      <div className={styles.chartHeader}>
        <h3>Ocupación por hora</h3>
        <Input type="date" title="Fecha" value={date} max={today()} onChange={({ value }) => setDate(value)} />
      </div>
      {isLoading ? (
        <div className={styles.placeholder}>Cargando…</div>
      ) : (
        <ResponsiveContainer width="100%" height={240}>
          <BarChart data={chartData} margin={{ top: 8, right: 16, left: 0, bottom: 0 }}>
            <CartesianGrid strokeDasharray="3 3" stroke={colors.borderLight} />
            <XAxis dataKey="label" tick={{ fill: colors.lightGrey, fontSize: 11 }} />
            <YAxis domain={[0, 100]} tickFormatter={(v) => `${v}%`} tick={{ fill: colors.lightGrey, fontSize: 11 }} />
            <Tooltip
              contentStyle={{
                background: colors.tooltipBg,
                border: "1px solid rgba(255,255,255,0.1)",
                borderRadius: 6,
              }}
              labelStyle={{ color: colors.lightText }}
              formatter={(val) => [`${Number(val)}%`, "Ocupación"]}
            />
            <Bar
              dataKey="rate"
              radius={[4, 4, 0, 0]}
              shape={(props: BarShapeProps) => {
                const { x, y, width, height, payload } = props;
                const rate = (payload as { rate: number }).rate;
                const fill = rate > 75 ? colors.red : rate > 40 ? colors.orange : colors.green;
                return (
                  <rect
                    x={x as number}
                    y={y as number}
                    width={width as number}
                    height={height as number}
                    fill={fill}
                    rx={4}
                    ry={4}
                  />
                );
              }}
            />
          </BarChart>
        </ResponsiveContainer>
      )}
    </section>
  );
};
