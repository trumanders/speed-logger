import { Dropdown } from "react-bootstrap";
import type { AcademicYear } from "../types/academicYear";

interface AcademicYearDropdownProps {
  academicYears: AcademicYear[];
  selectedAcademicYearId: number | null;
  onAcademicYearSelect: (id: number) => void;
}

const AcademicYearDropdown = ({ academicYears, selectedAcademicYearId, onAcademicYearSelect }: AcademicYearDropdownProps) => {
  return (
    <Dropdown className="w-100 mb-3">
      <Dropdown.Toggle 
        variant="outline-secondary" 
        className="w-100 d-flex align-items-center justify-content-between bg-white text-dark border-secondary-subtle"
      >
        <span>Läsår: <strong className="ms-1">{selectedAcademicYearId ? academicYears.find(y => y.id === selectedAcademicYearId)?.name : "Du har inga läsår"}</strong></span>
      </Dropdown.Toggle>

      <Dropdown.Menu className="w-100 shadow-sm border-0 mt-1">
        {academicYears.map((year) => (
          <Dropdown.Item
            className="py-2"
            key={year.id}
            active={year.id === selectedAcademicYearId}
            onClick={() => {onAcademicYearSelect(year.id)}}
          >
            {year.name}
          </Dropdown.Item>
        ))}
      </Dropdown.Menu>
    </Dropdown>
  );
};

export default AcademicYearDropdown;