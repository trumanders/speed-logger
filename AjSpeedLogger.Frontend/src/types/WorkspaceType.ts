export const WorkspaceType = {
  Log: "LOG",
  ManageStudents: "MANAGE_STUDENTS",
  ManageBaseLog: "MANAGE_BASELOG",
  AddLog: "ADD_LOG",
};

export type WorkspaceType = (typeof WorkspaceType)[keyof typeof WorkspaceType];
