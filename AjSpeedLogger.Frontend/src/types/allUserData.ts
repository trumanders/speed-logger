export interface AcademicYearAllUserDataDto {
  name: string;
  startDate: string;
  endDate: string;
  baseLogDaysDtos: BaseLogDayDto[];
  students: StudentAllUserDataDto[];
}

export interface BaseLogDayDto {
  id: number;
  date: string;
  weekNumber: number;
  content: string;
}

export interface StudentAllUserDataDto {
  id: number;
  name: string;
  log: LogAllUserDataDto;
}

export interface LogAllUserDataDto {
  id: number;
  dayOfWeek: number;
  lessonStartTime: string;
  lessonEndTime: string;
  logDays: LogDayDto[];
}

export interface LogDayDto {
  id: number;
  baseLogDayId: number;
  content: string;
}
