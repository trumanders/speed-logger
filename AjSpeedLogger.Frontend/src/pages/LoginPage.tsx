import { Link, useNavigate } from "react-router";
import { useState } from "react";
import Form from "react-bootstrap/Form";
import { Button, Spinner } from "react-bootstrap";
import type { LoginCredentials } from "../services/api";
import { login } from "../services/api";
import Alert from "react-bootstrap/Alert";
import { useLocation } from "react-router";

const LoginPage = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const successMessage = location.state?.successMessage;

  const handleLogin = async (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();
    setIsLoading(true)
    const credentials: LoginCredentials = { email, password };
    try {
      const data = await login(credentials); // Returns { token: "string"}

      if (data) {
        localStorage.setItem("token", data.token);
        localStorage.setItem("username", data.username);
        navigate("/");
      }
    } catch (err: any) {
      if (err.response && err.response.status === 401) {
        setError("Fel användarnamn eller lösenord.");
      } else {
        setError(
          err.response?.data?.message ||
            "Ett fel inträffade. Försök igen senare.",
        );
      }
    }
    setIsLoading(false);
  };

  return (
    <>
      {successMessage && <Alert variant="success">{successMessage}</Alert>}
      <Form onSubmit={handleLogin}>
        <h2>Logga in</h2>

        {error && (
          <Alert variant="danger" onClose={() => setError(null)} dismissible>
            {error}
          </Alert>
        )}

        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>E-postadress</Form.Label>
          <Form.Control
            type="email"
            placeholder="Ange e-postadress"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicPassword">
          <Form.Label>Lösenord</Form.Label>
          <Form.Control
            type="password"
            placeholder="Ange lösenord"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </Form.Group>
        <p>
          <Link to="/forgot-password">Glömt lösenord?</Link>
        </p>
        <Button variant="primary" disabled={isLoading} type="submit">
          {isLoading ? (
            <>
              <Spinner size="sm" animation="border" className="me-2" />
            </>
          ) : (
            "Logga in"
          )}
        </Button>
      </Form>
    </>
  );
};

export default LoginPage;
