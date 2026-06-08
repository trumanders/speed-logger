import { Button, Modal } from "react-bootstrap";

interface ErrorModalProps {
  message: string;
  errorTitle: string | null;
  onClose: () => void;
}

const ErrorModal = ({ message, onClose, errorTitle }: ErrorModalProps) => {
  return (
    <>
      <Modal show={!!message} onHide={onClose}>
        <Modal.Header closeButton>
          <Modal.Title>{errorTitle || "Fel!"}</Modal.Title>
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

export default ErrorModal;
