import { ListGroup, Stack } from "react-bootstrap";
import type { AcademicYearAllUserData } from "../types/allUserData";
import AcademicYearDropdown from "./AcademicYearDropDown";
import { Link } from "react-router";
import type { AcademicYearName } from "../types/academicYearName";
import { weekDays } from "../constants/weekDays";
import React from "react";

interface SideBarProps {
  allUserData: AcademicYearAllUserData | null;
  academicYears: AcademicYearName[];
  onSelectStudent: (id: number) => void;
  onAcademicYearSelect: (id: number) => void;
  selectedAcademicYearId: number | null;
}

const Sidebar = ({
  allUserData,
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
            <Link to="/add_log" className="btn btn-outline-primary btn-sm">
              Ny logg
            </Link>
          </div>

          <ListGroup>
            {allUserData?.students
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
          <Link to="/add_year" className="btn btn-outline-primary btn-sm">
            Lägg till läsår
          </Link>
        </div>
      )}
      {/* <div><br></br>
        <p>TESTDATA:::</p>
        <p>antal läsår: {academicYears?.length ?? 0}</p>
        <p>selectedId: {selectedAcademicYearId ?? "null"}</p>

        <p>elever: {allUserData?.students?.length ?? 0}</p>
        <p>basloggdagar: {allUserData?.baseLogDays?.length ?? 0}</p>
      </div> */}
    </>
  );
};

export default Sidebar;
