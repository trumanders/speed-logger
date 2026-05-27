import Navbar from "react-bootstrap/Navbar";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav"; // <--- Lägg till denna import
import { Link } from "react-router";
import { NavLink } from "react-router-dom";

const Navigation = () => {
  return (
    <Navbar sticky="top" bg="dark" data-bs-theme="dark">
      <Container>
        <Navbar.Brand as={Link} to="/">
          Speed Logger
        </Navbar.Brand>
        <Nav className="ms-auto">
          {" "}
          {/* "ms-auto" trycker knappen till höger */}
          <Nav.Link as={NavLink} to="/login">
            Logga in
          </Nav.Link>
          <Nav.Link as={NavLink} to="/signup">
            Skapa konto
          </Nav.Link>
        </Nav>
      </Container>
    </Navbar>
  );
};

export default Navigation;
