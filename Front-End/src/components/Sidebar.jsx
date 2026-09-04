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

                    <a href="#">
                        🏠 Início
                    </a>

                    <a href="#">
                        📰 Últimas notícias
                    </a>

                    <div className="sidebar-section">
                        <span>CATEGORIAS</span>
                    </div>

                    <a
                        href="#"
                        className="sidebar-link d-flex align-items-center gap-3"
                    >
                        <span>💻</span>
                        <span>Tecnologia</span>
                    </a>

                    <a href="#" className="sidebar-link d-flex align-items-center gap-3">
                        <span>⚽</span>
                        <span>Esportes</span>
                    </a>

                    <a href="#" className="sidebar-link d-flex align-items-center gap-3">
                        <span>🏛️</span>
                        <span>Política</span>
                    </a>

                    <a href="#" className="sidebar-link d-flex align-items-center gap-3">
                        <span>🔬</span>
                        <span>Ciência</span>
                    </a>

                    <a href="#" className="sidebar-link d-flex align-items-center gap-3">
                        <span>🎬</span>
                        <span>Entretenimento</span>
                    </a>

                </nav>

                <div className="sidebar-bottom">
                    <a href="#">⚙️ Configurações</a>
                    <a href="#">❓ Ajuda</a>
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