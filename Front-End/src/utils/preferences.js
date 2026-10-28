const TEXT_SIZE_KEY = "ivnews-text-size";
const NEWS_LAYOUT_KEY = "ivnews-news-layout";

const DEFAULT_TEXT_SIZE = "medium";
const DEFAULT_NEWS_LAYOUT = "grid";

const VALID_TEXT_SIZES = ["small", "medium", "large"];
const VALID_NEWS_LAYOUTS = ["grid", "list"];

export function getSavedTextSize() {
  const saved = localStorage.getItem(TEXT_SIZE_KEY);

  return VALID_TEXT_SIZES.includes(saved) ? saved : DEFAULT_TEXT_SIZE;
}

export function getSavedNewsLayout() {
  const saved = localStorage.getItem(NEWS_LAYOUT_KEY);

  return VALID_NEWS_LAYOUTS.includes(saved) ? saved : DEFAULT_NEWS_LAYOUT;
}

export function applyTextSize(size) {
  const validSize = VALID_TEXT_SIZES.includes(size) ? size : DEFAULT_TEXT_SIZE;

  localStorage.setItem(TEXT_SIZE_KEY, validSize);
  document.documentElement.dataset.newsTextSize = validSize;

  return validSize;
}

export function applyNewsLayout(layout) {
  const validLayout = VALID_NEWS_LAYOUTS.includes(layout)
    ? layout
    : DEFAULT_NEWS_LAYOUT;

  localStorage.setItem(NEWS_LAYOUT_KEY, validLayout);
  document.documentElement.dataset.newsLayout = validLayout;

  return validLayout;
}

export function applySavedPreferences() {
  applyTextSize(getSavedTextSize());
  applyNewsLayout(getSavedNewsLayout());
}
