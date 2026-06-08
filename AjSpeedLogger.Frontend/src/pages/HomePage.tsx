import { Row, Col } from "react-bootstrap";
import { useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router";
import type { AcademicYearAllUserData } from "../types/allUserData";
import {
  addAcademicYear,
  addLog,
  addStudent,
  getAcademicYears,
  getAllUserDataByAcademicYear,
} from "../services/api";
import ActionBar from "../components/ActionBar";
import Sidebar from "../components/Sidebar";
import Workspace from "../components/Workspace";
import type { AcademicYearCreate } from "../types/academicYearCreate";
import type { AcademicYearName } from "../types/academicYearName";
import type { LogCreate } from "../types/logCreate";
import { AddStudentModal } from "../components/AddStudentModal";
import Spinner from "react-bootstrap/Spinner";
import type { Student } from "../types/student";
import ErrorModal from "../components/ErrorModal";

const HomePage = () => {
  const [error, setError] = useState<string | null>(null);
  const [errorTitle, setErrorTitle] = useState<string | null>(null);
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(true);
  const [allUserData, setAllUserData] =
    useState<AcademicYearAllUserData | null>(null);
  const [academicYears, setAcademicYears] = useState<AcademicYearName[]>([]);
  const [selectedStudent, setSelectedStudent] = useState<number | null>(null);
  const [selectedAcademicYearId, setSelectedAcademicYearId] = useState<
    number | null
  >(null);
  const [showAddStudentModal, setShowAddStudentModal] =
    useState<boolean>(false);
  const [newStudentName, setNewStudentName] = useState<string>("");

  useEffect(() => {
    const fetchData = async () => {
      try {
        setIsLoading(true);
        const fetchAcademicYears = await getAcademicYears();
        const localStorageAcademicYearId = localStorage.getItem(
          "selectedAcademicYearId",
        );
        const academicYearSavedId = localStorageAcademicYearId
          ? parseInt(localStorageAcademicYearId)
          : null;
        const validId =
          academicYearSavedId &&
          fetchAcademicYears.find((ay) => ay.id === Number(academicYearSavedId))
            ? academicYearSavedId
            : null;

        if (!validId) localStorage.removeItem("selectedAcademicYearId");

        setSelectedAcademicYearId(validId);
        setAllUserData(await getAllUserDataByAcademicYear(validId));
        setAcademicYears(fetchAcademicYears);
      } catch (err) {
        console.error("Error fetching data:", err);
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
    setNewStudentName("");
  }, []);

  const handleSelectedStudent = (id: number) => {
    setSelectedStudent(id);
  };

  // When user selects a year int the dropdown, fetch, set selected year and store id in local storage.
  const handleAcademicYearSelect = async (id: number) => {
    const fetchAllUserData = await getAllUserDataByAcademicYear(id);
    setAllUserData(fetchAllUserData);
    setSelectedAcademicYearId(id);
    localStorage.setItem("selectedAcademicYearId", id.toString());
  };

  const handleAddAcademicYear = async (newAcademicYear: AcademicYearCreate) => {
    const response = await addAcademicYear(newAcademicYear);
    setAllUserData(await getAllUserDataByAcademicYear(response.id));
    setAcademicYears([...academicYears, response]);
    setSelectedAcademicYearId(response.id);
    localStorage.setItem("selectedAcademicYearId", response.id.toString());
    navigate("/");
  };

  const studentList = useMemo(
    () => allUserData?.students.map((s) => s),
    [allUserData],
  );

  const handleAddLog = async (newLog: LogCreate) => {
    if (!newLog.newStudentName && !newLog.studentId)
      throw new Error("There is no student associated with the log.");

    // If a new student was added, create it and save to db
    try {
      if (newLog.newStudentName) {
        const newStudent: Student = await addStudent({
          name: newLog.newStudentName,
        });
        newLog.studentId = newStudent.id;
      }

      await addLog(newLog);
      setSelectedStudent(newLog.studentId);
      setAllUserData(
        await getAllUserDataByAcademicYear(selectedAcademicYearId),
      );
    } catch (err: any) {
      const message = err.response?.data || "Ett fel uppstod";
      setError(message);
      setErrorTitle("Kunde inte lägga till loggen");
    }
  };

  const handleAddStudent = (name: string) => {
    setNewStudentName(name);
  };

  const handleShowAddStudent = () => {
    setShowAddStudentModal(true);
  };

  const handleResetNewStudentName = () => {
    setNewStudentName("");
  };

  const handleStudentClick = () => {
    navigate("/Log");
  };

  const handleBaseLogClick = () => {};

  return (
    <>
      <ActionBar />
      {allUserData ? "inte null" : "null"}
      <Row>
        <Col sm={4} className="border-end">
          {isLoading ? (
            <Spinner animation="border" role="status">
              <span className="visually-hidden">Loading...</span>
            </Spinner>
          ) : (
            <Sidebar
              allUserData={allUserData}
              onSelectStudent={handleSelectedStudent}
              onAcademicYearSelect={handleAcademicYearSelect}
              academicYears={academicYears}
              selectedAcademicYearId={selectedAcademicYearId}
            />
          )}
        </Col>
        <Col sm={8} className="ps-4">
          <Workspace
            selectedStudent={selectedStudent}
            allUserData={allUserData}
            onAddAcademicYear={handleAddAcademicYear}
            academicYearId={selectedAcademicYearId}
            studentList={studentList}
            onAddLog={handleAddLog}
            onShowAddStudent={handleShowAddStudent}
            newStudentName={newStudentName}
            resetNewStudentName={handleResetNewStudentName}
          />
        </Col>
      </Row>

      <AddStudentModal
        show={showAddStudentModal}
        onHide={() => setShowAddStudentModal(false)}
        onAddStudent={handleAddStudent}
      />

      {error && (
        <ErrorModal
          message={error}
          errorTitle={errorTitle}
          onClose={() => {
            setError(null);
            setErrorTitle(null);
          }}
        />
      )}
    </>
  );
};

export default HomePage;
