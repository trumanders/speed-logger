import axios from "axios";
import type { AcademicYearAllUserData } from "../types/allUserData";

const BASE_URL = "https://localhost:7293/api";

export interface LoginCredentials {
  email: string;
  password: string;
}

export const login = async (credentials: LoginCredentials) => {
  const response = await axios.post(`${BASE_URL}/Auth/login`, credentials);
  return response.data;
};

export const signup = async (credentials: LoginCredentials) => {
  const response = await axios.post(`${BASE_URL}/Auth/register`, credentials);
  return response.data;
};

export const forgotPassword = async (email: string) => {
  const response = await axios.post(`${BASE_URL}/Auth/forgot-password`, {
    email,
  });
  return response.data;
};

export const resetPassword = async (
  email: string,
  token: string,
  newPassword: string,
) => {
  await axios.post(`${BASE_URL}/Auth/reset-password`, {
    email,
    token,
    newPassword: newPassword,
  });
};

export const getAllUserDataByAcademicYear = async (academicYearId: number | null) => {
  const token = localStorage.getItem("token");
  if (!token) {
    throw new Error("User is not authenticated");
  }
  try {
    const response = await axios.get<AcademicYearAllUserData | null>(
      `${BASE_URL}/AcademicYear/all-user-data`,
      {
        headers: { Authorization: `Bearer ${token}` },
        params: { academicYearId }
      },
    );
    return response.data;
  } catch {
    throw new Error("Kunde inte hämta data för läsåret.");
  }
};

export const getAcademicYears = async () => {
  const response = await axios.get(`${BASE_URL}/AcademicYear`);
  return response.data;
}
