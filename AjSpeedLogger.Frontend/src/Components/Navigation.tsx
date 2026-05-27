import Navbar from "react-bootstrap/Navbar";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import { Link, useNavigate } from "react-router"; // Importera useNavigate härifrån
import { NavLink } from "react-router-dom";

const Navigation = () => {
  const navigate = useNavigate();
  // Kolla om användaren är inloggad genom att se om token finns
  const isLoggedIn = !!localStorage.getItem("token");

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    navigate("/login");
  };

  return (
    <Navbar sticky="top" bg="dark" data-bs-theme="dark">
      <Container>
        <Navbar.Brand as={Link} to="/">
          Speed Logger
        </Navbar.Brand>
        <Nav className="ms-auto">
          {isLoggedIn ? (
            // Visas om man ÄR inloggad
            <Nav.Link
              as="button"
              onClick={handleLogout}
              className="btn btn-link nav-link"
            >
              Logga ut
            </Nav.Link>
          ) : (
            // Visas om man INTE är inloggad
            <>
              <Nav.Link as={NavLink} to="/login">
                Logga in
              </Nav.Link>
              <Nav.Link as={NavLink} to="/signup">
                Skapa konto
              </Nav.Link>
            </>
          )}
        </Nav>
      </Container>
    </Navbar>
  );
};

export default Navigation;
