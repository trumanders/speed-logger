import { useState } from "react";
import Form from "react-bootstrap/Form";
import { Button } from "react-bootstrap";
import { forgotPassword } from "../services/api";

const ForgotPasswordPage = () => {
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState<string | null>(null);
  const [isError, setIsError] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage(null);
    setIsError(false);

    try {
      await forgotPassword(email);
      setMessage(
        "Om e-postadressen existerar har en återställningslänk skickats (kolla backend-loggen!).",
      );
    } catch (error: any) {
      setIsError(true);
      setMessage("Något gick fel. Försök igen.");
    }
  };

  return (
    <Form onSubmit={handleSubmit}>
      <h2>Glömt lösenord</h2>
      <p style={{ color: "#666" }}>
        Ange din e-postadress så skickar vi en länk för att återställa ditt
        lösenord.
      </p>

      {message && (
        <p style={{ color: isError ? "red" : "green", fontWeight: "bold" }}>
          {message}
        </p>
      )}

      <Form.Group className="mb-3" controlId="forgotEmail">
        <Form.Label>E-postadress</Form.Label>
        <Form.Control
          type="email"
          placeholder="Ange din e-post"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </Form.Group>

      <Button variant="primary" type="submit">
        Skicka återställningslänk
      </Button>
    </Form>
  );
};

export default ForgotPasswordPage;
