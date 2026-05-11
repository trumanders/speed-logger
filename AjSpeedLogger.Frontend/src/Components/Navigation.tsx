import Navbar from "react-bootstrap/Navbar";
import Container from "react-bootstrap/Container";
import { Link } from "react-router";

const Navigation = () => {
  return (
    <Navbar sticky="top" bg="dark" data-bs-theme="dark">
      <Container>
        <Navbar.Brand as={Link} to="/">
          Speed Logger
        </Navbar.Brand>
      </Container>
    </Navbar>
  );
};

export default Navigation;
