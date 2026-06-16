import { useOutletContext } from "react-router";
import type { BaseLogDaysContext } from "../pages/HomePage";

const ManageBaseLog = () => {
  const { baseLogDays } = useOutletContext<BaseLogDaysContext>();
  return (
    <>
      <p>
        MANAGE BASE LOG {baseLogDays ? baseLogDays[0].academicYearId : "N/A"}
      </p>
      {baseLogDays.map((log) => (
        <div key={log.id}>
          <p>{log.date}</p>
          <p>{log.content}</p>
        </div>
      ))}
    </>
  );
};

export default ManageBaseLog;
