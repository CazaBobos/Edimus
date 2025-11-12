export type LoginResponse = {
  username: string;
  email: string;
  companyIds: number[];
  token: string;
  refreshToken: string;
  role: number;
  expiresIn: number;
  created: Date;
};

export type LoginRequest = {
  userOrEmail: string;
  password: string;
};
