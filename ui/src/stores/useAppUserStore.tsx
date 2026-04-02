import { LoginResponse } from "@/types";
import { create } from "zustand";
import { persist } from "zustand/middleware";

const REFRESH_EXPIRY_MS = 7 * 24 * 60 * 60 * 1000; // = 7 Days

type AppUser = LoginResponse & {
  created: Date;
  refreshTokenExpiresAt: number;
};

type AppUserStore = {
  user: AppUser | null;
  setUser: (user: LoginResponse) => void;
  updateTokens: (token: string, refreshToken: string) => void;
  endSession: () => void;
  isLoggedIn: () => boolean;
};

export const useAppUserStore = create<AppUserStore>()(
  persist(
    (set, get) => ({
      user: null,
      setUser: (user: LoginResponse) =>
        set(() => ({
          user: {
            ...user,
            created: new Date(),
            refreshTokenExpiresAt: Date.now() + REFRESH_EXPIRY_MS,
          },
        })),
      updateTokens: (token: string, refreshToken: string) =>
        set((state) => ({
          user: state.user
            ? {
                ...state.user,
                token,
                refreshToken,
                created: new Date(),
                refreshTokenExpiresAt: Date.now() + REFRESH_EXPIRY_MS,
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
