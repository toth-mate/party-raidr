export const toMap = <T>(arr: T[]): Map<T, number> => {
  let idx = 0;
  const result: Map<T, number> = new Map();

  for (const val of arr) {
    result.set(val, idx);
    idx++;
  }

  return result;
};
