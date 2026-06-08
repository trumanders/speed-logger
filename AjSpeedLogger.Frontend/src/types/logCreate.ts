export interface LogCreate {
  dayOfWeek: number;
  lessonStartTime: string;
  lessonEndTime: string;
  academicYearId: number | null;
  studentId: number | null;
  newStudentName: string | null;
}
