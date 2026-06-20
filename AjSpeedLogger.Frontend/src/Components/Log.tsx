import { weekDays } from "../constants/weekDays";
import { useEffect, useRef, useState } from "react";
import { type LogDay } from "../types/allUserData";
import { Stack } from "react-bootstrap";
import {
  getLogDaysByLogId,
  saveLogDayContent,
} from "../services/logDayService";
import type { LogContext } from "../pages/HomePage";
import { useOutletContext, useParams } from "react-router";

const Log = () => {
  const { baseLogDays, students } = useOutletContext<LogContext>();
  const { studentId } = useParams();
  const student = students.find((s) => s.id === Number(studentId));
  const log = student?.log;

  if (!student) return null;

  const [logDays, setLogDays] = useState<LogDay[]>(() =>
    (baseLogDays || []).map((b) => ({
      id: 0,
      baseLogDayId: b.id,
      content: "",
    })),
  );
  const logTextSaveTimers = useRef<Record<number, any>>({});
  if (!log || !student) return <div>Kan inte hitta log eller elev</div>;

  const handleTextChange = async (baseLogDayId: number, newContent: string) => {
    setLogDays((prev) =>
      prev.map((item) =>
        item.baseLogDayId === baseLogDayId
          ? { ...item, content: newContent }
          : item,
      ),
    );

    if (logTextSaveTimers.current[baseLogDayId])
      clearTimeout(logTextSaveTimers.current[baseLogDayId]);
    logTextSaveTimers.current[baseLogDayId] = setTimeout(async () => {
      await saveLogDayContent(log.id, baseLogDayId, newContent);

      delete logTextSaveTimers.current[baseLogDayId];
    }, 2000);
  };

  useEffect(() => {
    const fetchData = async () => {
      const fetchedLogs = await getLogDaysByLogId(log.id);
      const merged = baseLogDays.map(
        (bld) =>
          fetchedLogs.find((l) => l.baseLogDayId === bld.id) || {
            baseLogDayId: bld.id,
            content: "",
          },
      );
      setLogDays(merged as LogDay[]);
    };
    if (log.id) fetchData();
  }, [student.id, log.id]);

  return (
    <>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          height: "100vh",
          overflow: "hidden",
        }}
      >
        <div style={{ flexShrink: 0 }}>
          <h6>
            {weekDays[log.dayOfWeek]} - {log.lessonStartTime.substring(0, 5)}
          </h6>
          <h4>{student.name}</h4>
        </div>
        <div
          style={{
            flexGrow: 1,
            overflowY: "auto",
          }}
        >
          {(() => {
            const logDayTextBoxes = [];

            // Start from the correct weekday
            const firstBaseLogWeekday = new Date(baseLogDays[0].date).getDay() - 1; // We use mon-sun = 0-6, instead of sun-sat = 0-6
            let firstIndex = log.dayOfWeek - firstBaseLogWeekday;
            if (firstIndex < 0) firstIndex = firstIndex + 7; // Log starts the week after the academicYear start.

            for (let i = firstIndex; i < baseLogDays.length; i += 7) {
              const baseLogDay = baseLogDays[i];
              logDayTextBoxes.push(
                <div key={baseLogDay.id}>
                  v{baseLogDay.weekNumber} -{" "}
                  {new Intl.DateTimeFormat("sv-SE", {
                    day: "numeric",
                    month: "long",
                  }).format(new Date(baseLogDay.date))}
                </div>,
                <Stack direction="horizontal" className="mb-3">
                  {baseLogDay.content ? (<p>FINNS BASELOGG-INLÄGG</p>) : (
                    <textarea
                      value={
                        logDays.find((ld) => ld.baseLogDayId === baseLogDay.id)
                          ?.content || ""
                      }
                      key={`${student.id}-${baseLogDay.id}`}
                      rows={1}
                      style={{ width: "90%", overflow: "hidden", resize: "none" }}
                      onChange={(e) => {
                        const target = e.target as HTMLTextAreaElement;
                        target.style.height = "auto";
                        target.style.height = `${target.scrollHeight}px`;
                        handleTextChange(baseLogDay.id, target.value);
                      }}
                    />
                  )}


                </Stack>
              );
            }
            return logDayTextBoxes;
          })()}
        </div>
      </div>
    </>
  );
};

export default Log;
