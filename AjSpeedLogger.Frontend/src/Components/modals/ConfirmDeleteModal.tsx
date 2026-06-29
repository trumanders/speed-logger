import { Modal, Button } from "react-bootstrap";

interface ConfirmDeleteModalProps {
  show: boolean;
  onClose: () => void;
  onConfirm: () => void;
  type: string;
  message: string;
}

const ConfirmDeleteModal = ({
  show,
  onClose,
  onConfirm,
  type,
  message,
}: ConfirmDeleteModalProps) => {
  const deleteType = type === "STUDENT" ? "elev" : "";

  return (
    <Modal show={show} onHide={onClose}>
      <Modal.Header closeButton>
        <Modal.Title>Bekräfta radering av {deleteType}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <strong>{message}</strong>?
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onClose}>
          Avbryt
        </Button>
        <Button variant="danger" onClick={() => onConfirm()}>
          Ta bort
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default ConfirmDeleteModal;
