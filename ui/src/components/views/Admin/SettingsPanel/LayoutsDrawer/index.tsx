import { useLayoutMutations } from "@/hooks/mutations/useLayoutMutations";
import { Layout } from "@/types";
import { Drawer } from "@mantine/core";
import { useState } from "react";
import { BiCheck, BiPlus, BiSolidCircle, BiTrash, BiUpload, BiX } from "react-icons/bi";
import { MdOutlineEdit } from "react-icons/md";

import { Button } from "@/components/ui/Button";
import { Input } from "@/components/ui/Input";

import styles from "./styles.module.scss";

type Props = {
  premiseId: number | null;
  layouts: Layout[];
  opened: boolean;
  onClose: () => void;
};

type EditState = { name: string; height: string; width: string };

export const LayoutsDrawer = ({ premiseId, layouts, opened, onClose }: Props) => {
  const { createLayoutMutation, updateLayoutMutation, removeLayoutMutation, restoreLayoutMutation } =
    useLayoutMutations();

  const [newName, setNewName] = useState("");
  const [newHeight, setNewHeight] = useState("");
  const [newWidth, setNewWidth] = useState("");

  const [editingId, setEditingId] = useState<number | null>(null);
  const [editState, setEditState] = useState<EditState>({ name: "", height: "", width: "" });

  const canCreate = newName.trim() && Number(newHeight) > 0 && Number(newWidth) > 0;

  const handleCreate = () => {
    if (!canCreate || !premiseId) return;
    createLayoutMutation.mutate(
      { premiseId, name: newName.trim(), height: Number(newHeight), width: Number(newWidth) },
      {
        onSuccess: () => {
          setNewName("");
          setNewHeight("");
          setNewWidth("");
        },
      },
    );
  };

  const handleStartEdit = (layout: Layout) => {
    setEditingId(layout.id);
    setEditState({ name: layout.name, height: String(layout.height), width: String(layout.width) });
  };

  const handleSaveEdit = () => {
    if (!editingId || !editState.name.trim()) return;
    updateLayoutMutation.mutate(
      {
        id: editingId,
        name: editState.name.trim(),
        height: Number(editState.height) || undefined,
        width: Number(editState.width) || undefined,
      },
      {
        onSuccess: () => {
          setEditingId(null);
          setEditState({ name: "", height: "", width: "" });
        },
      },
    );
  };

  const handleCancelEdit = () => {
    setEditingId(null);
    setEditState({ name: "", height: "", width: "" });
  };

  return (
    <Drawer opened={opened} onClose={onClose} title="Planos" position="right" size="md" shadow="xl">
      <div className={styles.content}>
        <div className={styles.addRow}>
          <Input
            title=""
            name="newLayoutName"
            placeholder="Nombre..."
            value={newName}
            onChange={(s) => setNewName(s.value)}
          />
          <input
            type="number"
            className={styles.dimInput}
            placeholder="Alto"
            value={newHeight}
            min={1}
            onChange={(e) => setNewHeight(e.target.value)}
          />
          <input
            type="number"
            className={styles.dimInput}
            placeholder="Ancho"
            value={newWidth}
            min={1}
            onChange={(e) => setNewWidth(e.target.value)}
          />
          <Button variant="action" icon={<BiPlus size={15} />} onClick={handleCreate} disabled={!canCreate} />
        </div>

        <div className={styles.list}>
          {layouts.length === 0 && <p className={styles.empty}>No hay planos aún.</p>}
          {layouts.map((layout) => (
            <div key={layout.id} className={styles.layoutRow}>
              {editingId === layout.id ? (
                <>
                  <input
                    className={styles.editInput}
                    value={editState.name}
                    autoFocus
                    onChange={(e) => setEditState((s) => ({ ...s, name: e.target.value }))}
                    onKeyDown={(e) => {
                      if (e.key === "Enter") handleSaveEdit();
                      if (e.key === "Escape") handleCancelEdit();
                    }}
                  />
                  <input
                    type="number"
                    className={styles.editDim}
                    value={editState.height}
                    min={1}
                    onChange={(e) => setEditState((s) => ({ ...s, height: e.target.value }))}
                  />
                  <input
                    type="number"
                    className={styles.editDim}
                    value={editState.width}
                    min={1}
                    onChange={(e) => setEditState((s) => ({ ...s, width: e.target.value }))}
                  />
                  <Button variant="action" icon={<BiCheck size={14} />} onClick={handleSaveEdit} />
                  <Button variant="action" icon={<BiX size={14} />} onClick={handleCancelEdit} />
                </>
              ) : (
                <>
                  <BiSolidCircle className={styles.dot} data-enabled={layout.enabled} />
                  <span className={styles.layoutName}>{layout.name}</span>
                  <span className={styles.dims}>
                    {layout.height}×{layout.width}
                  </span>
                  <Button variant="action" icon={<MdOutlineEdit size={14} />} onClick={() => handleStartEdit(layout)} />
                  {layout.enabled ? (
                    <Button
                      variant="action"
                      danger
                      icon={<BiTrash size={14} />}
                      onClick={() => removeLayoutMutation.mutate(layout.id)}
                    />
                  ) : (
                    <Button
                      variant="action"
                      icon={<BiUpload size={14} />}
                      onClick={() => restoreLayoutMutation.mutate(layout.id)}
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
