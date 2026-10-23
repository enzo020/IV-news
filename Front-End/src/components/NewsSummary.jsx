

function NewsSummary({ resumo }) {
  if (!resumo) {
    return (
      <section className="news-summary news-summary-loading">
        <div className="news-summary-heading">
          <span className="news-summary-icon">
            <i className="bi bi-stars" aria-hidden="true"></i>
          </span>
          <div>
            <h2>Resumo inteligente</h2>
            <span>Gerado por inteligência artificial</span>
          </div>
        </div>

        <p>Resumo indisponível no momento.</p>
      </section>
    );
  }

  const pontosChave = resumo.pontosChave ?? resumo.pontos_chave ?? [];

  const resumoExecutivo = resumo.resumoExecutivo ?? resumo.resumo_executivo;

  const conteudoTratado = resumo.conteudoTratado ?? resumo.conteudo_tratado;

  const sentimento = resumo.sentimento;

  return (
    <section className="news-summary">
      <div className="news-summary-heading">
        <span className="news-summary-icon">
          <i className="bi bi-stars" aria-hidden="true"></i>
        </span>

        <div>
          <h2>Resumo inteligente</h2>
          <span>Gerado por inteligência artificial</span>
        </div>
      </div>

      {resumoExecutivo && (
        <div className="news-summary-executive">
          <h3>Em resumo</h3>
          <p>{resumoExecutivo}</p>
        </div>
      )}

      {pontosChave.length > 0 && (
        <div className="news-summary-points">
          <h3>Pontos-chave</h3>
          <ul>
            {pontosChave.map((ponto, index) => (
              <li key={index}>{ponto}</li>
            ))}
          </ul>
        </div>
      )}

      {sentimento && (
        <div className="news-summary-sentiment">
          <span>Tom do texto:</span> <strong>{sentimento}</strong>
        </div>
      )}

      <p className="news-summary-disclaimer">
        Este resumo foi gerado por IA e pode conter imprecisões. Consulte a
        fonte original para confirmar os detalhes.
      </p>

    </section>
  );
}

export default NewsSummary;
