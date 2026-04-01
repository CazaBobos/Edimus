import { useAdminStore } from "@/stores";
import { useRouter, useSearchParams } from "next/navigation";
import { useEffect, useRef } from "react";

export const useUrlSync = () => {
  const router = useRouter();
  const searchParams = useSearchParams();
  const selectedTab = useAdminStore((store) => store.headerPanelState.selectedTab);
  const setHeaderPanelState = useAdminStore((store) => store.setHeaderPanelState);
  const seeded = useRef(false);

  // Seed store from URL once on mount (client-only)
  useEffect(() => {
    if (seeded.current) return;
    seeded.current = true;

    const tab = searchParams.get("tab");
    if (tab !== null) {
      setHeaderPanelState({ selectedTab: parseInt(tab) });
    }
  }, [searchParams, setHeaderPanelState]);

  // Keep URL in sync with store
  useEffect(() => {
    if (!seeded.current) return;

    const params = new URLSearchParams(searchParams.toString());
    params.set("tab", String(selectedTab));
    router.replace(`?${params.toString()}`);
  }, [selectedTab, router, searchParams]);
};
