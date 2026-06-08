export interface AcademicYearAllUserData {
  id: number;
  name: string;
  startDate: string;
  endDate: string;
  baseLogDays: BaseLogDay[];
  students: StudentAllUserData[];
}

export interface BaseLogDay {
  id: number;
  date: string;
  weekNumber: number;
  content: string;
}

export interface StudentAllUserData {
  id: number;
  name: string;
  isTerminated: boolean;
  log: LogAllUserData;
}

export interface LogAllUserData {
  id: number;
  dayOfWeek: number;
  lessonStartTime: string;
  lessonEndTime: string;
  logDays: LogDay[];
}

export interface LogDay {
  id: number;
  baseLogDayId: number;
  content: string;
}
