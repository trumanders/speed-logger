import axios from "axios";

const BASE_URL = "http://localhost:7293/api";

export interface LoginCredentials {
  email: string;
  password: string;
}

export const login = async (credentials: LoginCredentials) => {
  const response = await axios.post(`${BASE_URL}/Auth/login`, credentials);
  return response.data;
};
