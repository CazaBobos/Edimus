import { useMenuStore } from "@/stores";
import { BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Dialog } from "@/components/ui/Dialog";

import styles from "./styles.module.scss";

export const FiltersDialog = () => {
  const tags = ["Carnes", "Pastas", "Pizza", "Postres", "Dulce", "Salado", "Sin Gluten", "Veggie"];

  const { isFiltersDialogOpen, setIsFiltersDialogOpen } = useMenuStore();

  const handleClose = () => setIsFiltersDialogOpen(false);

  return (
    <Dialog open={isFiltersDialogOpen} onClose={handleClose}>
      <div className={styles.content}>
        <div className={styles.header}>
          <h2>Filtros</h2>
          <BiX size={32} />
        </div>
        {tags.map((t) => (
          <div key={t} className={styles.tag}>
            <input name={t} type="checkbox" />
            <label htmlFor={t}>{t}</label>
          </div>
        ))}
        <Button label="Aplicar" />
      </div>
    </Dialog>
  );
};
