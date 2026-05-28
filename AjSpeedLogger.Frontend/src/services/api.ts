import axios from "axios";

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
