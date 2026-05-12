import { useTagMutations } from "@/hooks/mutations/useTagMutations";
import { useTagsQuery } from "@/hooks/queries/useTagsQuery";
import { Tag } from "@/types";
import { Drawer } from "@mantine/core";
import { useState } from "react";
import { BiCheck, BiPlus, BiSolidCircle, BiTrash, BiUpload, BiX } from "react-icons/bi";
import { MdOutlineEdit } from "react-icons/md";

import { Button } from "@/components/ui/Button";
import { Input } from "@/components/ui/Input";

import styles from "./styles.module.scss";

type Props = { opened: boolean; onClose: () => void };

export const TagsDrawer = ({ opened, onClose }: Props) => {
  const { data: tags } = useTagsQuery();
  const { createTagMutation, updateTagMutation, removeTagMutation, restoreTagMutation } = useTagMutations();

  const [newName, setNewName] = useState("");
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingName, setEditingName] = useState("");

  const handleCreate = () => {
    if (!newName.trim()) return;
    createTagMutation.mutate(newName.trim(), { onSuccess: () => setNewName("") });
  };

  const handleStartEdit = (tag: Tag) => {
    setEditingId(tag.id);
    setEditingName(tag.name);
  };

  const handleSaveEdit = () => {
    if (!editingId || !editingName.trim()) return;
    updateTagMutation.mutate(
      { id: editingId, name: editingName.trim() },
      {
        onSuccess: () => {
          setEditingId(null);
          setEditingName("");
        },
      },
    );
  };

  const handleCancelEdit = () => {
    setEditingId(null);
    setEditingName("");
  };

  return (
    <Drawer opened={opened} onClose={onClose} title="Etiquetas" position="right" size="sm" shadow="xl">
      <div className={styles.content}>
        <div className={styles.addRow}>
          <Input
            title=""
            name="newTag"
            placeholder="Nueva etiqueta..."
            value={newName}
            onChange={(s) => setNewName(s.value)}
          />
          <Button variant="action" icon={<BiPlus size={15} />} onClick={handleCreate} disabled={!newName.trim()} />
        </div>

        <div className={styles.list}>
          {tags.length === 0 && <p className={styles.empty}>No hay etiquetas aún.</p>}
          {tags.map((tag) => (
            <div key={tag.id} className={styles.tagRow}>
              {editingId === tag.id ? (
                <>
                  <input
                    className={styles.editInput}
                    value={editingName}
                    autoFocus
                    onChange={(e) => setEditingName(e.target.value)}
                    onKeyDown={(e) => {
                      if (e.key === "Enter") handleSaveEdit();
                      if (e.key === "Escape") handleCancelEdit();
                    }}
                  />
                  <Button variant="action" icon={<BiCheck size={14} />} onClick={handleSaveEdit} />
                  <Button variant="action" icon={<BiX size={14} />} onClick={handleCancelEdit} />
                </>
              ) : (
                <>
                  <BiSolidCircle className={styles.dot} data-enabled={tag.enabled} />
                  <span className={styles.tagName}>{tag.name}</span>
                  <Button variant="action" icon={<MdOutlineEdit size={14} />} onClick={() => handleStartEdit(tag)} />
                  {tag.enabled ? (
                    <Button
                      variant="action"
                      danger
                      icon={<BiTrash size={14} />}
                      onClick={() => removeTagMutation.mutate(tag.id)}
                    />
                  ) : (
                    <Button
                      variant="action"
                      icon={<BiUpload size={14} />}
                      onClick={() => restoreTagMutation.mutate(tag.id)}
                    />
                  )}
                </>
              )}
            </div>
          ))}
        </div>
      </div>
    </Drawer>
  );
};
