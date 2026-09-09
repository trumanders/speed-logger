interface EditAcademicYearButtonProps {
  academicYearId: number;
  onEditAcademicYearButtonClick: (academicYearId: number) => void;
}

const EditAcademicYearButton = ({
  academicYearId,
  onEditAcademicYearButtonClick,
}: EditAcademicYearButtonProps) => {
  return (
    <button
      className="btn p-0 border-0 ms-auto"
      onClick={() => onEditAcademicYearButtonClick(academicYearId)}
    >
      <img src="/images/edit.png" className="edit-icon" />
    </button>
  );
};

export default EditAcademicYearButton;
