import { useEffect, useMemo, useState } from "react";
import { Alert, Button, Form } from "react-bootstrap";
import type { LogCreate } from "../types/logCreate";
import { weekDays } from "../constants/weekDays";
import { useOutletContext } from "react-router";
import type { AddLogContext } from "../pages/HomePage";

const AddLog = () => {
  const {
    selectedAcademicYearId,
    selectedAcademicYearStudents,
    allStudents,
    onAddLog,
    onShowAddStudent,
    newStudentName,
    resetNewStudentName,
  } = useOutletContext<AddLogContext>();

  const [studentId, setStudentId] = useState<number | null>(null);
  const [weekDay, setWeekDay] = useState<number>(0);
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const [endTimeError, setEndTimeError] = useState<string | null>(null);
  const [showError, setShowError] = useState(false);

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
      dayOfWeek: weekDay,
      lessonStartTime: startTime,
      lessonEndTime: endTime,
      academicYearId: selectedAcademicYearId,
      studentId: studentId,
      newStudentName: newStudentName,
    };
    onAddLog(newLog);
    setStudentId(null);
    setWeekDay(0);
    setStartTime("");
    setEndTime("");
    resetNewStudentName();
  };

  const handleSelectStudentChange = (
    e: React.ChangeEvent<HTMLSelectElement>,
  ) => {
    const val = e.target.value;
    setStudentId(val !== "" && !isNaN(Number(val)) ? Number(val) : null);
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

  const filteredStudents = useMemo(() => {
    return allStudents?.filter(
      (s) =>
        !s.isTerminated &&
        !selectedAcademicYearStudents?.some((says) => s.id === says.id),
    );
  }, [selectedAcademicYearStudents, allStudents]);

  return (
    <>
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

            {filteredStudents
              ?.filter((s) => s)
              .map((student) => (
                <option key={student.id} value={student.id!}>
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
            value={weekDay} // index value
            onChange={(e) => setWeekDay(Number(e.target.value))}
          >
            {weekDays.map((_, _i) => (
              <option key={_i} value={_i}>
                {weekDays[_i]}
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

export default AddLog;
