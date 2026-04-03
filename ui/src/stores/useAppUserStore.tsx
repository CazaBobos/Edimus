import { LoginResponse } from "@/types";
import { create } from "zustand";
import { persist } from "zustand/middleware";

type AppUser = LoginResponse & { created: Date };

type AppUserStore = {
  user: AppUser | null;
  setUser: (user: LoginResponse) => void;
  renewSession: (expiresAt: number) => void;
  endSession: () => void;
  isLoggedIn: () => boolean;
};

export const useAppUserStore = create<AppUserStore>()(
  persist(
    (set, get) => ({
      user: null,
      setUser: (user: LoginResponse) =>
        set(() => ({
          user: { ...user, created: new Date() },
        })),
      renewSession: (expiresAt: number) =>
        set((state) => ({
          user: state.user
            ? {
                ...state.user,
                created: new Date(),
                refreshTokenExpiresAt: expiresAt,
              }
            : null,
        })),
      endSession: () => set(() => ({ user: null })),
      isLoggedIn: () => {
        const user = get().user;
        if (!user) return false;
        return user.refreshTokenExpiresAt > Date.now();
      },
    }),
    {
      name: "Edimus_UserData",
    },
  ),
);
