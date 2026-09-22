import LoadingLogo from "./LoadingLogo";
import "./Loading.css";

function Loading() {
    return (
        <div className="page-state">

            <div className="page-state-card">

                <div className="loading-logo">
                    <LoadingLogo />
                </div>

                <p className="loading-title">
                    Carregando...
                </p>

                <p>
                    Estamos buscando as notícias. Aguarde um momento.
                </p>

            </div>

        </div>
    );
}

export default Loading;