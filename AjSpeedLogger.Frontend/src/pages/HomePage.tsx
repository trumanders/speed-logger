import { Container } from "react-bootstrap";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";

const HomePage = () => {
  const [isAutorized, setIsAutorized] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setIsAutorized(true);
    } else {
      navigate("/login");
    }
  }, []);

  return (
    <>
      <Container>
        <div>
          {isAutorized ? (
            <>
              <h1>Välkommen till Speed Logger!</h1>
              <p>Visa loggar</p>
            </>
          ) : null}
        </div>
      </Container>
    </>
  );
};

export default HomePage;
