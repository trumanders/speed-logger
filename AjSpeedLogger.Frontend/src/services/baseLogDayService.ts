import type { BaseLogDay } from "../types/allUserData";
import type { BaseLogDayUpdate } from "../types/baseLogDayUpdate";
import apiClient from "./apiClient";

export const getBaseLogDaysByAcademicYear = async (
  academicYearId: number,
): Promise<BaseLogDay[]> => {
  try {
    const response = await apiClient.get(
      `BaseLogDay/academicYearId/${academicYearId}`,
    );
    return response.data;
  } catch {
    throw new Error("Kunde inte hämta basloggar för läsåret");
  }
};

export const updateBaseLogDay = async (
  baseLogDayUpdate: BaseLogDayUpdate,
): Promise<BaseLogDay> => {
  const response = await apiClient.put(
    `/BaseLogDay/${baseLogDayUpdate.id}/content`,
    JSON.stringify(baseLogDayUpdate.content),
    {
      headers: { "Content-Type": "application/json" },
    },
  );
  return response.data;
};
