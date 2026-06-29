import { useOutletContext } from "react-router";
import type { ManageStudentsContext } from "../../pages/HomePage";
import type { Student } from "../../types/student";

const EditStudentButton = ({ student }: { student: Student }) => {
  const { onEditStudentClick } = useOutletContext<ManageStudentsContext>();
  return (
    <button
      className="btn p-0 border-0 ms-auto"
      onClick={() => student && onEditStudentClick(student)}
    >
      <img src="/images/edit.png" className="edit-icon" />
    </button>
  );
};

export default EditStudentButton;
