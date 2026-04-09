"use client";

import {
  useAttentionTimesQuery,
  useRotationQuery,
  useSalesQuery,
  useSpendingQuery,
  useTopProductsQuery,
} from "@/hooks/queries/useStatsQueries";
import { useState } from "react";

import { KpiRow } from "./KpiRow";
import { OccupancyChart } from "./OccupancyChart";
import { RangeControls } from "./RangeControls";
import { SalesChart } from "./SalesChart";
import styles from "./styles.module.scss";
import { TopProductsChart } from "./TopProductsChart";

const today = () => new Date().toISOString().split("T")[0];
const daysAgo = (n: number) => {
  const d = new Date();
  d.setDate(d.getDate() - n);
  return d.toISOString().split("T")[0];
};

type GroupBy = "day" | "week" | "month";

export const Statistics = () => {
  const [rangeFrom, setRangeFrom] = useState(daysAgo(29));
  const [rangeTo, setRangeTo] = useState(today());
  const [groupBy, setGroupBy] = useState<GroupBy>("day");

  const rangeParams = { from: rangeFrom, to: rangeTo };

  const { data: sales = [], isLoading: loadingSales } = useSalesQuery({ ...rangeParams, groupBy });
  const { data: rotation, isLoading: loadingRotation } = useRotationQuery(rangeParams);
  const { data: spending, isLoading: loadingSpending } = useSpendingQuery(rangeParams);
  const { data: topProducts = [], isLoading: loadingProducts } = useTopProductsQuery({ ...rangeParams, limit: 10 });
  const { data: attention, isLoading: loadingAttention } = useAttentionTimesQuery(rangeParams);

  return (
    <div className={styles.container}>
      <h2 className={styles.title}>Estadísticas</h2>
      <KpiRow
        rotation={rotation}
        spending={spending}
        attention={attention}
        loadingRotation={loadingRotation}
        loadingSpending={loadingSpending}
        loadingAttention={loadingAttention}
      />
      <RangeControls
        rangeFrom={rangeFrom}
        rangeTo={rangeTo}
        groupBy={groupBy}
        onFromChange={setRangeFrom}
        onToChange={setRangeTo}
        onGroupByChange={setGroupBy}
      />
      <SalesChart data={sales} isLoading={loadingSales} groupBy={groupBy} />
      <OccupancyChart />
      <TopProductsChart data={topProducts} isLoading={loadingProducts} />
    </div>
  );
};
