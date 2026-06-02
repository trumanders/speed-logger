import axios from "axios";
import type { AcademicYearAllUserData } from "../types/allUserData";
import apiClient from "./apiClient";
import type { AcademicYearCreate } from "../types/academicYearCreate";

export interface LoginCredentials {
  email: string;
  password: string;
}

export const login = async (credentials: LoginCredentials) => {
  const response = await apiClient.post(`/Auth/login`, credentials);
  return response.data;
};

export const signup = async (credentials: LoginCredentials) => {
  const response = await apiClient.post(`/Auth/register`, credentials);
  return response.data;
};

export const forgotPassword = async (email: string) => {
  const response = await apiClient.post(`/Auth/forgot-password`, {
    email,
  });
  return response.data;
};

export const resetPassword = async (
  email: string,
  token: string,
  newPassword: string,
) => {
  await apiClient.post(`/Auth/reset-password`, {
    email,
    token,
    newPassword: newPassword,
  });
};

export const getAllUserDataByAcademicYear = async (
  academicYearId: number | null,
) => {
  const token = localStorage.getItem("token");
  if (!token) {
    throw new Error("User is not authenticated");
  }
  try {
    const response = await apiClient.get<AcademicYearAllUserData | null>(
      `/AcademicYear/all-user-data`,
      {
        headers: { Authorization: `Bearer ${token}` },
        params: { academicYearId },
      },
    );
    return response.data;
  } catch {
    throw new Error("Kunde inte hämta data för läsåret.");
  }
};

export const getAcademicYears = async () => {
  const token = localStorage.getItem("token");
  if (!token) {
    throw new Error("User is not authenticated");
  }
  const response = await apiClient.get(`/AcademicYear`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return response.data;
};

export const addAcademicYear = async (academicYear: AcademicYearCreate) => {
  const token = localStorage.getItem("token");
  if (!token) {
    throw new Error("User is not authenticated");
  }
  const response = await apiClient.post(`/AcademicYear`, academicYear, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return response.data;
};
