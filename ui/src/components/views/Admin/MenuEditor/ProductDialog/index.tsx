import { useProductMutations } from "@/hooks/mutations/useProductMutations";
import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useIngredientsQuery } from "@/hooks/queries/useIngredientsQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useAdminStore } from "@/stores";
import { measurementUnitsMap, ProductRequest } from "@/types";
import { Drawer } from "@mantine/core";
import { useEffect, useState } from "react";
import { BiPlus, BiSave, BiTrash, BiUpload, BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Checkbox } from "@/components/ui/Checkbox";
import { ControlState } from "@/components/ui/common";
import { Input } from "@/components/ui/Input";
import { Select } from "@/components/ui/Select";

import styles from "./styles.module.scss";

export const ProductDialog = () => {
  const product = useAdminStore((store) => store.productDialogOpenState);
  const setProductDialogOpenState = useAdminStore((store) => store.setProductDialogOpenState);

  const handleClose = () => {
    setRequest({});
    setNewIngredientId("-1");
    setNewAmount("");
    setProductDialogOpenState(undefined);
  };

  const { data: products } = useProductsQuery();
  const { data: categories } = useCategoriesQuery();
  const { data: ingredients, ingredientsMap } = useIngredientsQuery({ enabled: true });

  const parentableProducts = products.filter((p) => !!p.categoryId);

  const { createProductMutation, updateProductMutation, removeProductMutation, restoreProductMutation } =
    useProductMutations();

  const handleEnable = () => {
    if (!product) return;
    if (product.enabled) removeProductMutation.mutate(product.id);
    else restoreProductMutation.mutate(product.id);
    handleClose();
  };

  const [request, setRequest] = useState<ProductRequest>({});
  const handleChange = (state: ControlState) => {
    const { name, value } = state;
    const newState: ProductRequest = {
      [name]: ["name", "description"].includes(name) ? value : Number(value),
    };
    setRequest((prev) => ({ ...prev, ...newState }));
  };

  const isRequestValid = () => {
    if (!request.name || !request.price) return false;
    if (request.categoryId && request.parentId) return false;
    return true;
  };

  const handleSave = () => {
    const filteredRequest: ProductRequest = isVariant
      ? { ...request, categoryId: undefined }
      : { ...request, parentId: undefined };

    if (product) {
      updateProductMutation.mutate({ id: product.id, request: filteredRequest }, { onSuccess: handleClose });
    } else if (isRequestValid()) {
      createProductMutation.mutate(filteredRequest as Required<ProductRequest>, { onSuccess: handleClose });
    }
  };

  const [isVariant, setIsVariant] = useState<boolean>(false);
  useEffect(() => {
    setIsVariant(!!product?.parentId);
    setRequest((prev) => ({ ...prev, consumptions: product?.consumptions ?? [] }));
  }, [product]);

  // ─── Consumptions ───────────────────────────────────────────
  const [newIngredientId, setNewIngredientId] = useState<string>("-1");
  const [newAmount, setNewAmount] = useState<string>("");

  const handleAddConsumption = () => {
    const ingredientId = Number(newIngredientId);
    const amount = Number(newAmount);
    if (!ingredientId || !amount || amount <= 0) return;
    if (request.consumptions?.some((c) => c.ingredientId === ingredientId)) return;

    setRequest((prev) => ({ ...prev, consumptions: [...(prev.consumptions ?? []), { ingredientId, amount }] }));
    setNewIngredientId("-1");
    setNewAmount("");
  };

  const handleRemoveConsumption = (ingredientId: number) => {
    setRequest((prev) => ({
      ...prev,
      consumptions: prev.consumptions?.filter((c) => c.ingredientId !== ingredientId),
    }));
  };

  const availableIngredients = ingredients.filter((i) => !request.consumptions?.some((c) => c.ingredientId === i.id));
  const hasIncompleteConsumption = (newIngredientId !== "-1") !== (newAmount !== "");

  const title = product
    ? product.parentId
      ? `Variante: ${product.name}`
      : `Producto: ${product.name}`
    : "Nuevo Producto";

  return (
    <Drawer
      opened={product !== undefined}
      onClose={handleClose}
      title={title}
      position="right"
      size="md"
      withOverlay={false}
      shadow="xl"
    >
      <div className={styles.content}>
        <Input title="Nombre" name="name" defaultValue={product?.name} onChange={handleChange} />
        <Input
          multiline
          title="Descripción"
          name="description"
          defaultValue={product?.description}
          onChange={handleChange}
        />
        <div className={styles.row}>
          <Input title="Precio ($)" name="price" type="number" defaultValue={product?.price} onChange={handleChange} />
          <div className={styles.checkboxWrapper}>
            <Checkbox checked={isVariant} title="Es variante" onChange={() => setIsVariant(!isVariant)} />
          </div>
        </div>
        <div className={styles.row}>
          <Select
            title="Categoría"
            name="categoryId"
            options={categories.map((c) => ({ value: c.id, label: c.name }))}
            defaultValue={product?.categoryId?.toString()}
            onChange={handleChange}
            disabled={isVariant}
          />
          <Select
            title="Producto padre"
            name="parentId"
            options={parentableProducts.map((c) => ({ value: c.id, label: c.name }))}
            defaultValue={product?.parentId?.toString()}
            onChange={handleChange}
            disabled={!isVariant}
          />
        </div>

        {product && (
          <div className={styles.consumptionsSection}>
            <p className={styles.sectionLabel}>Ingredientes</p>
            {(request.consumptions?.length ?? 0) > 0 && (
              <div className={styles.consumptionList}>
                {request.consumptions?.map((c) => {
                  const ingredient = ingredientsMap?.get(c.ingredientId);

                  return (
                    <div key={c.ingredientId} className={styles.consumptionRow}>
                      <span className={styles.consumptionName}>{ingredient?.name ?? `#${c.ingredientId}`}</span>
                      <span className={styles.consumptionUnit}>{measurementUnitsMap[ingredient?.unit ?? 0]}</span>
                      <span className={styles.consumptionAmount}>{c.amount}</span>
                      <button
                        className={styles.consumptionRemove}
                        onClick={() => handleRemoveConsumption(c.ingredientId)}
                      >
                        <BiX size={14} />
                      </button>
                    </div>
                  );
                })}
              </div>
            )}
            <div className={styles.consumptionAdd}>
              <div className={styles.consumptionAddSelects}>
                <Select
                  title=""
                  name="newIngredient"
                  options={availableIngredients.map((i) => ({ value: i.id, label: i.name }))}
                  value={newIngredientId}
                  onChange={(s) => setNewIngredientId(s.value)}
                />
                <Input
                  title=""
                  name="newAmount"
                  type="number"
                  value={newAmount}
                  onChange={(s) => setNewAmount(s.value)}
                  placeholder="Cant."
                />
              </div>
              <Button variant="action" icon={<BiPlus size={15} />} onClick={handleAddConsumption} />
            </div>
          </div>
        )}

        <div className={styles.actions}>
          <Button
            label="Guardar Cambios"
            icon={<BiSave />}
            disabled={(!product && !isRequestValid()) || hasIncompleteConsumption}
            onClick={handleSave}
          />
          {product && (
            <Button
              label={product.enabled ? "Deshabilitar" : "Restaurar"}
              icon={product.enabled ? <BiTrash /> : <BiUpload />}
              onClick={handleEnable}
            />
          )}
        </div>
      </div>
    </Drawer>
  );
};
