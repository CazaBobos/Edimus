import { LoginResponse } from "@/types";
import { create } from "zustand";
import { persist } from "zustand/middleware";

type AppUser = LoginResponse & {
  created: Date;
};

type AppUserStore = {
  user: AppUser | null;
  setUser: (user: LoginResponse) => void;
  endSession: () => void;
  isLoggedIn: () => boolean;
};

export const useAppUserStore = create<AppUserStore>()(
  persist(
    (set, get) => ({
      user: null,
      setUser: (user: LoginResponse) => set(() => ({ user: { ...user, created: new Date() } })),
      endSession: () => set(() => ({ user: null })),
      isLoggedIn: () => {
        const user = get().user;
        if (!user) return false;

        const sessionExpiration = new Date(user.created)?.getTime() + user.expiresIn * 60000;

        return new Date(sessionExpiration) > new Date();
      },
    }),
    {
      name: "Edimus_UserData",
    },
  ),
);
