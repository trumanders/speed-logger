import type { LogCreate } from "../types/logCreate";
import type { LogCreateResponse } from "../types/logCreateResponse";
import apiClient from "./apiClient";

export const addLog = async (
  logCreate: LogCreate,
): Promise<LogCreateResponse> => {
  const response = await apiClient.post(`/Log`, logCreate);
  return response.data;
};
