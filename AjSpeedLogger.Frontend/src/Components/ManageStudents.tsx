import { useOutletContext, useSearchParams } from "react-router";
import type { ManageStudentsContext } from "../pages/HomePage";
import React, { useState } from "react";
import { Stack } from "react-bootstrap";
import "../assets/scss/App.scss";

const ManageStudents = () => {
  const { allStudents } = useOutletContext<ManageStudentsContext>();
  const { onEditStudentClick } = useOutletContext<ManageStudentsContext>();

  return (
    <React.Fragment>
      <h4 className="mb-3">Hantera elever</h4>
      <div>
        {allStudents.map((s) => (
          <Stack direction="horizontal">
            <p className="m-0">{s.name}</p>
            <button
              className="btn p-0 border-0 ms-auto"
              onClick={() => onEditStudentClick(s)}
            >
              <img src="/images/edit.png" className="edit-icon" />
            </button>
          </Stack>
        ))}
      </div>
    </React.Fragment>
  );
};

export default ManageStudents;
