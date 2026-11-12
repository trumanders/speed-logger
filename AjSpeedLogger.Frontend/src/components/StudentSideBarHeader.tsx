import { Link } from "react-router";

const StudentSidebarHeader = () => {
  return (
    <>
      <div className="d-flex justify-content-center">
        <Link to="/add-log" className="btn btn-outline-primary btn-sm">
          Lägg till logg
        </Link>
      </div>
      <h5 className="ms-4"></h5>
    </>
  );
};

export default StudentSidebarHeader;
