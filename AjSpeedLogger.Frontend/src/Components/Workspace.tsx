import { Routes, Route } from "react-router";
import type {
  AcademicYearAllUserData,
  StudentAllUserData,
} from "../types/allUserData";
import WorkspaceManageBaseLog from "./WorkspaceManageBaseLog";
import WorkspaceLog from "./WorkspaceLog";
import WorkspaceAddYear from "./WorkspaceAddYear";
import WorkspaceAddLog from "./WorkspaceAddLog";
import WorkspaceManageStudents from "./WorkspaceManageStudents";
import type { AcademicYearCreate } from "../types/academicYearCreate";
import type { LogCreate } from "../types/logCreate";

interface WorkspaceProps {
  selectedStudent: number | null;
  allUserData: AcademicYearAllUserData | null;
  onAddAcademicYear: (newAcademicYear: AcademicYearCreate) => void;
  academicYearId: number | null;
  studentList: StudentAllUserData[] | undefined;
  onAddLog: (newLog: LogCreate) => void;
  onShowAddStudent: () => void;
  newStudentName: string;
  resetNewStudentName: () => void;
}

const Workspace = ({
  selectedStudent,
  allUserData,
  onAddAcademicYear,
  academicYearId,
  studentList,
  onAddLog,
  onShowAddStudent,
  newStudentName,
  resetNewStudentName,
}: WorkspaceProps) => {
  return (
    <Routes>
      <Route
        path="/add_year"
        element={<WorkspaceAddYear onAddAcademicYear={onAddAcademicYear} />}
      />
      <Route
        path="/manage_baselog"
        element={<WorkspaceManageBaseLog allUserData={allUserData} />}
      />
      <Route path="/manage_students" element={<WorkspaceManageStudents />} />
      <Route
        path="/log"
        element={
          <WorkspaceLog
            allUserData={allUserData}
            selectedStudent={selectedStudent}
          />
        }
      />
      <Route
        path="/add_log"
        element={
          <WorkspaceAddLog
            academicYearId={academicYearId}
            studentList={studentList}
            onAddLog={onAddLog}
            onShowAddStudent={onShowAddStudent}
            newStudentName={newStudentName}
            resetNewStudentName={resetNewStudentName}
          />
        }
      />
    </Routes>
  );
};

export default Workspace;
