import { useProductMutations } from "@/hooks/mutations/useProductMutations";
import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useAdminStore } from "@/stores";
import { ProductRequest } from "@/types";
import { useState } from "react";
import { BiSave, BiTrash, BiUpload, BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { ControlState } from "@/components/ui/common";
import { Dialog } from "@/components/ui/Dialog";
import { Input } from "@/components/ui/Input";
import { Select } from "@/components/ui/Select";

import styles from "./styles.module.scss";

export const ProductDialog = () => {
  const { productDialogOpenState, setProductDialogOpenState } = useAdminStore();

  const handleClose = () => setProductDialogOpenState(undefined);

  const { data: products } = useProductsQuery();
  const { data: categories } = useCategoriesQuery();

  const product = products.find((p) => p.id === productDialogOpenState);
  const parentableProducts = products.filter((p) => !!p.categoryId);

  const { createProductMutation, updateProductMutation, removeProductMutation, restoreProductMutation } =
    useProductMutations();

  const handleEnable = () => {
    if (!product) return;

    if (product.enabled) removeProductMutation.mutate(product.id);
    else restoreProductMutation.mutate(product.id);
  };

  const [request, setRequest] = useState<ProductRequest>({});
  const handleChange = (state: ControlState) => {
    const { name, value } = state;

    setRequest((prev) => ({ ...prev, [name]: value }));
  };

  const isRequestValid = () => {
    const valid = true;
    if (!request.name || !request.price) return false;
    if (request.categoryId && request.parentId) return false;
    return valid;
  };

  const handleSave = () => {
    if (!!product) {
      updateProductMutation.mutate(
        { id: product.id, request },
        {
          onSuccess: handleClose,
        },
      );
    } else if (isRequestValid()) {
      createProductMutation.mutate(request as Required<ProductRequest>, {
        onSuccess: handleClose,
      });
    }
  };

  return (
    <Dialog open={productDialogOpenState !== undefined} onClose={handleClose}>
      <div className={styles.container}>
        <div className={styles.header}>
          {product ? (
            <h2>
              <span>Editar Producto</span>
              <BiX size={28} onClick={handleClose} />
            </h2>
          ) : (
            <h2>Nuevo Producto</h2>
          )}
        </div>
        <div className={styles.content}>
          <Input title="Nombre" name="name" defaultValue={product?.name} onChange={handleChange} />
          <Input
            multiline
            title="Descripción"
            name="description"
            defaultValue={product?.description}
            onChange={handleChange}
          />
          <Input title="Precio ($)" name="price" defaultValue={product?.price} onChange={handleChange} />
          <div className={styles.row}>
            <Select
              title="Categoría"
              name="categoryId"
              options={categories.map((c) => ({ value: c.id, label: c.name }))}
              defaultValue={product?.categoryId?.toString()}
              onChange={handleChange}
            />
            <Select
              title="Padre"
              name="parentId"
              options={parentableProducts.map((c) => ({ value: c.id, label: c.name }))}
              defaultValue={product?.parentId?.toString()}
              onChange={handleChange}
            />
          </div>
          <div className={styles.row}>
            {product && (
              <Button
                label={product.enabled ? "Eliminar" : "Restaurar"}
                icon={product.enabled ? <BiTrash /> : <BiUpload />}
                onClick={handleEnable}
              />
            )}
            <Button label="Guardar" icon={<BiSave />} disabled={!product && !isRequestValid()} onClick={handleSave} />
          </div>
        </div>
      </div>
    </Dialog>
  );
};
