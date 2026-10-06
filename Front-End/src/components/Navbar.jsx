import { Link } from "react-router-dom";

function Navbar() {
    return (
        <nav className="site-navbar">
            <div className="container">
                <div className="navbar-links">

                    <Link to="/">Home</Link>

                    <Link to="/noticias">
                        Notícias
                    </Link>

                    <Link to="/categorias">
                        Categorias
                    </Link>

                    <Link to="/sobre">
                        Sobre
                    </Link>

                    <Link to="/ajuda">
                        Ajuda
                    </Link>

                </div>
            </div>
        </nav>
    );
}

export default Navbar;