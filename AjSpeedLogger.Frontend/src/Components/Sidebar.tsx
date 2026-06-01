import { ListGroup } from "react-bootstrap";
import type { AcademicYearAllUserDataDto } from "../types/allUserData";

interface SideBarProps {
  allUserData: AcademicYearAllUserDataDto | null;
}

const Sidebar = ({ allUserData }: SideBarProps) => {
  return (
    <>
      <div>
        <h3>{allUserData ? allUserData.name : ""}</h3>
      </div>
      <ListGroup>
        {allUserData?.students && allUserData.students.length > 0 ? (
          allUserData.students.map((student) => (
            <ListGroup.Item
              key={student.id}
              action
              href={`/students/${student.id}`}
            >
              {student.log.lessonStartTime.substring(0, 5)} {student.name}
            </ListGroup.Item>
          ))
        ) : (
          <ListGroup.Item disabled>{allUserData?.name}</ListGroup.Item>
        )}
      </ListGroup>
    </>
  );
};

export default Sidebar;
