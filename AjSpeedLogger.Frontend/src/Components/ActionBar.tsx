import { Button, Stack } from "react-bootstrap";

interface ActionBarProps {
  onAddAcademicYearClick?: () => void;
  onStudentsClick?: () => void;
  onBaseLogClick?: () => void;
}

const ActionBar = ({
  onAddAcademicYearClick,
  onStudentsClick,
  onBaseLogClick,
}: ActionBarProps) => {
  return (
    <div className="bg-light py-1 px-3 border-bottom w-100">
      <Stack direction="horizontal" gap={2}>

        <Button variant="outline-primary" size="sm" onClick={onAddAcademicYearClick}>
          Skapa nytt läsår
        </Button>

        <Button variant="outline-primary" size="sm" onClick={onStudentsClick}>
          Elever
        </Button>

        <Button variant="outline-primary" size="sm" onClick={onBaseLogClick}>
          Baslog
        </Button>

      </Stack>
    </div>
  );
};

export default ActionBar;
