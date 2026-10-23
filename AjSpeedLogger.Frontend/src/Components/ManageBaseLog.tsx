import { useOutletContext } from "react-router";
import type { BaseLogDaysContext } from "../pages/HomePage";
import { Stack } from "react-bootstrap";
import { useEffect, useRef } from "react";
import {
  getBaseLogDaysByAcademicYear,
  updateBaseLogDay,
} from "../services/baseLogDayService";
import LogTextarea from "./LogTextarea";

const ManageBaseLog = () => {
  const { baseLogDays, academicYear, onAddBaseLogDay, onUpdateBaseLogDay } =
    useOutletContext<BaseLogDaysContext>();
  const baseLogTextSaveTimers = useRef<Record<number, any>>({});

  const handleTextChange = async (id: number, newContent: string) => {
    // Local update
    if (baseLogTextSaveTimers.current[id]) {
      clearTimeout(baseLogTextSaveTimers.current[id]);
    }
    baseLogTextSaveTimers.current[id] = setTimeout(async () => {
      onUpdateBaseLogDay(id, newContent);

      await updateBaseLogDay({ id: id, content: newContent });
      delete baseLogTextSaveTimers.current[id];
    }, 2000);
  };

  useEffect(() => {
    const fetchData = async () => {
      const fetchedBaseLogDays = await getBaseLogDaysByAcademicYear(
        academicYear.id,
      );
      fetchedBaseLogDays.forEach((fetchedBaseLogDay) => {
        const existingBaseLogDay = baseLogDays.find(
          (bld) => bld.id === fetchedBaseLogDay.id,
        );
        if (!existingBaseLogDay) {
          onAddBaseLogDay(fetchedBaseLogDay);
        }
      });
    };

    fetchData();
  }, [academicYear.id, onAddBaseLogDay]);

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
        <div className="m-3" style={{ flexShrink: 0 }}>
          <h4>Baslogg - {academicYear.name}</h4>
        </div>
        <div style={{ flexGrow: 1, overflowY: "auto" }}>
          {(() => {
            const sortedBaseLogDays = [...baseLogDays].sort((a, b) =>
              a.date.localeCompare(b.date),
            );
            const textBoxes = [];
            for (let i = 0; i < sortedBaseLogDays.length; i++) {
              const baseLogDay = sortedBaseLogDays[i];
              const date = new Date(baseLogDay.date);
              textBoxes.push(
                <div key={baseLogDay.id}>
                  {date.getDay() === 1 && (
                    <>
                      <b>
                        v{baseLogDay.weekNumber} <br></br>
                      </b>
                    </>
                  )}
                  {new Intl.DateTimeFormat("sv-SE", {
                    day: "numeric",
                    weekday: "long",
                    month: "long",
                  }).format(new Date(baseLogDay.date))}
                </div>,
                <Stack direction="horizontal" className="mb-3">
                  <LogTextarea
                    id={baseLogDay.id}
                    content={baseLogDay.content}
                    onTextChange={handleTextChange}
                  />
                </Stack>,
              );
            }
            return textBoxes;
          })()}
        </div>
      </div>
    </>
  );
};

export default ManageBaseLog;
