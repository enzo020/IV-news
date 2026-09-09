import Icon from "./Icon";

function Header({ onMenuClick }) {
    return (
        <header className="site-header">

            <div className="container-fluid header-content">
                <div className="d-flex align-items-center justify-content-between w-100">
                <button
                    className="menu-button"
                    onClick={onMenuClick}
                >
                    ☰
                </button>
                    <Icon />
                </div>


          
                <div className="header-center">

                    <a href="/" className="site-logo">
                        <span className="site-logo-mark">IV</span>

                        <span className="site-logo-name">
                            NEWS
                        </span>
                    </a>

                    <p className="site-tagline">
                        Notícias atualizadas, onde você estiver
                    </p>

                    <div className="search-box">
                        <input
                            type="text"
                            placeholder="Pesquise notícias"
                        />
                    </div>

                </div>
   


                <button className="account-button">
                    Conta
                </button>

            </div>
        </header>
    );
}

export default Header;