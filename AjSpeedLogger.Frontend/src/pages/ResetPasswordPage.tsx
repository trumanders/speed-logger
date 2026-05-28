import { useState } from "react";
import { useSearchParams, useNavigate } from "react-router";
import Form from "react-bootstrap/Form";
import { Button } from "react-bootstrap";
import axios from "axios";
import { resetPassword } from "../services/api";

const ResetPasswordPage = () => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [message, setMessage] = useState<string | null>(null);

  // Hämta token och email från URL (?token=xyz&email=test@test.com)
  const token = searchParams.get("token");
  const email = searchParams.get("email");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage(null);

    if (password !== confirmPassword) {
      setMessage("Lösenorden matchar inte.");
      return;
    }

    try {
      resetPassword(email!, token!, password);
      alert("Lösenordet har återställts! Du skickas nu till inloggningen.");
      navigate("/login");
    } catch (error: any) {
      setMessage(
        "Det gick inte och återställa lösenordet. Token kan ha löpt ut.",
      );
    }
  };

  return (
    <Form onSubmit={handleSubmit}>
      <h2>Välj nytt lösenord</h2>

      {message && <p style={{ color: "red", fontWeight: "bold" }}>{message}</p>}

      <Form.Group className="mb-3" controlId="newPassword">
        <Form.Label>Nytt lösenord</Form.Label>
        <Form.Control
          type="password"
          placeholder="Ange nytt lösenord"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="confirmNewPassword">
        <Form.Label>Bekräfta nytt lösenord</Form.Label>
        <Form.Control
          type="password"
          placeholder="Bekräfta lösenord"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          required
        />
      </Form.Group>

      <Button variant="primary" type="submit">
        Spara nytt lösenord
      </Button>
    </Form>
  );
};

export default ResetPasswordPage;
