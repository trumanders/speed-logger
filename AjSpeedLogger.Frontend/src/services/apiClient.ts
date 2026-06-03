import axios from "axios";

// Skapa en instans av axios
const apiClient = axios.create({
  baseURL: "https://localhost:5000/api",
});

// Lägg till en interceptor för alla utgående anrop
apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem("token"); // Eller var du nu sparar din token

  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export default apiClient;
