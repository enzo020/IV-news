import { useState } from "react";
import { applyTheme, getSavedTheme } from "../utils/theme";
import { getSavedTextSize, getSavedNewsLayout, applyTextSize, applyNewsLayout } from "../utils/preferences";

function Settings() {
  const [selectedTheme, setSelectedTheme] = useState(() => getSavedTheme());
  const [newsLayout, setNewsLayout] = useState(() => getSavedNewsLayout());
  const [textSize, setTextSize] = useState(() => getSavedTextSize());

  function selectTextSize(size) {
    setTextSize(applyTextSize(size));
  }

  function selectNewsLayout(layout) {
    setNewsLayout(applyNewsLayout(layout));
  }

  function selectTheme(theme) {
    const appliedTheme = applyTheme(theme);
    setSelectedTheme(appliedTheme);
  }

  return (
    <main className="settings-page">
      <header className="settings-header">
        <div>
          <span className="settings-eyebrow">IV NEWS</span>
          <h1>Configurações</h1>
          <p>Personalize sua experiência de leitura.</p>
        </div>

        <div className="settings-header-icon" aria-hidden="true">
          <i className="bi bi-gear-wide-connected"></i>
        </div>
      </header>

      <section className="settings-section">
        <div className="settings-section-heading">
          <div className="settings-section-icon">
            <span aria-hidden="true">◐</span>
          </div>

          <div>
            <h2>Aparência</h2>
            <p>Escolha como o IV News será exibido.</p>
          </div>
        </div>

        <div className="settings-card">
          <div className="settings-card-heading">
            <div>
              <h3>Tema da interface</h3>
              <p>Selecione a aparência que prefere para ler suas notícias.</p>
            </div>

            <span className="settings-tag">Visual</span>
          </div>

          <div className="theme-options">
            {/* Modo escuro */}
            <button
              type="button"
              className={`theme-option ${
                selectedTheme === "dark" ? "selected" : ""
              }`}
              onClick={() => selectTheme("dark")}
              aria-pressed={selectedTheme === "dark"}
            >
              <div className="theme-preview theme-preview-dark">
                <div className="preview-topbar">
                  <span />
                  <span />
                </div>

                <div className="preview-content">
                  <div className="preview-sidebar" />

                  <div className="preview-lines">
                    <span />
                    <span />
                    <span />
                  </div>
                </div>
              </div>

              <div className="theme-option-footer">
                <div className="theme-option-label">
                  <span className="theme-radio">
                    {selectedTheme === "dark" && <span />}
                  </span>

                  <div>
                    <strong>Modo escuro</strong>
                    <small>Mais confortável em ambientes escuros</small>
                  </div>
                </div>

                {selectedTheme === "dark" && (
                  <span className="theme-check">✓</span>
                )}
              </div>
            </button>

            {/* Modo claro */}
            <button
              type="button"
              className={`theme-option ${
                selectedTheme === "light" ? "selected" : ""
              }`}
              onClick={() => selectTheme("light")}
              aria-pressed={selectedTheme === "light"}
            >
              <div className="theme-preview theme-preview-light">
                <div className="preview-topbar">
                  <span />
                  <span />
                </div>

                <div className="preview-content">
                  <div className="preview-sidebar" />

                  <div className="preview-lines">
                    <span />
                    <span />
                    <span />
                  </div>
                </div>
              </div>

              <div className="theme-option-footer">
                <div className="theme-option-label">
                  <span className="theme-radio">
                    {selectedTheme === "light" && <span />}
                  </span>

                  <div>
                    <strong>Modo claro</strong>
                    <small>Uma interface clara e luminosa</small>
                  </div>
                </div>

                {selectedTheme === "light" && (
                  <span className="theme-check">✓</span>
                )}
              </div>
            </button>
          </div>

          <div className="settings-note">
            <span aria-hidden="true">ⓘ</span>
            <p>
              Sua preferência de aparência é aplicada em toda a plataforma e
              salva automaticamente neste navegador.
            </p>
          </div>
        </div>
      </section>

      <div className="settings-preferences">
        <div className="settings-preference">
          <div className="preference-icon">Aa</div>

          <div className="preference-description">
            <h3>Tamanho do texto</h3>
            <p>Ajuste o tamanho da fonte das notícias.</p>
          </div>

          <div className="preference-control">
            <label htmlFor="text-size">Tamanho</label>
            <select
              id="text-size"
              value={textSize}
              onChange={(event) => selectTextSize(event.target.value)}
            >
              <option value="small">Pequeno</option>
              <option value="medium">Médio</option>
              <option value="large">Grande</option>
            </select>
          </div>
        </div>

        <div className="settings-preference">
          <div className="preference-icon">▤</div>

          <div className="preference-description">
            <h3>Exibição das notícias</h3>
            <p>Personalize como os artigos aparecem no feed.</p>
          </div>

          <div className="preference-control">
            <label htmlFor="news-layout">Visualização</label>
            <select
              id="news-layout"
              value={newsLayout}
              onChange={(event) => selectNewsLayout(event.target.value)}
            >
              <option value="grid">Grade</option>
              <option value="list">Lista</option>
            </select>
          </div>
        </div>

        <div className="settings-preference">
          <div className="preference-icon">⌖</div>

          <div className="preference-description">
            <h3>Preferências de localização</h3>
            <p>Defina como as notícias regionais são priorizadas.</p>
          </div>

          <span className="settings-tag">Em breve</span>
        </div>
      </div>

      <footer className="settings-footer">
        <span className="settings-footer-mark">IV</span>
        <p>IV News · Configurações da plataforma</p>
      </footer>
    </main>
  );
}

export default Settings;
