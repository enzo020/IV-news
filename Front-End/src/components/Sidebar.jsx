import { Link } from "react-router-dom";
import { useCategorias } from "../hooks/useCategorias";
import CategoryLink from "./CategoryLink";

function Sidebar({ isOpen, onClose }) {
  const { categorias, loading, erro } = useCategorias();

  return (
    <>
      <aside className={`sidebar ${isOpen ? "sidebar-open" : ""}`}>
        <div className="sidebar-header">
          <div className="d-flex align-items-center gap-3 p-4">
            <div className="account-avatar">M</div>

            <div>
              <strong className="d-block">Murilo</strong>
              <small className="text-secondary">murilo@email.com</small>
            </div>
          </div>

          <button
            className="sidebar-close"
            onClick={onClose}
            aria-label="Fechar menu"
          >
            ×
          </button>
        </div>

        <nav className="sidebar-nav">
          <Link to="/" onClick={onClose}>
            🏠 Início
          </Link>

          <Link to="/noticias" onClick={onClose}>
            📰 Últimas notícias
          </Link>

          <div className="sidebar-section">
            <span>CATEGORIAS</span>
          </div>

          {loading && <span>Carregando categorias...</span>}

          {erro && <span>Não foi possível carregar as categorias.</span>}

          {!loading &&
            !erro &&
            categorias.map((categoria) => (
              <CategoryLink
                key={categoria.id}
                categoria={categoria}
                onClick={onClose}
                className="sidebar-link d-flex align-items-center gap-3"
              />
            ))}
        </nav>

        <div className="sidebar-bottom">
          <Link to="/" onClick={onClose}>
            ⚙️ Configurações
          </Link>
          <Link to="/" onClick={onClose}>
            ❓ Ajuda
          </Link>
        </div>
      </aside>

      {isOpen && <div className="sidebar-overlay" onClick={onClose} />}
    </>
  );
}

export default Sidebar;
