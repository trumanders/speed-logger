import { Row, Col } from "react-bootstrap";
import { useEffect, useMemo, useState } from "react";
import { Outlet, useNavigate } from "react-router";
import type {
  AcademicYearAllUserData,
  BaseLogDay,
  StudentAllUserData,
} from "../types/allUserData";
import {
  addAcademicYear,
  updateAcademicYear,
} from "../services/academicYearService";
import { addLog } from "../services/logService";
import {
  addStudent,
  deleteStudent,
  getAllStudents,
  updateStudent,
} from "../services/studentService";
import { getAcademicYears } from "../services/academicYearService";
import { getAllUserDataByAcademicYear } from "../services/academicYearService";
import ActionBar from "../components/ActionBar";
import Sidebar from "../components/Sidebar";
import type { AcademicYearCreate } from "../types/academicYearCreate";
import type { LogCreate } from "../types/logCreate";
import { AddStudentModal } from "../components/modals/AddStudentModal";
import Spinner from "react-bootstrap/Spinner";
import type { Student } from "../types/student";
import type { AcademicYear } from "../types/academicYear";
import AlertModal from "../components/AlertModal";
import EditStudentModal from "../components/modals/EditStudentModal";
import ConfirmDeleteModal from "../components/modals/ConfirmDeleteModal";
import EditAcademicYearModal from "../components/modals/EditAcademicYearModal";

export interface AddYearContext {
  onAddAcademicYear: (newAcademicYear: AcademicYearCreate) => void;
}

export interface EditAcademicYearContext {
  handleEditAcademicYear: (id: number, academicYear: AcademicYear) => void;
  handleEditAcademicYearButtonClick: (academicYear: AcademicYear) => void;
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
  onEditStudentClick: (updateStudent: Student) => void;
}

const HomePage = () => {
  const [alert, setAlert] = useState<string | null>(null);
  const [alertTitle, setAlertTitle] = useState<string | null>(null);
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(true);
  const [allUserData, setAllUserData] =
    useState<AcademicYearAllUserData | null>(null);
  const [academicYears, setAcademicYears] = useState<AcademicYear[]>([]);
  const [selectedAcademicYearId, setSelectedAcademicYearId] = useState<
    number | null
  >(null);
  const [showAddStudentModal, setShowAddStudentModal] =
    useState<boolean>(false);
  const [showEditStudentModal, setShowEditStudentModal] =
    useState<boolean>(false);
  const [newStudentName, setNewStudentName] = useState<string>("");
  const [allStudents, setAllStudents] = useState<Student[]>([]);
  const [studentToEdit, setStudentToEdit] = useState<Student | undefined>(
    undefined,
  );
  const [showConfirmDeleteModal, setShowConfirmDeleteModal] = useState(false);
  const [deleteType, setDeleteType] = useState<string>("");
  const [deleteMessage, setDeleteMessage] = useState<string>("");
  const [academicYearToEdit, setAcademicYearToEdit] = useState<
    AcademicYear | undefined
  >(undefined);
  const [showEditAcademicYearModal, setShowEditAcademicYearModal] =
    useState<boolean>(false);

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
      setAlert(message);
      setAlertTitle("Kunde inte lägga till loggen");
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

  const handleEditStudentClick = async (studentUpdate: Student) => {
    setStudentToEdit(studentUpdate);
    setShowEditStudentModal(true);
  };

  const handleEditStudent = async (studentUpdate: Student) => {
    const updatedStudent = await updateStudent(studentUpdate);
    setAllStudents((prev) =>
      prev.map((s) => (s.id === updatedStudent.id ? updatedStudent : s)),
    );
    setAllUserData(await getAllUserDataByAcademicYear(selectedAcademicYearId));
    setShowEditStudentModal(false);
  };

  const handleChangeStudentIsTerminated = (
    studentId: number,
    isTerminated: boolean,
  ) => {
    setAllStudents((prev) =>
      prev.map((s) => (s.id === studentId ? { ...s, isTerminated } : s)),
    );
  };

  const handleDeleteStudent = (type: string, message: string) => {
    setDeleteType(type);
    setDeleteMessage(message);
    setShowConfirmDeleteModal(true);
  };

  const handleConfirmDeleteModal = async () => {
    if (deleteType === "STUDENT") {
      await deleteStudent(studentToEdit!.id);
      setAllStudents((prev) => prev.filter((s) => s.id !== studentToEdit!.id));
      setAllUserData(
        await getAllUserDataByAcademicYear(selectedAcademicYearId),
      );
      setShowConfirmDeleteModal(false);
      setShowEditStudentModal(false);
    }
  };

  const handleEditAcademicYear = async (academicYear: AcademicYear) => {
    const updatedAcademicYear = await updateAcademicYear(
      academicYear.id,
      academicYear,
    );
    academicYears;
  };

  const handleEditAcademicYearButtonClick = (academicYearId: number) => {
    const academicYear = academicYears.find((ay) => ay.id === academicYearId);
    if (academicYear) {
      setAcademicYearToEdit(academicYear);
      setShowEditAcademicYearModal(true);
    }
  };

  const handleAddStudent = (name: string) => {
    setNewStudentName(name);
    setShowAddStudentModal(false);
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

      // EditAcademicYearContext
      handleEditAcademicYear,
      handleEditAcademicYearButtonClick,

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

      // ManageStudent
      onEditStudentClick: handleEditStudentClick,
      handleEditStudent,
      handleDeleteStudent,
      handleChangeStudentIsTerminated,
    }),
    [
      allUserData,
      allStudents,
      selectedAcademicYearId,
      academicYears,
      newStudentName,
      handleChangeStudentIsTerminated,
      handleEditAcademicYear,
      handleEditAcademicYearButtonClick,
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
              onEditAcademicYearButtonClick={handleEditAcademicYearButtonClick}
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
      <EditStudentModal
        show={showEditStudentModal}
        onHide={() => setShowEditStudentModal(false)}
        student={studentToEdit}
        allStudents={allStudents}
        onEditStudent={handleEditStudent}
        onDeleteStudent={handleDeleteStudent}
      />
      <ConfirmDeleteModal
        show={showConfirmDeleteModal}
        onClose={() => setShowConfirmDeleteModal(false)}
        onConfirm={() => handleConfirmDeleteModal()}
        type={deleteType}
        message={deleteMessage}
      />

      {showEditAcademicYearModal && academicYearToEdit && (
        <EditAcademicYearModal
          academicYears={academicYears}
          academicYear={academicYearToEdit}
          show={showEditAcademicYearModal}
          onHide={() => setShowEditAcademicYearModal(false)}
          onConfirm={() => handleEditAcademicYear(academicYearToEdit)}
        />
      )}

      {alert && (
        <AlertModal
          message={alert}
          alertTitle={alertTitle}
          onHide={() => {
            setAlert(null);
            setAlertTitle(null);
          }}
        />
      )}
    </>
  );
};

export default HomePage;
