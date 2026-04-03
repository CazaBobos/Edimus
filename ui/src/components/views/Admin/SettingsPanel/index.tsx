"use client";

import { useCompanyMutations } from "@/hooks/mutations/useCompanyMutations";
import { useAppUserStore } from "@/stores";
import { UpdateCompanyRequest } from "@/types";
import { useMantineColorScheme } from "@mantine/core";
import { useState } from "react";
import { BiCog, BiSave } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { ControlState } from "@/components/ui/common";
import { Toggle } from "@/components/ui/Toggle";

import styles from "./styles.module.scss";

export const SettingsPanel = () => {
  const user = useAppUserStore((store) => store.user);
  const companyId = user?.companyIds[0] ?? 0;

  const { colorScheme, setColorScheme } = useMantineColorScheme();

  const [request, setRequest] = useState<UpdateCompanyRequest>({});

  const handleToggle = (event: ControlState) => {
    const { name, value } = event;

    setRequest((prev) => ({ ...prev, [name]: value === "true" }));
  };
  const { updateSettingsMutation } = useCompanyMutations();

  const handleSave = () => {
    updateSettingsMutation.mutate({ id: companyId, request });
  };

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
      </div>
    </div>
  );
};
