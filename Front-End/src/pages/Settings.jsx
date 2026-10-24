import React, { useState } from "react";

function Settings() {
  const [temaSelecionado, setTemaSelecionado] = useState("escuro");

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
            <button
              type="button"
              className={`theme-option ${
                temaSelecionado === "escuro" ? "selected" : ""
              }`}
              onClick={() => setTemaSelecionado("escuro")}
              aria-pressed={temaSelecionado === "escuro"}
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
                    {temaSelecionado === "escuro" && <span />}
                  </span>
                  <div>
                    <strong>Modo escuro</strong>
                    <small>Mais confortável em ambientes escuros</small>
                  </div>
                </div>
                {temaSelecionado === "escuro" && (
                  <span className="theme-check">✓</span>
                )}
              </div>
            </button>

            <button
              type="button"
              className={`theme-option ${
                temaSelecionado === "claro" ? "selected" : ""
              }`}
              onClick={() => setTemaSelecionado("claro")}
              aria-pressed={temaSelecionado === "claro"}
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
                    {temaSelecionado === "claro" && <span />}
                  </span>
                  <div>
                    <strong>Modo claro</strong>
                    <small>Uma interface clara e luminosa</small>
                  </div>
                </div>
                {temaSelecionado === "claro" && (
                  <span className="theme-check">✓</span>
                )}
              </div>
            </button>
          </div>

          <div className="settings-note">
            <span aria-hidden="true">ⓘ</span>
            <p>
              A seleção é apenas uma prévia da interface. A aplicação global do
              tema será implementada posteriormente.
            </p>
          </div>
        </div>
      </section>

      <section className="settings-section">
        <div className="settings-section-heading">
          <div className="settings-section-icon">
            <span aria-hidden="true">✦</span>
          </div>
          <div>
            <h2>Experiência de leitura</h2>
            <p>Opções para personalizar a navegação.</p>
          </div>
        </div>

        <div className="settings-card settings-preferences">
          <div className="settings-preference">
            <div className="preference-icon">Aa</div>
            <div className="preference-description">
              <h3>Tamanho do texto</h3>
              <p>Ajuste o tamanho da fonte das notícias.</p>
            </div>
            <span className="settings-tag">Em breve</span>
          </div>

          <div className="settings-preference">
            <div className="preference-icon">▤</div>
            <div className="preference-description">
              <h3>Exibição das notícias</h3>
              <p>Personalize como os artigos aparecem no feed.</p>
            </div>
            <span className="settings-tag">Em breve</span>
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
      </section>

      <footer className="settings-footer">
        <span className="settings-footer-mark">IV</span>
        <p>IV News · Configurações da plataforma</p>
      </footer>
    </main>
  );
}

export default Settings;
