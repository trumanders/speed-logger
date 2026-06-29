import React, { useState } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import type { Student } from "../../types/student";

interface AddStudentModalProps {
  show: boolean;
  onHide: () => void;
  onAddStudent: (nameInput: string) => void;
  allStudents: Student[] | null;
}

export const AddStudentModal = ({
  show,
  onHide,
  onAddStudent,
  allStudents,
}: AddStudentModalProps) => {
  const [name, setName] = useState("");
  const [isDuplicate, setIsDuplicate] = useState(false);

  const onAddStudentClick = (name: string) => {
    if (allStudents?.some((s) => s.name === name)) {
      setIsDuplicate(true);
    } else {
      onAddStudent(name);
      setName("");
    }
  };

  return (
    <React.Fragment>
      <Modal show={show} onHide={onHide}>
        <Modal.Header closeButton>
          <Modal.Title>Lägg till ny elev:</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Control
            placeholder="Namn"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />

          {isDuplicate && (
            <React.Fragment>
              <p className="mt-3">
                En elev med samma namn finns redan. Ange smeknamn, eller annan
                unik personlig identifikation för att särskilja dem.
              </p>
            </React.Fragment>
          )}
          {name.length < 2 && (
            <p className="text-alert mt-3">
              Namnet måste innehålla minst 2 tecken.
            </p>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button
            disabled={name.length < 2 || isDuplicate}
            variant="primary"
            onClick={() => onAddStudentClick(name)}
          >
            Spara
          </Button>
        </Modal.Footer>
      </Modal>
    </React.Fragment>
  );
};
