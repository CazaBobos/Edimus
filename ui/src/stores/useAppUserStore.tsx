import { LoginResponse } from "@/types";
import { create } from "zustand";
import { persist } from "zustand/middleware";

type AppUserStore = {
  user: LoginResponse | null;
  setUser: (user: LoginResponse) => void;
  endSession: () => void;
};

export const useAppUserStore = create<AppUserStore>()(
  persist(
    (set) => ({
      user: null,
      setUser: (user: LoginResponse) => set(() => ({ user: { ...user, created: new Date() } })),
      endSession: () => set(() => ({ user: null })),
    }),
    {
      name: "Edimus_UserData",
    },
  ),
);
