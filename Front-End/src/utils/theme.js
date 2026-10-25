const THEME_KEY = "ivnews-theme";
const DEFAULT_THEME = "dark";

export function getSavedTheme() {
  const savedTheme = localStorage.getItem(THEME_KEY);

  return savedTheme === "light" || savedTheme === "dark"
    ? savedTheme
    : DEFAULT_THEME;
}

export function applyTheme(theme) {
  const validTheme = theme === "light" || theme === "dark" ? theme : DEFAULT_THEME;

  document.documentElement.dataset.theme = validTheme;
  localStorage.setItem(THEME_KEY, validTheme);

  return validTheme;
}