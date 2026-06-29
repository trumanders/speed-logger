import React, { useEffect, useState } from "react";
import type { Student } from "../../types/student";
import { Button, Form, Modal } from "react-bootstrap";

interface EditStudentModalProps {
  show: boolean;
  student: Student | undefined;
  onHide: () => void;
  allStudents: Student[];
  onEditStudent: (student: Student) => void;
  onDeleteStudent: (type: string, message: string) => void;
}

const EditStudentModal = ({
  show,
  onHide,
  student,
  allStudents,
  onEditStudent,
  onDeleteStudent,
}: EditStudentModalProps) => {
  const [newName, setNewName] = useState<string | undefined>(
    student?.name || "",
  );
  const [isDuplicate, setIsDuplicate] = useState(false);
  const [disableButton, setDisableButton] = useState(true);
  const [isStudentTerminated, setIsStudentTerminated] = useState(
    student?.isTerminated || false,
  );

  const isSomethingChanged = (name: string, isTerminated: boolean) =>
    name.trim() !== student?.name || isTerminated !== student?.isTerminated;

  const isInvalid = (name: string) =>
    name.trim().length < 2 ||
    allStudents?.some((s) => s.name === name.trim() && s.id !== student?.id);

  const onChangeName = (value: string) => {
    const duplicate =
      value.trim() !== student?.name &&
      allStudents?.some((s) => s.name === value.trim());
    setIsDuplicate(duplicate);
    setNewName(duplicate ? student?.name : value.trim());
    setDisableButton(
      duplicate ||
        !isSomethingChanged(value, isStudentTerminated) ||
        isInvalid(value),
    );
  };

  const isTerminatedChange = (isTerminated: boolean) => {
    setIsStudentTerminated(isTerminated);
    setDisableButton(
      isDuplicate ||
        (isTerminated === student?.isTerminated && newName === student.name),
    );
  };

  useEffect(() => {
    setIsDuplicate(false);
    setNewName(student?.name || "");
    setIsStudentTerminated(student?.isTerminated || false);
  }, [show, student]);

  return (
    <React.Fragment>
      <Modal show={show} onHide={onHide}>
        <Modal.Header closeButton>
          <Modal.Title>Redigera elev</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>
            <b>
              {student?.name} NEW NAME= {newName}
            </b>
          </p>
          <Form.Control
            defaultValue={student?.name}
            onChange={(e) => onChangeName(e.target.value)}
          />
          {isDuplicate && (
            <React.Fragment>
              <p className="mt-3">
                En elev med samma namn finns redan. Ange smeknamn, eller annan
                unik personlig identifikation för att särskilja dem.
              </p>
            </React.Fragment>
          )}
          {newName && newName.length < 2 && (
            <p className="text-alert mt-3">
              Namnet måste innehålla minst 2 tecken.
            </p>
          )}
          <Form.Check
            type="switch"
            id="is-terminated-switch"
            label="Har slutat"
            checked={isStudentTerminated}
            onChange={(e) => isTerminatedChange(e.target.checked)}
          />
        </Modal.Body>
        <Modal.Footer className="justify-content-between">
          <Button
            className="btn-danger mt-3"
            me-auto
            onClick={() =>
              onDeleteStudent(
                "STUDENT",
                "Är du säker? Eleven och alla dess loggar kommer raderas. Detta kan inte ångras",
              )
            }
          >
            Ta bort elev
          </Button>
          <Button
            disabled={disableButton}
            variant="primary"
            onClick={() => {
              onEditStudent({
                id: student!.id,
                name: newName!,
                isTerminated: isStudentTerminated,
              });
            }}
          >
            Spara
          </Button>
        </Modal.Footer>
      </Modal>
    </React.Fragment>
  );
};

export default EditStudentModal;
