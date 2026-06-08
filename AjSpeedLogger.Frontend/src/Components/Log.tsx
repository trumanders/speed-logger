import type { LogAllUserData } from "../types/allUserData";

interface LogProps {
  log: LogAllUserData | null;
}

const Log = ({ log }: LogProps) => {
  if (!log) return null;

  return <p>HÄR KOMMER LOGGEN</p>;
};

export default Log;
