import { Container, Row, Col } from "react-bootstrap";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import Sidebar from "../components/Sidebar";

const HomePage = () => {
  const [isAutorized, setIsAutorized] = useState(false);
  const [username, setUserName] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setIsAutorized(true);
      setUserName(localStorage.getItem("username"));
    } else {
      navigate("/login");
    }
  }, []);

  return (
    <>
      <Container fluid className="mt-4">
        <div>
          {isAutorized ? (
            <Row>
              {/* VÄNSTERSPALT: Tar upp 3 av 12 delar av skärmbredden */}
              <h3>{username}</h3>
              <Col md={3} className="border-end">
                <Sidebar />
              </Col>

              {/* HÖGERSPALT: Tar upp resten (9 av 12 delar) */}
              <Col md={9} className="ps-4">
                <h2>LOG FÖR ELEV KALLE KALLESSON</h2>
              </Col>
            </Row>
          ) : (
            <p>Du måste logga in för att se innehållet på denna sida.</p>
          )}
        </div>
      </Container>
    </>
  );
};

export default HomePage;
