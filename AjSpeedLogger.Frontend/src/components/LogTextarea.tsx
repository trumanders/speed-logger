import { useEffect, useState } from "react";

interface LogTextareaProps {
  id: number;
  content: string;
  baseContent?: string | null;
  onTextChange?: (id: number, newContent: string) => void;
}

const LogTextarea = ({
  id,
  content,
  baseContent,
  onTextChange,
}: LogTextareaProps) => {
  const isReadOnly = Boolean(baseContent);
  const [text, setText] = useState<string>(baseContent || content);

  useEffect(() => {
    setText(baseContent || content);
  }, [baseContent, content]);

  return (
    <textarea
      readOnly={isReadOnly}
      value={text}
      rows={1}
      ref={(node) => {
        if (node) {
          node.style.height = "auto";
          node.style.height = `${node.scrollHeight}px`;
        }
      }}
      style={{
        width: "100%",
        overflow: "hidden",
        resize: "none",
        ...(isReadOnly ? { border: "3px solid red", outline: "none" } : {}),
      }}
      onChange={(e) => {
        if (isReadOnly) return;
        const target = e.target as HTMLTextAreaElement;
        target.style.height = "auto";
        target.style.height = `${target.scrollHeight}px`;
        setText(target.value);
        if (onTextChange) {
          onTextChange(id, target.value);
        }
      }}
    />
  );
};

export default LogTextarea;
