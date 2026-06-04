import { Routes, Route } from "react-router";
import type { AcademicYearAllUserData } from "../types/allUserData";
import WorkspaceManageBaseLog from "./WorkspaceManageBaseLog";
import WorkspaceLog from "./WorkspaceLog";
import WorkspaceAddYear from "./WorkspaceAddYear";
import WorkspaceAddLog from "./WorkspaceAddLog";
import WorkspaceManageStudents from "./WorkspaceManageStudents";
import type { AcademicYear } from "../types/academicYear";

interface WorkspaceProps {
  selectedStudent: number | null;
  allUserData: AcademicYearAllUserData | null;
  onAddAcademicYear: (newAcademicYear: AcademicYear) => void;
}

const Workspace = ({
  selectedStudent,
  allUserData,
  onAddAcademicYear,
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
      <Route path="/add_log" element={<WorkspaceAddLog />} />
    </Routes>
  );
};

export default Workspace;
