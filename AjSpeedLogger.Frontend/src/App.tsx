import { Container } from "react-bootstrap";
import { Route, Routes } from "react-router";
import LogInPage from "./pages/LoginPage";
import Navigation from "./components/Navigation";
import SignUpPage from "./pages/SignUpPage";
import HomePage from "./pages/HomePage";

const App = () => {
  return (
    <>
      <Container>
        <Navigation />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LogInPage />} />
          <Route path="/signup" element={<SignUpPage />} />
        </Routes>
      </Container>
    </>
  );
};

export default App;
