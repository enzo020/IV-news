import Icon from "./Icon";
import { useState } from "react";

function Header({ onMenuClick }) {

    const [busca, setBusca] = useState("");

    const realizarBusca = () => {
        const texto = busca.trim()
        if (!texto) {
            window.location.href = "/noticias";
            return;
        }

        window.location.href = `/noticias?busca=${encodeURIComponent(texto)}`;
    }

      const handleKeyDown = (event) => {
        if (event.key === "Enter") {
          realizarBusca();
        }
      };

    return (
      <header className="site-header">
        <div className="container-fluid header-content">
          <div className="d-flex align-items-center justify-content-between w-100">
            <button className="menu-button" onClick={onMenuClick}>
              ☰
            </button>
            <Icon />
          </div>

          <div className="header-center">
            <a href="/" className="site-logo">
              <span className="site-logo-mark">IV</span>

              <span className="site-logo-name">NEWS</span>
            </a>

            <p className="site-tagline">
              Notícias atualizadas, onde você estiver
            </p>

            <div className="search-box">
              <input
                type="text"
                placeholder="Pesquise notícias"
                value={busca}
                onChange={(event) => setBusca(event.target.value)}
                onKeyDown={handleKeyDown}
              />
              <button
                type="button"
                onClick={realizarBusca}
                aria-label="Pesquisar"
              >
                <i className="bi bi-search"></i>
              </button>
            </div>
          </div>

          <button className="account-button">Conta</button>
        </div>
      </header>
    );
}

export default Header;