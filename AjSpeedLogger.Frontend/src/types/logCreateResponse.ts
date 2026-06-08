export interface LogCreateResponse {
  id: number;
  dayOfWeek: number; // Motsvarar DayOfWeek (enum som int)
  lessonStartTime: string; // "HH:mm:ss"
  lessonEndTime: string;   // "HH:mm:ss"
}