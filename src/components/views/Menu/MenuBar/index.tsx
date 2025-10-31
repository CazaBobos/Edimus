import { useMenuStore } from "@/stores";
import { BiFilterAlt } from "react-icons/bi";
import { PiCallBell, PiBasket } from "react-icons/pi";

import { Button } from "@/components/ui/Button";

export const MenuBar = () => {
  const { setIsFiltersDialogOpen, setIsOrderDialogOpen } = useMenuStore();
  return (
    <>
      <Button icon={<BiFilterAlt size={32} />} onClick={() => setIsFiltersDialogOpen(true)} />
      <Button icon={<PiBasket size={32} />} onClick={() => setIsOrderDialogOpen(true)} />
      <Button icon={<PiCallBell size={32} />} />
    </>
  );
};
