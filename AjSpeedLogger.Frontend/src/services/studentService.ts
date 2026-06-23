import type { Student } from "../types/student";
import type { StudentCreate } from "../types/studentCreate";
import apiClient from "./apiClient";

export const addStudent = async (
  newStudent: StudentCreate,
): Promise<Student> => {
  const token = localStorage.getItem("token");
  if (!token) {
    throw new Error("Användaren är inte behörig.");
  }
  const response = await apiClient.post(`/Student`, newStudent, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return response.data;
};

export const getAllStudents = async (): Promise<Student[]> => {
  const response = await apiClient.get<Student[]>(`/Student`);
  return response.data;
};

export const updateStudent = async (student: Student): Promise<Student> => {
  const response = await apiClient.put<Student>(
    `/Student/${student.id}`,
    JSON.stringify(student),
    {
      headers: { "Content-Type": "application/json" },
    },
  );
  return response.data;
};
