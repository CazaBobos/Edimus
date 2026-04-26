import { useMenuStore } from "@/stores";
import { useState } from "react";
import { BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Checkbox } from "@/components/ui/Checkbox";
import { Dialog } from "@/components/ui/Dialog";

import styles from "./styles.module.scss";

const TAGS = ["Carnes", "Pastas", "Pizza", "Postres", "Dulce", "Salado", "Sin Gluten", "Veggie"];

export const FiltersDialog = () => {
  const { isFiltersDialogOpen, setIsFiltersDialogOpen } = useMenuStore();
  const handleClose = () => setIsFiltersDialogOpen(false);

  const [selectedTags, setSelectedTags] = useState<string[]>([]);
  const toggleTag = (tag: string) =>
    setSelectedTags((prev) => (prev.includes(tag) ? prev.filter((t) => t !== tag) : [...prev, tag]));

  return (
    <Dialog open={isFiltersDialogOpen} onClose={handleClose}>
      <div className={styles.content}>
        <div className={styles.header}>
          <h2>Filtros</h2>
          <Button variant="subtle" icon={<BiX size={18} />} onClick={handleClose} />
        </div>
        <div className={styles.tagList}>
          {TAGS.map((t) => (
            <label key={t} className={styles.tag}>
              <Checkbox name={t} checked={selectedTags.includes(t)} onChange={() => toggleTag(t)} />
              {t}
            </label>
          ))}
        </div>
        <Button label="Aplicar" onClick={handleClose} />
      </div>
    </Dialog>
  );
};
