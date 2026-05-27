import { Link } from "react-router";
import Form from "react-bootstrap/Form";
import { Button } from "react-bootstrap";

const SignUpPage = () => {
  return (
    <>
      <Form>
        <h2>Skapa konto</h2>

        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>E-postadress</Form.Label>
          <Form.Control type="email" placeholder="Ange e-postadress" />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicPassword">
          <Form.Label>Lösenord</Form.Label>
          <Form.Control type="password" placeholder="Ange lösenord" />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formConfirmPassword">
          <Form.Label>Bekräfta lösenord</Form.Label>
          <Form.Control type="password" placeholder="Bekräfta lösenord" />
        </Form.Group>

        <Button variant="primary" type="submit">
          Skapa konto
        </Button>
      </Form>

      <p>
        <Link to="/login">Har du redan ett konto?</Link>
      </p>
    </>
  );
};

export default SignUpPage;
