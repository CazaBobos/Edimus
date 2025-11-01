import { useCategoriesQuery } from "@/hooks/queries/useCategoriesQuery";
import { useProductsQuery } from "@/hooks/queries/useProductsQuery";
import { useAdminStore } from "@/stores";
import { BiArrowBack, BiHistory, BiPlus, BiSave, BiTrash, BiX } from "react-icons/bi";

import { Button } from "@/components/ui/Button";
import { Dialog } from "@/components/ui/Dialog";

import styles from "./styles.module.scss";

export const ProductDialog = () => {
  const { productDialogOpenState, setProductDialogOpenState } = useAdminStore();

  const handleClose = () => setProductDialogOpenState(undefined);

  const { data: products } = useProductsQuery();
  const { data: categories } = useCategoriesQuery();

  const product = products.find((p) => p.id === productDialogOpenState);

  return (
    <Dialog open={productDialogOpenState !== undefined} onClose={handleClose}>
      <div className={styles.header}>
        {product ? (
          <>
            <h2>
              <BiArrowBack size={28} onClick={() => setProductDialogOpenState(0)} />
              Producto #{product.id}
            </h2>
            <div>
              <Button icon={<BiHistory size={28} />} />
              <Button icon={<BiTrash size={28} />} />
            </div>
          </>
        ) : (
          <h2>Nuevo Producto</h2>
        )}
      </div>
      <div className={styles.content}>
        <span>Nombre</span>
        <input type="text" value={product?.name} />

        <span>Descripción</span>
        <textarea value={product?.description} />

        <span>Categoría</span>
        <select value={product?.categoryId}>
          {categories.map((c) => (
            <option key={c.id} value={c.id}>
              {c.name}
            </option>
          ))}
        </select>
        <span>Precio ($)</span>
        <input type="text" value={product?.price} />
        <span>Etiquetas</span>
        <div
          style={{
            marginBottom: "16px",
            border: "1px solid #888",
            backgroundColor: "#323232",
            borderRadius: "2px",
            padding: "8px",
            display: "flex",
            gap: "8px",
          }}
        >
          {["Salado", "Veggie"].map((tag) => (
            <div
              key={tag}
              style={{
                borderRadius: "16px",
                border: "1px solid #f2f2f2",
                padding: "4px 8px",
                width: "fit-content",
                display: "flex",
                alignItems: "center",
                backgroundColor: "#0a0a0a",
              }}
            >
              {tag}
              <BiX size={24} />
            </div>
          ))}
        </div>
        <div className={styles.row}>
          <span>Variantes</span>
          <Button label="Añadir" icon={<BiPlus size={24} />} />
        </div>
        <ul>
          {product?.variants?.map((v) => (
            <li key={v.name}>
              <div className={styles.row}>
                <input type="text" value={v.name} />
                <div className={styles.row}>
                  $<input type="text" value={v.price} />
                  <BiX size={32} color="red" />
                </div>
              </div>
              <textarea value={v.description} />
            </li>
          ))}
        </ul>
        <div className={styles.row}>
          <span style={{ marginBottom: "16px" }}>Imagen: </span>
          <input type="file" name="" id="" />
        </div>
        <Button label="Guardar" icon={<BiSave size={24} />} />
      </div>
    </Dialog>
  );
};
