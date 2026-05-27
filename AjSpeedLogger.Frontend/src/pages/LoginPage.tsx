import { Link, useNavigate } from "react-router";
import { useState } from "react";
import Form from "react-bootstrap/Form";
import { Button } from "react-bootstrap";
import type { LoginCredentials } from "../services/api";
import { login } from "../services/api";

const LoginPage = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    const credentials: LoginCredentials = { email, password };
    const data = await login(credentials); // Returns { token: "string"}

    if (data) {
      localStorage.setItem("token", data.token);
      localStorage.setItem("username", data.username);
      navigate("/");
    }
  };

  return (
    <>
      <Form onSubmit={handleLogin}>
        <h2>Logga in</h2>

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
        <Button variant="primary" type="submit">
          Logga in
        </Button>
      </Form>

      <p>
        <Link to="/signup">Har du inget konto?</Link>
      </p>
    </>
  );
};

export default LoginPage;
