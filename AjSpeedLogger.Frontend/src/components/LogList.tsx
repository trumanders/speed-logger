import { ListGroup } from "react-bootstrap";
import type { StudentAllUserData } from "../types/allUserData";
import React from "react";
import { weekDays } from "../constants/weekDays";

interface LogListProps {
  selectedAcademicYearStudents: StudentAllUserData[];
  onSelectStudent: (id: number) => void;
}

const LogList = ({
  selectedAcademicYearStudents,
  onSelectStudent,
}: LogListProps) => {
  return (
    <ListGroup>
      {selectedAcademicYearStudents
        ?.slice() // Create a copy to not mutate the original
        .sort(
          (a, b) =>
            a.log.dayOfWeek - b.log.dayOfWeek ||
            a.log.lessonStartTime.localeCompare(b.log.lessonStartTime),
        )
        .map((student, i, arr) => {
          const isNewDay =
            i === 0 || student.log.dayOfWeek !== arr[i - 1].log.dayOfWeek;

          return (
            <React.Fragment key={student.id}>
              {isNewDay && (
                <div className="mt-3 mb-1 fw-bold text-muted">
                  {weekDays[student.log.dayOfWeek]}
                </div>
              )}
              <ListGroup.Item
                action
                onClick={() => onSelectStudent(student.id)}
                className="mb-1 border rounded-3"
              >
                {student.log.lessonStartTime.substring(0, 5)} {student.name}
              </ListGroup.Item>
            </React.Fragment>
          );
        })}
    </ListGroup>
  );
};

export default LogList;
