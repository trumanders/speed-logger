import { Container } from "react-bootstrap";
import { Navigate, Outlet, Route, Routes } from "react-router";
import LogInPage from "./pages/LoginPage";
import Navigation from "./components/Navigation";
import SignUpPage from "./pages/SignUpPage";
import HomePage from "./pages/HomePage";
import ResetPasswordPage from "./pages/ResetPasswordPage";
import ForgotPasswordPage from "./pages/ForgotPasswordPage";
import { jwtDecode } from "jwt-decode";
import AddYear from "./components/AddYear";
import ManageStudents from "./components/ManageStudents";
import ManageBaseLog from "./components/ManageBaseLog";
import AddLog from "./components/AddLog";
import Log from "./components/Log";

const ProtectedRoute = () => {
  const token = localStorage.getItem("token");
  if (!token) return <Navigate to="/login" replace />;
  try {
    const decodedToken: { exp: number } = jwtDecode(token);
    const isValid = decodedToken.exp * 1000 > Date.now();
    if (!isValid) {
      localStorage.removeItem("token");
      localStorage.removeItem("username");
      return <Navigate to="/login" replace />;
    }
  } catch {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    return <Navigate to="/login" replace />;
  }
  return <Outlet />;
};

const App = () => {
  return (
    <>
      <Container>
        <Navigation />
        <Routes>
          <Route element={<ProtectedRoute />}>
            <Route path="/" element={<HomePage />}>
              <Route path="log/:studentId" element={<Log />} />
              <Route path="add-year" element={<AddYear />} />
              <Route path="manage-baselog" element={<ManageBaseLog />} />
              <Route path="manage-students" element={<ManageStudents />} />
              <Route path="add-log" element={<AddLog />} />
            </Route>
          </Route>
          <Route path="/login" element={<LogInPage />} />
          <Route path="/signup" element={<SignUpPage />} />
          <Route path="/reset-password" element={<ResetPasswordPage />} />
          <Route path="/forgot-password" element={<ForgotPasswordPage />} />
        </Routes>
      </Container>
    </>
  );
};

export default App;
