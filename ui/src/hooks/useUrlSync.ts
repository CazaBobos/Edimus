import { useAdminStore } from "@/stores";
import { useRouter, useSearchParams } from "next/navigation";
import { useEffect } from "react";

export const useUrlSync = () => {
  const router = useRouter();
  const searchParams = useSearchParams();
  const selectedTab = useAdminStore((store) => store.headerPanelState.selectedTab);

  useEffect(() => {
    const params = new URLSearchParams(searchParams.toString());
    params.set("tab", String(selectedTab));
    router.replace(`?${params.toString()}`);
  }, [selectedTab]);
};
