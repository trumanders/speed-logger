import type { AcademicYearAllUserData } from "../types/allUserData";
import Log from "./Log";


interface LogProps {
  allUserData: AcademicYearAllUserData | null;
  selectedStudent: number | null;
}

const WorkspaceLog = ({ allUserData, selectedStudent }: LogProps) => {
  if (!allUserData || selectedStudent === null)
    return null;

  const log = allUserData?.students[selectedStudent].log;   

  return (
    <Log log={log} />
  )
};

export default WorkspaceLog;
