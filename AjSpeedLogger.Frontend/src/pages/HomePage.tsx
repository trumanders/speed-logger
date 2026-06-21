import { Row, Col } from "react-bootstrap";
import { useEffect, useMemo, useState } from "react";
import { Outlet, useNavigate } from "react-router";
import type {
  AcademicYearAllUserData,
  BaseLogDay,
  StudentAllUserData,
} from "../types/allUserData";
import { addAcademicYear } from "../services/academicYearService";
import { addLog } from "../services/logService";
import { addStudent, getAllStudents } from "../services/studentService";
import { getAcademicYears } from "../services/academicYearService";
import { getAllUserDataByAcademicYear } from "../services/academicYearService";
import ActionBar from "../components/ActionBar";
import Sidebar from "../components/Sidebar";
import type { AcademicYearCreate } from "../types/academicYearCreate";
import type { AcademicYearName } from "../types/academicYearName";
import type { LogCreate } from "../types/logCreate";
import { AddStudentModal } from "../components/AddStudentModal";
import Spinner from "react-bootstrap/Spinner";
import type { Student } from "../types/student";
import ErrorModal from "../components/ErrorModal";
import type { AcademicYear } from "../types/academicYear";

export interface AddYearContext {
  onAddAcademicYear: (newAcademicYear: AcademicYearCreate) => void;
}

export interface AddLogContext {
  selectedAcademicYearId: number | null;
  selectedAcademicYearStudents: StudentAllUserData[] | null;
  allStudents: Student[] | undefined;
  onAddLog: (newLog: LogCreate) => void;
  onShowAddStudent: () => void;
  newStudentName: string;
  resetNewStudentName: () => void;
}

export interface LogContext extends BaseLogDaysContext {
  students: StudentAllUserData[];
}

export interface ManageBaseLogContext {
  allUserData: AcademicYearAllUserData | null;
}

export interface BaseLogDaysContext {
  baseLogDays: BaseLogDay[];
  academicYear: AcademicYear;
  onUpdateBaseLogDay: (id: number, newContent: string) => void;
}

export interface ManageStudentsContext {
  allStudents: Student[];
}

const HomePage = () => {
  const [error, setError] = useState<string | null>(null);
  const [errorTitle, setErrorTitle] = useState<string | null>(null);
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(true);
  const [allUserData, setAllUserData] =
    useState<AcademicYearAllUserData | null>(null);
  const [academicYears, setAcademicYears] = useState<AcademicYearName[]>([]);
  const [selectedAcademicYearId, setSelectedAcademicYearId] = useState<
    number | null
  >(null);
  const [showAddStudentModal, setShowAddStudentModal] =
    useState<boolean>(false);
  const [newStudentName, setNewStudentName] = useState<string>("");
  const [allStudents, setAllStudents] = useState<Student[]>([]);

  const handleSelectedStudent = (id: number) => {
    navigate(`/log/${id}`);
  };

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

  const handleAddLog = async (newLog: LogCreate) => {
    if (!newLog.newStudentName && !newLog.studentId)
      throw new Error("There is no student associated with the log.");

    // If a new student was added, create it and save to db
    try {
      if (!newLog.studentId && newLog.newStudentName) {
        const newStudent: Student = await addStudent({
          name: newLog.newStudentName,
        });
        setAllStudents((prev) => [...prev, newStudent]);
        newLog.studentId = newStudent.id;
      }

      await addLog(newLog);
      setAllUserData(
        await getAllUserDataByAcademicYear(selectedAcademicYearId),
      );
      navigate(`/log/${newLog.studentId}`);
    } catch (err: any) {
      const message = err.response?.data || "Ett fel uppstod";
      setError(message);
      setErrorTitle("Kunde inte lägga till loggen");
    }
  };

  const handleUpdateBaseLogDay = (id: number, newContent: string) => {
    // update the baselogdays-part of allUserData
    setAllUserData((prev) => {
      if (!prev) return null;
      return {
        ...prev,
        baseLogDays: prev.baseLogDays.map((baseLogDay) =>
          baseLogDay.id == id
            ? { ...baseLogDay, content: newContent }
            : baseLogDay,
        ),
      };
    });
  };

  const handleAddStudent = (name: string) => {
    setNewStudentName(name);
  };

  const handleShowAddStudent = () => {
    setShowAddStudentModal(true);
  };

  const resetNewStudentName = () => {
    setNewStudentName("");
  };

  const handleStudentClick = () => {
    navigate("/Log");
  };

  const handleBaseLogClick = () => {};

  const contexts = useMemo(
    () => ({
      // AddYearContext
      onAddAcademicYear: handleAddAcademicYear,

      // AddLogContext
      selectedAcademicYearId,
      selectedAcademicYearStudents: allUserData?.students,
      allStudents: allStudents,
      onAddLog: handleAddLog,
      onShowAddStudent: handleShowAddStudent,
      newStudentName,
      resetNewStudentName,

      // LogContext
      students: allUserData?.students,

      // LogContext / ManageBaseLogContext
      baseLogDays: allUserData?.baseLogDays,
      academicYear: academicYears.find(
        (ay) => ay.id === selectedAcademicYearId,
      ),
      onUpdateBaseLogDay: handleUpdateBaseLogDay,
    }),
    [
      allUserData,
      allStudents,
      selectedAcademicYearId,
      academicYears,
      newStudentName,
    ],
  );

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
        const fetchedAllStudents = await getAllStudents();
        setAllStudents(fetchedAllStudents);
      } catch (err) {
        console.error("Error fetching data:", err);
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
    setNewStudentName("");
  }, []);

  return (
    <>
      <ActionBar />
      <Row>
        <Col sm={4} className="border-end">
          {isLoading ? (
            <Spinner animation="border" role="status">
              <span className="visually-hidden">Loading...</span>
            </Spinner>
          ) : (
            <Sidebar
              selectedAcademicYearStudents={allUserData?.students ?? []}
              onSelectStudent={handleSelectedStudent}
              onAcademicYearSelect={handleAcademicYearSelect}
              academicYears={academicYears}
              selectedAcademicYearId={selectedAcademicYearId}
            />
          )}
        </Col>
        <Col sm={8} className="ps-4">
          <Outlet context={contexts} />
        </Col>
      </Row>

      <AddStudentModal
        show={showAddStudentModal}
        onHide={() => setShowAddStudentModal(false)}
        onAddStudent={handleAddStudent}
        allStudents={allStudents}
      />

      {error && (
        <ErrorModal
          message={error}
          errorTitle={errorTitle}
          onClose={() => {
            setError(null);
            setErrorTitle(null);
            // hejehj
          }}
        />
      )}
    </>
  );
};

export default HomePage;
