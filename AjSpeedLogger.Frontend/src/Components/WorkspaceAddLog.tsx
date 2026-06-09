import { useEffect, useState } from "react";
import { Alert, Button, Form } from "react-bootstrap";
import type { LogCreate } from "../types/logCreate";
import type { StudentAllUserData } from "../types/allUserData";

interface WorkspaceaAddLogProps {
  academicYearId: number | null;
  studentList: StudentAllUserData[] | undefined;
  onAddLog: (newLog: LogCreate) => void;
  onShowAddStudent: () => void;
  newStudentName: string;
  resetNewStudentName: () => void;
}

const WorkspaceAddLog = ({
  academicYearId,
  studentList,
  onAddLog,
  onShowAddStudent,
  newStudentName,
  resetNewStudentName,
}: WorkspaceaAddLogProps) => {
  const [studentId, setStudentId] = useState<number | null>(null);
  const [weekDay, setWeekDay] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const [endTimeError, setEndTimeError] = useState<string | null>(null);
  const [showError, setShowError] = useState(false);
  const weekDays = [
    "Söndag",
    "Måndag",
    "Tisdag",
    "Onsdag",
    "Torsdag",
    "Fredag",
    "Lördag",
  ];
  const handleStartTimeChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setStartTime(value);
    if (value) {
      const [hours, minutes] = value.split(":").map(Number);
      const total = hours * 60 + minutes + 20;
      const newHours = Math.floor(total / 60) % 24;
      const newMinutes = total % 60;
      setEndTime(
        `${String(newHours).padStart(2, "0")}:${String(newMinutes).padStart(2, "0")}`,
      );
    }
  };

  const handleEndTimeChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    if (value <= startTime) {
      setEndTimeError("Sluttid måste vara efter starttid.");
    }
    if (value > startTime) {
      setEndTimeError(null);
    }
    setEndTime(value);
  };

  const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();
    const newLog: LogCreate = {
      dayOfWeek: weekDays.indexOf(weekDay),
      lessonStartTime: startTime,
      lessonEndTime: endTime,
      academicYearId: academicYearId,
      studentId: studentId,
      newStudentName: newStudentName,
    };
    onAddLog(newLog);

    setStudentId(null);
    setWeekDay(weekDays[0]);
    setStartTime("");
    setEndTime("");
    resetNewStudentName();
  };

  const handleSelectStudentChange = (
    e: React.ChangeEvent<HTMLSelectElement>,
  ) => {
    const val = e.target.value;
    setStudentId(val !== "" && !isNaN(Number(val)) ? Number(val) : null);
    // if (val) resetNewStudentName(); // Existing student selected
  };

  useEffect(() => {
    setStudentId(null);
  }, [newStudentName]);

  useEffect(() => {
    if (endTimeError) {
      const timer = setTimeout(() => setShowError(true), 1000);
      return () => clearTimeout(timer);
    } else {
      setShowError(false);
    }
  }, [endTimeError]);

  return (
    <>
      <p>{studentId ?? "null"}</p>
      <Form onSubmit={handleSubmit} className="p-3">
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label className="fw-bold">Elev</Form.Label>
          <Form.Select
            value={studentId ?? newStudentName}
            onChange={(e) => {
              handleSelectStudentChange(e);
            }}
          >
            <option value={newStudentName}>
              {newStudentName || "Välj elev..."}
            </option>
            {studentList?.map((student) => (
              <option key={student.id} value={student.id}>
                {student.name}
              </option>
            ))}
          </Form.Select>
        </Form.Group>
        <span
          onClick={onShowAddStudent}
          style={{
            cursor: "pointer",
            color: "blue",
            textDecoration: "underline",
            fontSize: "0.85rem",
          }}
        >
          + Lägg till ny elev
        </span>

        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label className="fw-bold">Veckodag</Form.Label>
          <Form.Select
            value={weekDay}
            onChange={(e) => setWeekDay(e.target.value)}
          >
            {weekDays.map((weekDay) => (
              <option key={weekDay} value={weekDay}>
                {weekDay}
              </option>
            ))}
          </Form.Select>
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Lektionens starttid</Form.Label>
          <Form.Control
            value={startTime}
            onChange={handleStartTimeChange}
            type="time"
          />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Lektionens sluttid</Form.Label>
          <Form.Control
            value={endTime}
            onChange={handleEndTimeChange}
            type="time"
          />
        </Form.Group>
        {showError && <Alert>{endTimeError}</Alert>}
        <Button
          variant="primary"
          type="submit"
          disabled={(!studentId && !newStudentName) || endTimeError !== null}
        >
          Skapa elevlogg
        </Button>
      </Form>
    </>
  );
};

export default WorkspaceAddLog;
