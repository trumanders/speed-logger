import { Container, Row, Col } from "react-bootstrap";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import type { AcademicYearAllUserData } from "../types/allUserData";
import { getAcademicYears, getAllUserDataByAcademicYear } from "../services/api";
import ActionBar from "../Components/ActionBar";
import Sidebar from "../Components/Sidebar";
import Workspace from "../Components/Workspace";
import type { AcademicYear } from "../types/academicYear";

const HomePage = () => {
  const [isAutorized, setIsAutorized] = useState(false);
  const [username, setUserName] = useState<string | null>(null);
  const navigate = useNavigate();
  const [allUserData, setAllUserData] =
    useState<AcademicYearAllUserData | null>(null);
  const [academicYears, setAcademicYears] = useState<AcademicYear[]>([]);
  const [selectedStudent, setSelectedStudent] = useState<number | null>(null);
  const [selectedAcademicYearId, setSelectedAcademicYearId] = useState<number | null>(null);
  const [currentWorkspace, setCurrentWorkspace] = useState<string>("LOG");

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
        const academicYearAllUserData = await getAllUserDataByAcademicYear(null);
        setAllUserData(academicYearAllUserData);
        setAcademicYears(await getAcademicYears());
        if (academicYearAllUserData && academicYearAllUserData.id) {
          setSelectedAcademicYearId(academicYearAllUserData.id);
        }
      } catch (err) {
        // WHAT?
      }
    };
    if (isAutorized) fetchData();
  }, [isAutorized]);

  const handleSelectedStudent = (id: number) => {
    setSelectedStudent(id);
    setCurrentWorkspace("LOG");
  };

  const handleAcademicYearSelect = async (id: number) => {
    if (isAutorized) {
      const fetchAllUserData = await getAllUserDataByAcademicYear(id)      
      setAllUserData(fetchAllUserData);
    }
  }

  if (!isAutorized) {
    return null;
  }

  return (
    <>
      <ActionBar/>      
      <Container fluid className="mt-4">
        <Row>
          <Col md={3} className="border-end">
            <Sidebar
              allUserData={allUserData}
              onSelectStudent={handleSelectedStudent}
              onAcademicYearSelect={handleAcademicYearSelect}
              academicYears={academicYears}
              selectedAcademicYearId={selectedAcademicYearId}
              />
          </Col>
          <Col md={9} className="ps-4">
            <Workspace
              currentWorkspace={currentWorkspace}
              selectedStudent={selectedStudent}
              allUserData={allUserData} />
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default HomePage;
