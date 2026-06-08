import { useState } from "react";
import { Modal, Button, Form } from "react-bootstrap";

interface AddStudentModalProps {
  show: boolean;
  onHide: () => void;
  onAddStudent: (name: string) => void;
}

export const AddStudentModal = ({
  show,
  onHide,
  onAddStudent,
}: AddStudentModalProps) => {
  const [name, setName] = useState("");

  return (
    <Modal show={show} onHide={onHide}>
      <Modal.Header closeButton>
        <Modal.Title>Lägg till ny elev</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form.Control
          placeholder="Namn"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onHide}>
          Avbryt
        </Button>
        <Button
          variant="primary"
          onClick={() => {
            onAddStudent(name);
            setName("");
          }}
        >
          Spara
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
