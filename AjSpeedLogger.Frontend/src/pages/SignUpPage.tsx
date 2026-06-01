import { Link, useNavigate } from "react-router";
import { useState } from "react";
import Form from "react-bootstrap/Form";
import { Alert, Button } from "react-bootstrap";
import { signup } from "../services/api";

const SignUpPage = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleSignUp = async (e: React.FormEvent) => {
    e.preventDefault();
    setErrorMessage(null);

    if (password !== confirmPassword) {
      setErrorMessage("Lösenorden matchar inte.");
      return;
    }

    try {
      const credentials = { email, password };
      await signup(credentials);
      navigate("/login", {state: {successMessage: "Konto har skapats. Du kan nu logga in."}});
    } catch (error: any) {
      if (error.response && error.response.data) {
        const backendData = error.response.data;

        // Om .NET Identity skickar en array av fel (t.ex. [{ description: "..." }])
        if (Array.isArray(backendData)) {
          const messages = backendData
            .map((err: any) => {
              if (
                err.code === "DuplicateUserName" ||
                err.code === "DuplicateEmail"
              )
                return "E-postadressen är redan registrerad.";
              if (err.code === "PasswordTooShort")
                return "Lösenordet är för kort.";
              if (err.code === "PasswordRequiresNonAlphanumeric")
                return "Lösenordet måste innehålla minst ett specialtecken.";
              if (err.code === "PasswordRequiresDigit")
                return "Lösenordet måste innehålla minst en siffra.";
              if (err.code === "PasswordRequiresLower")
                return "Lösenordet måste innehålla minst en liten bokstav.";
              if (err.code === "PasswordRequiresUpper")
                return "Lösenordet måste innehålla minst en stor bokstav.";
              return err.description || "Ett okänt fel inträffade.";
            });

            const uniqueMessages = [...new Set(messages)].join("\n");
          setErrorMessage(uniqueMessages);
        } else {
          setErrorMessage(
            "Registreringen misslyckades. Kontrollera lösenordskraven.",
          );
        }
      } else {
        setErrorMessage("Kunde inte ansluta till servern.");
      }
    }
  };

  return (
    <>
      <Form onSubmit={handleSignUp}>
        <h2>Skapa konto</h2>

        {/* Visar felmeddelanden från valideringen eller servern */}
        {errorMessage && (
          <Alert variant="danger" onClose={() => setErrorMessage(null)} dismissible>
            {errorMessage.split("\n").map((msg, idx) => (
              <div key={idx}>{msg}</div>  
            ))}
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

        <Form.Group className="mb-3" controlId="formConfirmPassword">
          <Form.Label>Bekräfta lösenord</Form.Label>
          <Form.Control
            type="password"
            placeholder="Bekräfta lösenord"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </Form.Group>

        <Button variant="primary" type="submit">
          Skapa konto
        </Button>
      </Form>

      <p style={{ marginTop: "15px" }}>
        <Link to="/login">Har du redan ett konto?</Link>
      </p>
    </>
  );
};

export default SignUpPage;
