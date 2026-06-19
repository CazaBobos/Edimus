import { useTagsQuery } from "@/hooks/queries/useTagsQuery";
import { useMenuStore } from "@/stores";
import { useState } from "react";
import { BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Checkbox } from "@/components/ui/Checkbox";
import { Dialog } from "@/components/ui/Dialog";

import styles from "./styles.module.scss";

export const FiltersDialog = ({ companyId }: { companyId: number }) => {
  const { isFiltersDialogOpen, setIsFiltersDialogOpen, tagFilters, setTagFilters } = useMenuStore();
  const { data: tags } = useTagsQuery({ companyId, enabled: true });

  const [selectedTagIds, setSelectedTagIds] = useState<number[]>(tagFilters);

  const toggleTag = (id: number) =>
    setSelectedTagIds((prev) => (prev.includes(id) ? prev.filter((t) => t !== id) : [...prev, id]));

  const handleApply = () => {
    setTagFilters(selectedTagIds);
    setIsFiltersDialogOpen(false);
  };

  const handleClose = () => setIsFiltersDialogOpen(false);

  return (
    <Dialog open={isFiltersDialogOpen} onClose={handleClose}>
      <div className={styles.content}>
        <div className={styles.header}>
          <h2>Filtros</h2>
          <Button variant="subtle" icon={<BiX size={18} />} onClick={handleClose} />
        </div>
        <div className={styles.tagList}>
          {tags.map((t) => (
            <label key={t.id} className={styles.tag}>
              <Checkbox name={String(t.id)} checked={selectedTagIds.includes(t.id)} onChange={() => toggleTag(t.id)} />
              {t.name}
            </label>
          ))}
        </div>
        <Button label="Aplicar" onClick={handleApply} />
      </div>
    </Dialog>
  );
};
