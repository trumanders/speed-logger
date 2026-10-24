import { useEffect, useState } from "react";

interface LogTextareaProps {
  id: number;
  content: string;
  onTextChange: (id: number, newContent: string) => void;
}

const LogTextarea = ({ id, content, onTextChange }: LogTextareaProps) => {
  const [text, setText] = useState<string>(content);

  useEffect(() => {
    setText(content);
  }, [content]);

  return (
    <textarea
      value={text}
      key={`${id}`}
      rows={1}
      style={{ width: "90%", overflow: "hidden", resize: "none" }}
      onChange={(e) => {
        const target = e.target as HTMLTextAreaElement;
        target.style.height = "auto";
        target.style.height = `${target.scrollHeight}px`;
        onTextChange(id, target.value);
        setText(target.value);
      }}
    />
  );
};

export default LogTextarea;
