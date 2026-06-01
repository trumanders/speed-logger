import { Container, Row, Col } from "react-bootstrap";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import Sidebar from "../components/Sidebar";
import type { AcademicYearAllUserDataDto } from "../types/allUserData";
import { getUserDataForCurrentYear } from "../services/api";

const HomePage = () => {
  const [isAutorized, setIsAutorized] = useState(false);
  const [username, setUserName] = useState<string | null>(null);
  const navigate = useNavigate();
  const [allUserData, setAllUserData] =
    useState<AcademicYearAllUserDataDto | null>(null);
  const [selectedStudent, setSelectedStudent] = useState<number | null>(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setIsAutorized(true);
      setUserName(localStorage.getItem("username"));
    } else {
      navigate("/login");
    }
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const academicYearAllUserData = await getUserDataForCurrentYear();
        setAllUserData(academicYearAllUserData);
      } catch (err) {
        // WHAT?
      }
    };
    if (isAutorized) fetchData();
  }, [isAutorized]);

  return (
    <>
      <Container fluid className="mt-4">
        <div>
          {isAutorized &&
          allUserData?.students &&
          allUserData.students.length > 0 ? (
            <Row>
              <Col md={3} className="border-end">
                <Sidebar allUserData={allUserData} />
              </Col>

              <Col md={9} className="ps-4">
                <h2>LOG här</h2>
              </Col>
            </Row>
          ) : (
            <p>Du har inga loggar.</p>
          )}
        </div>
      </Container>
    </>
  );
};

export default HomePage;
