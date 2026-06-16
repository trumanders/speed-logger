import type { AcademicYearCreate } from "../types/academicYearCreate";
import type { AcademicYearName } from "../types/academicYearName";
import type { AcademicYearAllUserData } from "../types/allUserData";
import apiClient from "./apiClient";

export const getAllUserDataByAcademicYear = async (
  academicYearId: number | null,
) => {
  try {
    const response = await apiClient.get<AcademicYearAllUserData | null>(
      `/AcademicYear/all-user-data`,
      {
        params: academicYearId ? { academicYearId } : {},
      },
    );
    return response.data;
  } catch {
    throw new Error("Kunde inte hämta data för läsåret.");
  }
};

export const getAcademicYears = async (): Promise<AcademicYearName[]> => {
  const response = await apiClient.get(`/AcademicYear`);
  return response.data;
};

export const addAcademicYear = async (academicYear: AcademicYearCreate) => {
  const response = await apiClient.post(`/AcademicYear`, academicYear);
  return response.data;
};
