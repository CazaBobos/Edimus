import { AttentionTimes, RotationData, SpendingData } from "@/types/stats.types";

import styles from "./styles.module.scss";

const formatMinutes = (mins: number | null | undefined) => {
  if (mins == null) return "—";
  const h = Math.floor(mins / 60);
  const m = Math.round(mins % 60);
  return h > 0 ? `${h}h ${m}m` : `${m}m`;
};

const formatSeconds = (secs: number | null | undefined) => {
  if (secs == null) return "—";
  const m = Math.floor(secs / 60);
  const s = Math.round(secs % 60);
  return m > 0 ? `${m}m ${s}s` : `${s}s`;
};

type KpiRowProps = {
  rotation: RotationData | undefined;
  spending: SpendingData | undefined;
  attention: AttentionTimes | undefined;
  loadingRotation: boolean;
  loadingSpending: boolean;
  loadingAttention: boolean;
};

export const KpiRow = ({
  rotation,
  spending,
  attention,
  loadingRotation,
  loadingSpending,
  loadingAttention,
}: KpiRowProps) => (
  <section className={styles.kpiRow}>
    <div className={styles.kpiCard}>
      <span className={styles.kpiLabel}>Rotación promedio</span>
      <span className={styles.kpiValue}>{loadingRotation ? "…" : formatMinutes(rotation?.averageMinutes)}</span>
    </div>
    <div className={styles.kpiCard}>
      <span className={styles.kpiLabel}>Gasto promedio por mesa</span>
      <span className={styles.kpiValue}>
        {loadingSpending
          ? "…"
          : spending?.averagePerSession != null
            ? `$${spending.averagePerSession.toFixed(2)}`
            : "—"}
      </span>
    </div>
    <div className={styles.kpiCard}>
      <span className={styles.kpiLabel}>Tiempo de atención (llegada)</span>
      <span className={styles.kpiValue}>
        {loadingAttention ? "…" : formatSeconds(attention?.averageArrivalSeconds)}
      </span>
    </div>
    <div className={styles.kpiCard}>
      <span className={styles.kpiLabel}>Tiempo de atención (llamado)</span>
      <span className={styles.kpiValue}>
        {loadingAttention ? "…" : formatSeconds(attention?.averageCallingSeconds)}
      </span>
    </div>
  </section>
);
