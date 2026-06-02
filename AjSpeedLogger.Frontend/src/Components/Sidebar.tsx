import { ListGroup } from "react-bootstrap";
import type { AcademicYearAllUserData } from "../types/allUserData";
import AcademicYearDropdown from "./AcademicYearDropDown";
import type { AcademicYear } from "../types/academicYear";

interface SideBarProps {
  allUserData: AcademicYearAllUserData | null;
  academicYears: AcademicYear[];
  onSelectStudent: (id: number) => void;
  onAcademicYearSelect: (id: number) => void;
  selectedAcademicYearId: number | null;
}

const Sidebar = ({ allUserData, academicYears, onSelectStudent, onAcademicYearSelect, selectedAcademicYearId }: SideBarProps) => {
  return (
    <>
      <div>
        <AcademicYearDropdown
          academicYears={academicYears}
          onAcademicYearSelect={onAcademicYearSelect}
          selectedAcademicYearId={selectedAcademicYearId}
        />
      </div>
      <ListGroup>
        {allUserData?.students && allUserData.students.length > 0 ? (
          allUserData.students.map((student) => (
            <ListGroup.Item
              key={student.id}
              action
              onClick={() => onSelectStudent(student.id)}
            >
              {student.log.lessonStartTime.substring(0, 5)} {student.name}
            </ListGroup.Item>
          ))
        ) : (
          <ListGroup.Item disabled>Det finns inga loggar.</ListGroup.Item>
        )}
      </ListGroup>
    </>
  );
};

export default Sidebar;
