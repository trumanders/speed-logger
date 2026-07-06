import type { Student } from "../types/student";

export const isStudentNameValid = (allStudents: Student[], name: string) =>
  name.trim().length >= 2 && !allStudents?.some((s) => s.name === name.trim());
