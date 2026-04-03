export type LoginResponse = {
  username: string;
  email: string;
  companyIds?: number[];
  token: string;
  refreshToken: string;
  role: number;
  expiresIn: number;
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
