import { useOutletContext } from "react-router";
import type { BaseLogDaysContext } from "../pages/HomePage";
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
  }, [academicYear, onAddBaseLogDay]);

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

  if (!academicYear || !baseLogDays) {
    return <div>Laddar...</div>;
  }

  return (
    <>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
        }}
      >
        <div className="m-3" style={{ flexShrink: 0 }}>
          <h4>Baslogg - {academicYear.name}</h4>
        </div>
        <div>
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
                  {(date.getDay() === 1 || i === 0) && (
                    <div>
                      <b>
                        <h4>
                          vecka {baseLogDay.weekNumber} <br></br>
                        </h4>
                      </b>
                    </div>
                  )}
                  {new Intl.DateTimeFormat("sv-SE", {
                    day: "numeric",
                    weekday: "long",
                    month: "long",
                  }).format(new Date(baseLogDay.date))}
                </div>,
                <div className="mb-2">
                  <LogTextarea
                    id={baseLogDay.id}
                    content={baseLogDay.content}
                    onTextChange={handleTextChange}
                  />
                </div>,
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
