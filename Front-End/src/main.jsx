import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom';
import './index.css'
import App from './App.jsx'
import { applyTheme, getSavedTheme } from './utils/theme.js';
import { applySavedPreferences } from './utils/preferences.js';

applySavedPreferences();
applyTheme(getSavedTheme());

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </StrictMode>,
)
