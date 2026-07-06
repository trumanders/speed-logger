const startYear = new Date().getFullYear() - 3;

export const academicYearOptions = Array.from(
  { length: 8 },
  (_, i) => `${startYear + i}/${startYear + i + 1}`,
);
