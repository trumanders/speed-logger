import { useState } from "react";
import { Button, Form, Row, Col } from "react-bootstrap";
import type { AcademicYearCreate } from "../types/academicYearCreate.tsx";
import { useOutletContext } from "react-router";
import type { AddYearContext } from "../pages/HomePage.tsx";

const months = [
  "januari",
  "februari",
  "mars",
  "april",
  "maj",
  "juni",
  "juli",
  "augusti",
  "september",
  "oktober",
  "november",
  "december",
];

const daysInMonth = (month: number, year: number) =>
  new Date(year, month, 0).getDate();

const pad = (n: number) => String(n).padStart(2, "0");

interface MonthDayPickerProps {
  label: string;
  year: number;
  month: number;
  day: number;
  onMonthChange: (month: number) => void;
  onDayChange: (day: number) => void;
}

const MonthDayPicker = ({
  label,
  year,
  month,
  day,
  onMonthChange,
  onDayChange,
}: MonthDayPickerProps) => (
  <Form.Group className="mb-3">
    <Form.Label>
      {label} ({year})
    </Form.Label>
    <Row>
      <Col>
        <Form.Select
          value={day}
          onChange={(e) => onDayChange(Number(e.target.value))}
        >
          {Array.from(
            { length: daysInMonth(month, year) },
            (_, i) => i + 1,
          ).map((d) => (
            <option key={d} value={d}>
              {d}
            </option>
          ))}
        </Form.Select>
      </Col>
      <Col>
        <Form.Select
          value={month}
          onChange={(e) => onMonthChange(Number(e.target.value))}
        >
          {months.map((m, i) => (
            <option key={m} value={i + 1}>
              {m}
            </option>
          ))}
        </Form.Select>
      </Col>
    </Row>
  </Form.Group>
);

const AddYear = () => {
  const { onAddAcademicYear } = useOutletContext<AddYearContext>();
  const yearNameOptionsStartYear = new Date().getFullYear() - 3;
  const yearNameOptions = Array.from(
    { length: 8 },
    (_, i) =>
      `${yearNameOptionsStartYear + i}/${yearNameOptionsStartYear + i + 1}`,
  );
  const [yearName, setYearName] = useState<string>(
    `${new Date().getFullYear()}/${new Date().getFullYear() + 1}`,
  );
  const [startMonth, setStartMonth] = useState(8);
  const [startDay, setStartDay] = useState(15);
  const [endMonth, setEndMonth] = useState(6);
  const [endDay, setEndDay] = useState(15);

  const [firstYear, secondYear] = yearName.split("/").map(Number);

  const handleStartMonthChange = (month: number) => {
    setStartMonth(month);
    setStartDay((d) => Math.min(d, daysInMonth(month, firstYear)));
  };

  const handleEndMonthChange = (month: number) => {
    setEndMonth(month);
    setEndDay((d) => Math.min(d, daysInMonth(month, secondYear)));
  };

  const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();
    const newAcademicYear: AcademicYearCreate = {
      name: yearName,
      startDate: `${firstYear}-${pad(startMonth)}-${pad(Math.min(startDay, daysInMonth(startMonth, firstYear)))}`,
      endDate: `${secondYear}-${pad(endMonth)}-${pad(Math.min(endDay, daysInMonth(endMonth, secondYear)))}`,
    };
    onAddAcademicYear(newAcademicYear);
  };

  return (
    <Form onSubmit={handleSubmit} className="p-3">
      <Form.Group className="mb-3">
        <h4>Lägg till nytt läsår</h4>
        <Form.Select
          value={yearName}
          onChange={(e) => setYearName(e.target.value)}
        >
          {yearNameOptions.map((year) => (
            <option key={year} value={year}>
              {year}
            </option>
          ))}
        </Form.Select>
      </Form.Group>

      <MonthDayPicker
        label="Startdatum"
        year={firstYear}
        month={startMonth}
        day={startDay}
        onMonthChange={handleStartMonthChange}
        onDayChange={setStartDay}
      />
      <MonthDayPicker
        label="Slutdatum"
        year={secondYear}
        month={endMonth}
        day={endDay}
        onMonthChange={handleEndMonthChange}
        onDayChange={setEndDay}
      />

      <Button variant="primary" type="submit">
        Skapa läsår
      </Button>
    </Form>
  );
};

export default AddYear;
