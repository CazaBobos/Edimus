import { Ingredient, measurementUnitsMap } from "@/types";
import { BiChevronRight } from "react-icons/bi";

import styles from "./styles.module.scss";

export const getHealth = (ing: Ingredient) => {
  if (!ing.enabled) return "inactive";
  if (ing.stock === 0) return "critical";
  if (ing.stock <= ing.alert) return "low";
  return "ok";
};

type IngredientRowProps = {
  ingredient: Ingredient;
  onClick: () => void;
};

export const IngredientRow = ({ ingredient, onClick }: IngredientRowProps) => {
  const { name, stock, alert, unit } = ingredient;
  const health = getHealth(ingredient);
  const barPercent = alert > 0 ? Math.min((stock / (alert * 2)) * 100, 100) : stock > 0 ? 50 : 0;

  return (
    <div className={styles.row} data-health={health} onClick={onClick}>
      <div className={styles.healthDot} data-health={health} />
      <div className={styles.rowName}>{name}</div>
      <span className={styles.unitBadge}>{measurementUnitsMap[unit]}</span>
      <div className={styles.stockInfo}>
        <div className={styles.stockBar}>
          <div className={styles.stockBarFill} data-health={health} style={{ width: `${barPercent}%` }} />
          {alert > 0 && <div className={styles.alertLine} />}
        </div>
        <div className={styles.stockNumbers}>
          <span data-health={health}>{stock}</span>
          <span className={styles.alertRef}>/ mín. {alert}</span>
        </div>
      </div>
      <BiChevronRight className={styles.chevron} size={18} />
    </div>
  );
};
