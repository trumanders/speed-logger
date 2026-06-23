import React, { useRef } from "react";
import type { Student } from "../types/student";
import { Button, Form, Modal } from "react-bootstrap";

interface EditStudentModalProps {
  show: boolean;
  student: Student | null;
  onHide: () => void;
  onEditStudent: (studentUpdate: Student) => void;
}

const EditStudentModal = ({
  show,
  student,
  onHide,
  onEditStudent,
}: EditStudentModalProps) => {
  const newName = useRef<HTMLInputElement>(null);

  return (
    <React.Fragment>
      <Modal show={show} onHide={onHide}>
        <Modal.Header closeButton>
          <Modal.Title>Redigera elev:</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Control defaultValue={student?.name} ref={newName} />
        </Modal.Body>
        <Modal.Footer>
          <Button
            variant="primary"
            onClick={() =>
              onEditStudent({
                id: student!.id,
                name: newName.current!.value,
                isTerminated: student!.isTerminated,
              })
            }
          >
            Spara
          </Button>
        </Modal.Footer>
      </Modal>
    </React.Fragment>
  );
};

export default EditStudentModal;
