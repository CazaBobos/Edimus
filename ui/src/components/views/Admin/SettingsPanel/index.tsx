"use client";

import { useCompanyMutations } from "@/hooks/mutations/useCompanyMutations";
import { usePremiseMutations } from "@/hooks/mutations/usePremiseMutations";
import { useCompanyQuery } from "@/hooks/queries/useCompanyQuery";
import { useAppUserStore } from "@/stores";
import { Premise, UpdateCompanyRequest } from "@/types";
import { useMantineColorScheme } from "@mantine/core";
import { useState } from "react";
import { BiCheck, BiCog, BiPlus, BiSave, BiSolidCircle, BiTrash, BiUpload, BiX } from "react-icons/bi";
import { MdOutlineEdit } from "react-icons/md";
import { TbLayoutGrid } from "react-icons/tb";

import { Button } from "@/components/ui/Button";
import { ControlState } from "@/components/ui/common";
import { Input } from "@/components/ui/Input";
import { Toggle } from "@/components/ui/Toggle";

import { LayoutsDrawer } from "./LayoutsDrawer";
import styles from "./styles.module.scss";

export const SettingsPanel = () => {
  const { colorScheme, setColorScheme } = useMantineColorScheme();

  const [request, setRequest] = useState<UpdateCompanyRequest>({});

  const handleToggle = (event: ControlState) => {
    const { name, value } = event;
    setRequest((prev) => ({ ...prev, [name]: value === "true" }));
  };

  const handleSetRequest = (event: ControlState) => {
    const { name, value } = event;
    setRequest((prev) => ({ ...prev, [name]: value }));
  };

  const { updateSettingsMutation } = useCompanyMutations();

  const handleSave = () => {
    if (!company) return;

    updateSettingsMutation.mutate({ id: company.id, request });
  };

  // Premises
  const { data: company } = useCompanyQuery();
  const premises = company?.premises ?? [];

  const { createPremiseMutation, updatePremiseMutation, removePremiseMutation, restorePremiseMutation } =
    usePremiseMutations();

  const [newPremiseName, setNewPremiseName] = useState("");
  const [editingPremiseId, setEditingPremiseId] = useState<number | null>(null);
  const [editingPremiseName, setEditingPremiseName] = useState("");
  const [layoutsPremiseId, setLayoutsPremiseId] = useState<number | null>(null);

  const handleCreatePremise = () => {
    if (!newPremiseName.trim() || !company) return;
    createPremiseMutation.mutate(
      { companyId: company.id, name: newPremiseName.trim() },
      { onSuccess: () => setNewPremiseName("") },
    );
  };

  const handleStartEditPremise = (premise: Premise) => {
    setEditingPremiseId(premise.id);
    setEditingPremiseName(premise.name);
  };

  const handleSaveEditPremise = () => {
    if (!editingPremiseId || !editingPremiseName.trim()) return;
    updatePremiseMutation.mutate(
      { id: editingPremiseId, name: editingPremiseName.trim() },
      {
        onSuccess: () => {
          setEditingPremiseId(null);
          setEditingPremiseName("");
        },
      },
    );
  };

  const handleCancelEditPremise = () => {
    setEditingPremiseId(null);
    setEditingPremiseName("");
  };

  const selectedPremise = premises.find((p) => p.id === layoutsPremiseId);

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <div className={styles.titleRow}>
          <BiCog size={20} />
          <h2>Configuración</h2>
          <Button variant="brand" label="Guardar" icon={<BiSave />} onClick={handleSave} />
        </div>
      </div>

      <div className={styles.content}>
        <section className={styles.section}>
          <h3 className={styles.sectionTitle}>General</h3>
          <div className={styles.settingRow}>
            <div className={styles.settingInfo}>
              <span className={styles.settingLabel}>Slug</span>
              <span className={styles.settingDescription}>
                Identificador único del local en la URL del menú público. Se genera automáticamente si no se especifica.
              </span>
            </div>
            <Input name="slug" defaultValue={company?.slug} onChange={handleSetRequest} />
          </div>
        </section>
        <section className={styles.section}>
          <h3 className={styles.sectionTitle}>Apariencia</h3>
          <div className={styles.settingRow}>
            <div className={styles.settingInfo}>
              <span className={styles.settingLabel}>Esquema de color</span>
              <span className={styles.settingDescription}>Define el tema visual del panel de administración.</span>
            </div>
            <div className={styles.schemeToggle}>
              <button
                className={styles.schemeOption}
                data-active={colorScheme === "light"}
                onClick={() => setColorScheme("light")}
              >
                Claro
              </button>
              <button
                className={styles.schemeOption}
                data-active={colorScheme === "dark"}
                onClick={() => setColorScheme("dark")}
              >
                Oscuro
              </button>
            </div>
          </div>
        </section>

        <section className={styles.section}>
          <h3 className={styles.sectionTitle}>Inventario</h3>
          <div className={styles.settingRow}>
            <div className={styles.settingInfo}>
              <span className={styles.settingLabel}>Descuento automático de stock</span>
              <span className={styles.settingDescription}>
                Al confirmar un pedido, los ingredientes se descuentan automáticamente del inventario.
              </span>
            </div>
            <Toggle name="reactiveStock" checked={request.reactiveStock} onChange={handleToggle} />
          </div>
        </section>

        <section className={styles.section}>
          <h3 className={styles.sectionTitle}>Menú</h3>
          <div className={styles.settingRow}>
            <div className={styles.settingInfo}>
              <span className={styles.settingLabel}>Mostrar precios sin mesa vinculada</span>
              <span className={styles.settingDescription}>
                Los clientes pueden ver los precios aunque no estén asociados a ninguna mesa.
              </span>
            </div>
            <Toggle name="publicPrices" checked={request.publicPrices} onChange={handleToggle} />
          </div>
          <div className={styles.settingRow}>
            <div className={styles.settingInfo}>
              <span className={styles.settingLabel}>Historial de pedidos para clientes</span>
              <span className={styles.settingDescription}>
                Los clientes pueden consultar los pedidos que realizaron desde el menú.
              </span>
            </div>
            <Toggle name="publicOrders" checked={request.publicOrders} onChange={handleToggle} />
          </div>
        </section>

        <section className={styles.section}>
          <h3 className={styles.sectionTitle}>Locales</h3>
          <div className={styles.entityAddRow}>
            <Input
              title=""
              name="newPremise"
              placeholder="Nuevo local..."
              value={newPremiseName}
              onChange={(s) => setNewPremiseName(s.value)}
            />
            <Button
              variant="action"
              icon={<BiPlus size={15} />}
              onClick={handleCreatePremise}
              disabled={!newPremiseName.trim()}
            />
          </div>
          <div className={styles.entityList}>
            {premises.length === 0 && <p className={styles.entityEmpty}>No hay locales aún.</p>}
            {premises.map((premise) => (
              <div key={premise.id} className={styles.entityRow}>
                {editingPremiseId === premise.id ? (
                  <>
                    <input
                      className={styles.entityEditInput}
                      value={editingPremiseName}
                      autoFocus
                      onChange={(e) => setEditingPremiseName(e.target.value)}
                      onKeyDown={(e) => {
                        if (e.key === "Enter") handleSaveEditPremise();
                        if (e.key === "Escape") handleCancelEditPremise();
                      }}
                    />
                    <Button variant="action" icon={<BiCheck size={14} />} onClick={handleSaveEditPremise} />
                    <Button variant="action" icon={<BiX size={14} />} onClick={handleCancelEditPremise} />
                  </>
                ) : (
                  <>
                    <BiSolidCircle className={styles.entityDot} data-enabled={premise.enabled} />
                    <span className={styles.entityName}>{premise.name}</span>
                    <Button
                      variant="action"
                      icon={<TbLayoutGrid size={14} />}
                      title="Planos"
                      onClick={() => setLayoutsPremiseId(premise.id)}
                    />
                    <Button
                      variant="action"
                      icon={<MdOutlineEdit size={14} />}
                      onClick={() => handleStartEditPremise(premise)}
                    />
                    {premise.enabled ? (
                      <Button
                        variant="action"
                        danger
                        icon={<BiTrash size={14} />}
                        onClick={() => removePremiseMutation.mutate(premise.id)}
                      />
                    ) : (
                      <Button
                        variant="action"
                        icon={<BiUpload size={14} />}
                        onClick={() => restorePremiseMutation.mutate(premise.id)}
                      />
                    )}
                  </>
                )}
              </div>
            ))}
          </div>
        </section>
      </div>

      <LayoutsDrawer
        premiseId={layoutsPremiseId}
        layouts={selectedPremise?.layouts ?? []}
        opened={layoutsPremiseId !== null}
        onClose={() => setLayoutsPremiseId(null)}
      />
    </div>
  );
};
