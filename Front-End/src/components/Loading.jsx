function Loading() {
    return(
        <div className="page-state">
            <div className="page-state-card">
                <div className="spinner-border page-state-spinner" role="status">
                    <span className="visually-hidden">
                        Carregando...
                    </span>
                </div>

                <p>Carregando...</p> 

                <p>Estamos buscando as notícias. Aguarde um momento.</p>
            </div>
        </div>
    );
}

export default Loading;