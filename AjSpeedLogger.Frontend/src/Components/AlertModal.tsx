import { Button, Modal } from "react-bootstrap";

interface AlertModalProps {
  message: string;
  alertTitle: string | null;
  onClose: () => void;
}

const AlertModal = ({ message, onClose, alertTitle }: AlertModalProps) => {
  return (
    <>
      <Modal show={!!message} onHide={onClose}>
        <Modal.Header closeButton>
          <Modal.Title>{alertTitle || "Fel!"}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{message}</Modal.Body>
        <Modal.Footer>
          <Button
            onClick={() => {
              onClose();
            }}
          >
            Ok
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default AlertModal;
