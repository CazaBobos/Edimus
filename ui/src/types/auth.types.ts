export type LoginResponse = {
  username: string;
  email: string;
  companyIds: number[];
  role: number;
  tokenExpiresAt: number;
  refreshTokenExpiresAt: number;
};

export type LoginRequest = {
  userOrEmail: string;
  password: string;
};

export type ResetPasswordRequest = {
  userId: number;
  token: string;
  newPassword: string;
};
