import { Routes, Route } from "react-router";
import type { AcademicYearAllUserData, LogAllUserDataDto } from "../types/allUserData";
import WorkspaceManageBaseLog from "./WorkspaceManageBaseLog";
import WorkspaceLog from "./WorkspaceLog";
import WorkspaceAddYear from "./WorkspaceAddYear";
import WorkspaceAddLog from "./WorkspaceAddLog";
import WorkspaceManageStudents from "./WorkspaceManageStudents";

interface WorkspaceProps {
  selectedStudent: number | null;
  allUserData: AcademicYearAllUserData | null;
}


const Workspace = ({ selectedStudent, allUserData }: WorkspaceProps) => {
  return (
    <Routes>
      <Route path="/add_year" element={<WorkspaceAddYear />} />
      <Route path="/manage_baselog" element={<WorkspaceManageBaseLog />} />
      <Route path="/manage_students" element={<WorkspaceManageStudents />} />
      <Route path="/log" element={<WorkspaceLog allUserData={allUserData} selectedStudent={selectedStudent} />} />
      <Route path="/add_log" element={<WorkspaceAddLog />} />
    </Routes>
  );
};

export default Workspace;
