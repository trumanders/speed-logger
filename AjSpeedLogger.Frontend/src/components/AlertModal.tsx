import { Button, Modal } from "react-bootstrap";

interface AlertModalProps {
  message: string;
  alertTitle: string | null;
  onHide: () => void;
}

const AlertModal = ({ message, onHide, alertTitle }: AlertModalProps) => {
  return (
    <>
      <Modal show={!!message} onHide={onHide}>
        <Modal.Header closeButton>
          <Modal.Title>{alertTitle || "Fel!"}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{message}</Modal.Body>
        <Modal.Footer>
          <Button
            onClick={() => {
              onHide();
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
