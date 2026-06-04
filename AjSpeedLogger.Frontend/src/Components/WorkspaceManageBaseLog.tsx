import type { AcademicYearAllUserData } from "../types/allUserData";

interface WorkspaceManageBaseLogProps {
  allUserData: AcademicYearAllUserData | null;
}

const WorkspaceManageBaseLog = ({
  allUserData,
}: WorkspaceManageBaseLogProps) => {
  return (
    <>
      <p>MANAGE BASE LOG {allUserData ? allUserData.id : "N/A"}</p>
      {allUserData?.baseLogDaysDtos.map((log) => (
        <div key={log.id}>
          <p>{log.date}</p>
          <p>{log.content}</p>
        </div>
      ))}
    </>
  );
};

export default WorkspaceManageBaseLog;
