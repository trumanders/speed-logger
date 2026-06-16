import { ListGroup, Stack } from "react-bootstrap";
import AcademicYearDropdown from "./AcademicYearDropDown";
import { Link } from "react-router";
import type { AcademicYearName } from "../types/academicYearName";
import { weekDays } from "../constants/weekDays";
import React from "react";
import type { StudentAllUserData } from "../types/allUserData";

interface SideBarProps {
  selectedAcademicYearStudents: StudentAllUserData[] | [];
  academicYears: AcademicYearName[];
  onSelectStudent: (id: number) => void;
  onAcademicYearSelect: (id: number) => void;
  selectedAcademicYearId: number | null;
}

const Sidebar = ({
  selectedAcademicYearStudents,
  academicYears,
  onSelectStudent,
  onAcademicYearSelect,
  selectedAcademicYearId,
}: SideBarProps) => {
  return (
    <>
      {academicYears.length > 0 ? (
        <div>
          <AcademicYearDropdown
            academicYears={academicYears}
            onAcademicYearSelect={onAcademicYearSelect}
            selectedAcademicYearId={selectedAcademicYearId}
          />
          <div className="d-flex justify-content-center mb-3">
            <Link to="/add-log" className="btn btn-outline-primary btn-sm">
              Ny logg
            </Link>
          </div>

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
                      {student.log.lessonStartTime.substring(0, 5)}{" "}
                      {student.name}
                    </ListGroup.Item>
                  </React.Fragment>
                );
              })}
          </ListGroup>
        </div>
      ) : (
        <div>
          <p>
            <b>Du har inga läsår</b>
          </p>
          <Link to="/add-year" className="btn btn-outline-primary btn-sm">
            Lägg till läsår
          </Link>
        </div>
      )}
    </>
  );
};

export default Sidebar;
