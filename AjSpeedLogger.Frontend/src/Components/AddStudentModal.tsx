import React, { useState } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import type { Student } from "../types/student";

interface AddStudentModalProps {
  show: boolean;
  onHide: () => void;
  onAddStudent: (name: string) => void;
  allStudents: Student[] | null;
}

export const AddStudentModal = ({
  show,
  onHide,
  onAddStudent,
  allStudents,
}: AddStudentModalProps) => {
  const [name, setName] = useState("");
  const [isShowAdditionalInput, setIsShowAdditionalInput] = useState(false);
  const [additionalInput, setAdditionalInput] = useState("");

  const onAddStudentClick = (name: string) => {
    if (allStudents?.some((s) => s.name === name)) {
      console.log("HITTADE ELEV MED SAMMA NAMN");
      setIsShowAdditionalInput(true);
    } else {
      onAddStudent(name);
      setName("");
      setAdditionalInput("");
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

          {isShowAdditionalInput && (
            <React.Fragment>
              <p className="d">
                En elev med samma namn finns redan. Ange smeknamn, eller annan
                unik identifikation.
              </p>
              <Form.Control
                placeholder="Lägg till unikt namn"
                value={additionalInput}
                onChange={(e) => setAdditionalInput(e.target.value)}
              />
            </React.Fragment>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button
            disabled={!name || (isShowAdditionalInput && !additionalInput)}
            variant="primary"
            onClick={() => {
              onAddStudentClick(
                `${name}${additionalInput ? ` (${additionalInput})` : ""}`,
              );
            }}
          >
            Spara
          </Button>
        </Modal.Footer>
      </Modal>
    </React.Fragment>
  );
};
