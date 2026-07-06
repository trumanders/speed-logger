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
    <>
      <span>
        <h6>Läsår:</h6>
      </span>

      <Dropdown className="w-100 mb-3">
        <Dropdown.Toggle
          variant="outline-secondary"
          className="w-100 d-flex align-items-center justify-content-between bg-white text-dark border-secondary-subtle"
        >
          <Stack direction="horizontal" gap={2}>
            <span>
              {academicYears?.length > 0
                ? academicYears.find((y) => y.id === selectedAcademicYearId)
                    ?.name
                : "Du har inga läsår"}
            </span>
            {academicYears?.length > 0 && selectedAcademicYearId && (
              <button
                className="btn p-0 border-0 ms-auto"
                onClick={() =>
                  onEditAcademicYearButtonClick(selectedAcademicYearId)
                }
              >
                <img src="/images/edit.png" className="edit-icon" />
              </button>
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
    </>
  );
};

export default AcademicYearDropdown;
