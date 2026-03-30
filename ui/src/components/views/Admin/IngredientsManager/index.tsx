import { useIngredientsQuery } from "@/hooks/queries/useIngredientsQuery";
import { useAdminStore } from "@/stores";
import { useMemo, useState } from "react";
import { BiPlus, BiSearch } from "react-icons/bi";

import { Button } from "@/components/ui/Button";

import { IngredientDialog } from "./IngredientDialog";
import { getHealth, IngredientRow } from "./IngredientRow";
import styles from "./styles.module.scss";

type Filter = "all" | "ok" | "low" | "inactive";

export const IngredientsManager = () => {
  const { data: ingredients } = useIngredientsQuery();
  const { setIngredientDialogOpenState } = useAdminStore();

  const [search, setSearch] = useState("");
  const [filter, setFilter] = useState<Filter>("all");

  const counts = useMemo(
    () => ({
      all: ingredients.length,
      ok: ingredients.filter((i) => i.enabled && i.stock > i.alert).length,
      low: ingredients.filter((i) => i.enabled && i.stock <= i.alert).length,
      inactive: ingredients.filter((i) => !i.enabled).length,
    }),
    [ingredients],
  );

  const visible = useMemo(
    () =>
      ingredients.filter((ing) => {
        const health = getHealth(ing);
        const matchesFilter =
          filter === "all" ||
          (filter === "ok" && health === "ok") ||
          (filter === "low" && (health === "low" || health === "critical")) ||
          (filter === "inactive" && health === "inactive");

        return matchesFilter && ing.name.toLowerCase().includes(search.toLowerCase());
      }),
    [ingredients, filter, search],
  );

  const FILTERS: { key: Filter; label: string }[] = [
    { key: "all", label: "Todos" },
    { key: "ok", label: "En stock" },
    { key: "low", label: "Stock bajo" },
    { key: "inactive", label: "Inactivos" },
  ];

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <div className={styles.titleRow}>
          <h2>Ingredientes</h2>
          <div className={styles.stats}>
            <span className={styles.stat} data-type="ok">
              {counts.ok} en stock
            </span>
            <span className={styles.stat} data-type="low">
              {counts.low} bajo mínimo
            </span>
            <span className={styles.stat} data-type="inactive">
              {counts.inactive} inactivos
            </span>
          </div>
        </div>
        <div className={styles.toolbar}>
          <div className={styles.searchWrapper}>
            <BiSearch className={styles.searchIcon} size={14} />
            <input
              className={styles.searchInput}
              placeholder="Buscar ingrediente..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
            />
          </div>
          <Button
            variant="brand"
            label="Nuevo"
            icon={<BiPlus size={15} />}
            onClick={() => setIngredientDialogOpenState(null)}
          />
        </div>
        <div className={styles.filterTabs}>
          {FILTERS.map(({ key, label }) => (
            <Button
              key={key}
              variant="tab"
              label={label}
              active={filter === key}
              count={counts[key]}
              onClick={() => setFilter(key)}
            />
          ))}
        </div>
      </div>

      <div className={styles.list}>
        {visible.length === 0 ? (
          <div className={styles.empty}>
            {search ? `Sin resultados para "${search}"` : "No hay ingredientes en esta categoría"}
          </div>
        ) : (
          visible.map((ing) => (
            <IngredientRow key={ing.id} ingredient={ing} onClick={() => setIngredientDialogOpenState(ing)} />
          ))
        )}
      </div>

      <IngredientDialog />
    </div>
  );
};
