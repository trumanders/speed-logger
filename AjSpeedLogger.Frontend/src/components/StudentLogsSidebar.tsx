import { Link } from "react-router";
import type { StudentAllUserData } from "../types/allUserData";
import type { AcademicYear } from "../types/academicYear";
import StudentSidebarHeader from "./StudentSideBarHeader";
import LogList from "./LogList";

interface StudentLogsSideBarProps {
  selectedAcademicYearStudents: StudentAllUserData[] | [];
  academicYears: AcademicYear[];
  onSelectStudent: (id: number) => void;
  onAcademicYearSelect: (id: number) => void;
  selectedAcademicYearId: number | null;
}

const StudentLogsSidebar = ({
  selectedAcademicYearStudents,
  academicYears,
  onSelectStudent,
}: StudentLogsSideBarProps) => {
  return (
    <div className="d-flex flex-column h-100">
      {academicYears.length > 0 ? (
        <>
          <div>
            <StudentSidebarHeader />
          </div>
          <div className="flex-grow-1 overflow-auto">
            <LogList
              selectedAcademicYearStudents={selectedAcademicYearStudents}
              onSelectStudent={onSelectStudent}
            />
          </div>
        </>
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
    </div>
  );
};

export default StudentLogsSidebar;
