import { useMenuMutations } from "@/hooks/mutations/useMenuMutations";
import { useSingleTableQuery } from "@/hooks/queries/useSingleTableQuery";
import { useMenuStore } from "@/stores";
import { TableStatus } from "@/types";
import { useState } from "react";
import { BiFilterAlt } from "react-icons/bi";
import { PiCallBell, PiBasket } from "react-icons/pi";

import { Button } from "@/components/ui/Button";

import styles from "./styles.module.scss";

type MenuBarProps = {
  publicOrders: boolean;
};
export const MenuBar = ({ publicOrders }: MenuBarProps) => {
  const { setIsFiltersDialogOpen, setIsOrderDialogOpen } = useMenuStore();

  const { data: table } = useSingleTableQuery();
  const { callTableMutation } = useMenuMutations();

  const [locked, setLocked] = useState<boolean>(false);

  const handleTableCall = () => {
    if (!table) return;

    setLocked(true);
    callTableMutation.mutate(table.id, { onSettled: () => setLocked(false) });
  };

  if (!table) return null;
  return (
    <div className={styles.fab}>
      <Button icon={<BiFilterAlt size={24} />} onClick={() => setIsFiltersDialogOpen(true)} />
      {publicOrders && <Button icon={<PiBasket size={24} />} onClick={() => setIsOrderDialogOpen(true)} />}
      <Button
        icon={<PiCallBell size={24} />}
        onClick={handleTableCall}
        disabled={table.status === TableStatus.Calling || locked}
      />
    </div>
  );
};
