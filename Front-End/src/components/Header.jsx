import Icon from "./Icon";

function Header() {
    return (
        <header className="site-header">

            <div className="container header-content">

                <Icon />

                <div className="header-center">
                    <h1>IV NEWS</h1>

                    <p>Notícias digitais atualizadas em mãos</p>

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