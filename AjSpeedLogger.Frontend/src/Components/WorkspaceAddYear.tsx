import { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { addAcademicYear } from "../services/api";
import type { AcademicYearCreate } from "../types/academicYearCreate";

const WorkspaceAddYear = () => {
  const yearNameOptionsStartYear = new Date().getFullYear() - 3;
  const yearNameOptions = Array.from({ length:8 }, (_, i) =>
    `${yearNameOptionsStartYear + i}/${yearNameOptionsStartYear + i + 1}`);
  const [yearName, setYearName] = useState<string>(
    `${new Date().getFullYear()}/${new Date().getFullYear() + 1}`
  );
  const [startDate, setStartDate] = useState<string>("");
  const [endDate, setEndDate] = useState<string>("");
  
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const newAcademicYear: AcademicYearCreate = {
      name: yearName,
      startDate,
      endDate
    };
    await addAcademicYear(newAcademicYear);
  }

  return (
    <>
      <Form onSubmit={handleSubmit} className="p-3">
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Läsår</Form.Label>
          <Form.Select value={yearName} onChange={(e) => setYearName(e.target.value)}>
            {yearNameOptions.map((year) => (
              <option key={year} value={year}>{year}</option>
            ))}
          </Form.Select>

        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Startdatum</Form.Label>
          <Form.Control value={startDate} onChange={(e) => setStartDate(e.target.value)} type="date" placeholder="Ange startdatum" />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Slutdatum</Form.Label>
          <Form.Control value={endDate} onChange={(e) => setEndDate(e.target.value)} type="date" placeholder="Ange slutdatum" />
        </Form.Group>

        <Button variant="primary" type="submit">
          Skapa läsår
        </Button>
      </Form>
    </>
  )
};

export default WorkspaceAddYear;
