import { Button, Stack } from "react-bootstrap";
import { Link } from "react-router-dom";

const ActionBar = () => {
  return (
    <div className="bg-light py-1 px-3 border-bottom w-100">
      <Stack direction="horizontal" gap={2}>
        <Link to="/add_year" className="btn btn-outline-primary btn-sm">Nytt läsår</Link>
        <Link to="/manage_baselog" className="btn btn-outline-primary btn-sm">Baslog</Link>
        <Link to="/manage_students" className="btn btn-outline-primary btn-sm">Elever</Link>
      </Stack>
    </div>
  );
};

export default ActionBar;
