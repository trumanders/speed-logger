import { Stack } from "react-bootstrap";
import type { Student } from "../types/student";
import EditStudentButton from "./buttons/EditStudentButton";

interface LogHeaderProps {
  headerText: string;
  student: Student;
}

const LogHeader = ({ headerText, student }: LogHeaderProps) => {
  return (
    <div>
      <h6>{headerText}</h6>
      <Stack direction="horizontal">
        <h4>{student.name}</h4>
        <EditStudentButton student={student} />
      </Stack>
    </div>
  );
};

export default LogHeader;
