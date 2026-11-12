import { Stack } from "react-bootstrap";
import { Link } from "react-router-dom";
import AcademicYearDropdown from "./AcademicYearDropDown";
import type { AcademicYear } from "../types/academicYear";

interface ActionBarProps {
  academicYears: AcademicYear[];
  onAcademicYearSelect: (id: number) => void;
  onEditAcademicYearButtonClick: (academicYearId: number) => void;
  selectedAcademicYearId: number | null;
}

const ActionBar = ({
  academicYears,
  onAcademicYearSelect,
  onEditAcademicYearButtonClick,
  selectedAcademicYearId,
}: ActionBarProps) => {
  return (
    <div>
      <Stack direction="horizontal" gap={2}>
        <AcademicYearDropdown
          academicYears={academicYears}
          onAcademicYearSelect={onAcademicYearSelect}
          selectedAcademicYearId={selectedAcademicYearId}
          onEditAcademicYearButtonClick={onEditAcademicYearButtonClick}
        />
        <Link to="/add-year" className="btn btn-outline-primary btn-sm ms-auto">
          Nytt läsår
        </Link>
        <Link to="/manage-baselog" className="btn btn-outline-primary btn-sm">
          Baslog
        </Link>
        <Link to="/manage-students" className="btn btn-outline-primary btn-sm">
          Elever
        </Link>
      </Stack>
    </div>
  );
};

export default ActionBar;
