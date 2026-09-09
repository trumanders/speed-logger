import { Modal, Button, Form } from "react-bootstrap";
import type { AcademicYear } from "../../types/academicYear";
import { useState } from "react";
import React from "react";

interface EditAcademicYearModalProps {
  academicYears: AcademicYear[];
  academicYear: AcademicYear;
  show: boolean;
  onHide: () => void;
  onConfirm: (updatedAcademicYear: AcademicYear) => void;
}

const EditAcademicYearModal = ({
  academicYear,
  show,
  onHide,
  onConfirm,
}: EditAcademicYearModalProps) => {
  const [startDate, setStartDate] = useState(academicYear.startDate);
  const [endDate, setEndDate] = useState(academicYear.endDate);
  const [showStartRangeError, setShowStartRangeError] = useState(false);
  const [showEndRangeError, setShowEndRangeError] = useState(false);

  const isValidStartDate = () => {
    const startDateObj = new Date(startDate);
    return (
      startDateObj.getFullYear() === parseInt(academicYear.name.split("/")[0])
    );
  };

  const isValidEndDate = () => {
    const endDateObj = new Date(endDate);
    return (
      endDateObj.getFullYear() === parseInt(academicYear.name.split("/")[1])
    );
  };

  const confirmClick = () => {
    if (!isValidStartDate()) {
      setShowStartRangeError(true);
    }
    if (!isValidEndDate()) {
      setShowEndRangeError(true);
    }
    if (isValidStartDate() && isValidEndDate()) {
      console.log(
        "KÖR confirmClick i EditAcademicYearModal:",
        startDate,
        endDate,
      );
      onConfirm({ ...academicYear, startDate, endDate });
      onHide();
    }
  };

  return (
    <Modal show={show} onHide={onHide}>
      <Modal.Header closeButton>
        <Modal.Title>Redigera läsår {academicYear.name}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form.Group className="mb-3"></Form.Group>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>
            <strong>Startdatum</strong>
          </Form.Label>

          <Form.Control
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
            type="date"
            placeholder="Ange startdatum"
            min={academicYear.name.split("/")[0] + "-01-01"}
            max={academicYear.name.split("/")[0] + "-12-31"}
          />
        </Form.Group>
        <React.Fragment>
          {showStartRangeError && (
            <p className="text-danger">
              Startdatum måste matcha det valda läsåret.
            </p>
          )}
        </React.Fragment>

        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>
            <strong>Slutdatum</strong>
          </Form.Label>
          <Form.Control
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
            type="date"
            placeholder="Ange slutdatum"
            min={academicYear.name.split("/")[1] + "-01-01"}
            max={academicYear.name.split("/")[1] + "-12-31"}
          />
          {showEndRangeError && (
            <p className="text-danger">
              Slutdatum måste matcha det valda läsåret.
            </p>
          )}
        </Form.Group>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onHide}>
          Avbryt
        </Button>
        <Button variant="danger" onClick={() => confirmClick()}>
          Spara
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default EditAcademicYearModal;
