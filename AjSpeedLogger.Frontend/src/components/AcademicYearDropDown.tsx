import { Dropdown, Stack } from "react-bootstrap";
import type { AcademicYear } from "../types/academicYear";

interface AcademicYearDropdownProps {
  academicYears: AcademicYear[];
  selectedAcademicYearId: number | null;
  onAcademicYearSelect: (id: number) => void;
  onEditAcademicYearButtonClick: (academicYearId: number) => void;
}

const AcademicYearDropdown = ({
  academicYears,
  selectedAcademicYearId,
  onAcademicYearSelect,
  onEditAcademicYearButtonClick,
}: AcademicYearDropdownProps) => {
  return (
    <Stack className="align-items-center" direction="horizontal">
      <h6 className="mb-2 me-2 mt-2">Läsår:</h6>

      <Dropdown className="mb-2 mt-2">
        <Dropdown.Toggle
          variant="outline-secondary"
          className="d-flex align-items-center justify-content-between bg-white text-dark border-secondary-subtle"
        >
          <Stack direction="horizontal" gap={2}>
            <span>
              {academicYears?.length > 0
                ? academicYears.find((y) => y.id === selectedAcademicYearId)
                    ?.name
                : "Du har inga läsår"}
            </span>
            {academicYears?.length > 0 && selectedAcademicYearId && (
              <span
                role="button"
                className="btn p-0 border-0 ms-auto"
                onClick={(e) => {
                  e.stopPropagation();
                  onEditAcademicYearButtonClick(selectedAcademicYearId);
                }}
              >
                <img src="/images/edit.png" className="edit-icon" />
              </span>
            )}
          </Stack>
        </Dropdown.Toggle>

        <Dropdown.Menu className="w-100 shadow-sm border-0 mt-1">
          {academicYears.map((year) => (
            <Dropdown.Item
              className="py-2"
              key={year.id}
              active={year.id === selectedAcademicYearId}
              onClick={() => {
                onAcademicYearSelect(year.id);
              }}
            >
              <Stack direction="horizontal">
                <p>{year.name}</p>
              </Stack>
            </Dropdown.Item>
          ))}
        </Dropdown.Menu>
      </Dropdown>
    </Stack>
  );
};

export default AcademicYearDropdown;
