import { Link } from "react-router-dom";


function Sidebar({ isOpen, onClose }) {
    return (
        <>
            <aside className={`sidebar ${isOpen ? "sidebar-open" : ""}`}>

                <div className="sidebar-header">
                    <div className="d-flex align-items-center gap-3 p-4">
                        <div className="account-avatar">
                            M
                        </div>

                        <div>
                            <strong className="d-block">
                                Murilo
                            </strong>

                            <small className="text-secondary">
                                murilo@email.com
                            </small>
                        </div>
                    </div>

                    <button
                        className="sidebar-close"
                        onClick={onClose}
                    >
                        ×
                    </button>
                </div>

                <nav className="sidebar-nav">

                    <Link to={"/"} onClick={onClose}>
                        🏠 Início
                    </Link>

                    <Link to={"/"} onClick={onClose}>
                        📰 Últimas notícias
                    </Link>

                    <div className="sidebar-section" onClick={onClose}>
                        <span>CATEGORIAS</span>
                    </div>

                    <Link
                        to={"/"}
                        onClick={onClose}
                        className="sidebar-link d-flex align-items-center gap-3"
                    >
                        <span>💻</span>
                        <span>Tecnologia</span>
                    </Link>

                    <Link to={"/"} className="sidebar-link d-flex align-items-center gap-3" onClick={onClose}>
                        <span>⚽</span>
                        <span>Esportes</span>
                    </Link>

                    <Link to={"/"} className="sidebar-link d-flex align-items-center gap-3" onClick={onClose}>
                        <span>🏛️</span>
                        <span>Política</span>
                    </Link>

                    <Link to={"/"} className="sidebar-link d-flex align-items-center gap-3" onClick={onClose}>
                        <span>🔬</span>
                        <span>Ciência</span>
                    </Link>

                    <Link to={"/"} className="sidebar-link d-flex align-items-center gap-3" onClick={onClose}>
                        <span>🎬</span>
                        <span>Entretenimento</span>
                    </Link>

                </nav>

                <div className="sidebar-bottom">
                    <Link to={"/"} onClick={onClose}>⚙️ Configurações</Link>
                    <Link to={"/"} onClick={onClose}>❓ Ajuda</Link>
                </div>

            </aside>

            {isOpen && (
                <div
                    className="sidebar-overlay"
                    onClick={onClose}
                />
            )}
        </>
    );
}

export default Sidebar;