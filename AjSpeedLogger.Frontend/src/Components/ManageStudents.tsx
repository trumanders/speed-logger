import { useOutletContext } from "react-router";
import type { ManageStudentsContext } from "../pages/HomePage";
import React from "react";

const ManageStudents = () => {
  const { allStudents } = useOutletContext<ManageStudentsContext>();

  return (
    <React.Fragment>
      <h4 className="mb-3">Hantera elever</h4>
      <div>
        {allStudents.map((s) => (
          <p>{s.name}</p>
        ))}
      </div>
    </React.Fragment>
  );
};

export default ManageStudents;
