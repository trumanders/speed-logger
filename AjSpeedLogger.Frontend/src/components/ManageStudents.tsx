import { useOutletContext } from "react-router";
import type { ManageStudentsContext } from "../pages/HomePage";
import React from "react";
import { Stack } from "react-bootstrap";
import "../assets/scss/App.scss";
import EditStudentButton from "./buttons/EditStudentButton";

const ManageStudents = () => {
  const { allStudents } = useOutletContext<ManageStudentsContext>();

  return (
    <React.Fragment>
      <h4 className="mb-3">Hantera elever</h4>
      <div>
        {allStudents
          .sort((a, b) => a.name.localeCompare(b.name))
          .map((s) => (
            <Stack direction="horizontal">
              <p className="m-0">{s.name}</p>
              <EditStudentButton student={s} />
            </Stack>
          ))}
      </div>
    </React.Fragment>
  );
};

export default ManageStudents;
