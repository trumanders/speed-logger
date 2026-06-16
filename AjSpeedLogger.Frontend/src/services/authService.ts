import apiClient from "./apiClient";

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
