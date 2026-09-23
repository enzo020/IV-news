export function formatDate(date) {
  if (!date || date.startsWith("0001-01-01")) {
    return null;
  }

  return new Date(date).toLocaleDateString("pt-BR");
}
