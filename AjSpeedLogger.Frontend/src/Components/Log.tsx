import type { LogAllUserDataDto } from "../types/allUserData";

interface LogProps {
  log: LogAllUserDataDto | null;
}

const Log = ({ log }: LogProps) => {
  if (!log)
    return null;

    return (
        <p>HÄR KOMMER LOGGEN</p>
    )
};

export default Log;