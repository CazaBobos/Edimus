import { Company } from "@/types";

import { axiosClient } from "./axios";

export const companiesApi = {
  findOne: async (id: number) => {
    const response = await axiosClient.get<Company>(`companies/${id}`);

    return response.data;
  },
};
