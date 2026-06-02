import type { AcademicYearAllUserData } from "../types/allUserData";

interface WorkspaceProps {
  selectedStudent: number | null;
  currentWorkspace: string;
  allUserData: AcademicYearAllUserData | null;
}

const Workspace = ({
  selectedStudent,
  currentWorkspace,
  allUserData,
}: WorkspaceProps) => {
  return (
    <>
      <p>Current workspace: {currentWorkspace}</p>
      <p>
        Selected student:{" "}
        {allUserData?.students.find((s) => s.id == selectedStudent)?.name ??
          "ingen elev vald"}
      </p>
    </>
  );
};

export default Workspace;
