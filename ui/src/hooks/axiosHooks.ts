import { ExceptionResponse } from "@/services";
import { useQuery, useMutation } from "@tanstack/react-query";
import type { UseQueryOptions, UseMutationOptions, UseQueryResult, UseMutationResult } from "@tanstack/react-query";
import type { AxiosError } from "axios";

export type AxiosException = AxiosError<ExceptionResponse>;

export const getErrorMessages = (error: AxiosException): string[] =>
  error.response?.data?.errorMessages ?? [error.message];

export const useAxiosQuery = <TData>(
  options: UseQueryOptions<TData, AxiosException>,
): UseQueryResult<TData, AxiosException> => {
  return useQuery<TData, AxiosException>(options);
};

export const useAxiosMutation = <TData = unknown, TVariables = void>(
  options: UseMutationOptions<TData, AxiosException, TVariables>,
): UseMutationResult<TData, AxiosException, TVariables> => {
  return useMutation<TData, AxiosException, TVariables>(options);
};
