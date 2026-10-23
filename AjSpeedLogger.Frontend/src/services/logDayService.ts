import type { LogDay } from "../types/allUserData";
import type { LogDayCreate } from "../types/logDayCreate";
import apiClient from "./apiClient";

export const createLogDay = async (
  logDayCreate: LogDayCreate,
): Promise<LogDay> => {
  const response = await apiClient.post(`/LogDay`, logDayCreate);
  return response.data;
};

// ANVÄNDS INTE?
// export const updateLogDay = async (
//   logDayUpdate: LogDayUpdate,
// ): Promise<LogDay> => {
//   const response = await apiClient.put(
//     `/LogDay/${logDayUpdate.id}/content`,
//     JSON.stringify(logDayUpdate.content),
//     { headers: { "Content-Type": "application/json" } },
//   );
//   return response.data;
// };

export const deleteLogDay = async (logDayId: number) => {
  await apiClient.delete(`/LogDay/${logDayId}`);
};

export const getLogDayByBaseLogDayId = async (
  baseLogDayId: number,
): Promise<LogDay | null> => {
  try {
    const response = await apiClient.get(
      `/LogDay/baseLogDayId/${baseLogDayId}`,
    );
    return response.data;
  } catch (error: any) {
    if (error.response?.status === 404) return null;
    throw error;
  }
};

export const getLogDaysByLogId = async (logId: number): Promise<LogDay[]> => {
  const result = await apiClient.get(`LogDay/logId/${logId}`);
  return result.data;
};

export const saveLogDayContent = async (
  logId: number,
  baseLogDayId: number,
  content: string,
) => {
  await apiClient.put(`/LogDay/${logId}/content`, JSON.stringify(content), {
    headers: { "Content-Type": "application/json" },
    params: { baseLogDayId },
  });
};

export const saveAllLogDays = async (logId: number, logDays: LogDay[]) => {
  const promises = logDays.map((logDay) =>
    saveLogDayContent(logId, logDay.baseLogDayId, logDay.content),
  );
  await Promise.all(promises);
};
