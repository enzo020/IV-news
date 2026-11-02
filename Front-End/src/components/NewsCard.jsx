import { Link } from "react-router-dom";
import { formatDate } from "../utils/formatDate";
import ImageWithFallback from "./ImageWithFallBack";
import { gerarImagemDaNoticia } from "../services/noticiasService";
import { useState } from "react";


function NewsCard({
  id,
  title,
  description,
  image,
  category,
  locations = [],
  datetime,
  possuiPrompt
}) {
  const [gerandoImagem, setGerandoImagem] = useState(false);
  const [imagemAtual, setImagemAtual] = useState(image);

  const primeiraLocalizacao = locations[0];
  const outrasLocalizacoes = locations.slice(1);

  const possuiImagem = Boolean(imagemAtual?.trim());

  async function handleGerarImagem() {
    // Proteção adicional contra chamadas repetidas.
    if (gerandoImagem || possuiImagem) return;

    try {
      setGerandoImagem(true);

      const resultado = await gerarImagemDaNoticia(id);

      // Considerando que o backend retorna a imagem atualizada.
      setImagemAtual(resultado.imagemUrl);
    } catch (error) {
      console.error("Erro ao gerar imagem da notícia:", error);
    } finally {
      setGerandoImagem(false);
    }
  }

  return (
    <article className="news-card">
      <div className="news-card-image">
        <ImageWithFallback src={imagemAtual} alt={title} />
      </div>

      <div className="news-card-content">
        {category && <span className="news-card-category">{category}</span>}

        <h2 className="news-card-title">{title}</h2>

        <div className="news-card-meta">
          {primeiraLocalizacao && (
            <div className="news-card-location">
              <span>📍 {primeiraLocalizacao.cidade}</span>

              {outrasLocalizacoes.length > 0 && (
                <div className="location-tooltip-container">
                  <span className="location-more">
                    +{outrasLocalizacoes.length}
                  </span>

                  <div className="location-tooltip">
                    {outrasLocalizacoes.map((localizacao) => (
                      <span key={localizacao.id}>{localizacao.cidade}</span>
                    ))}
                  </div>
                </div>
              )}
            </div>
          )}

          {datetime && (
            <span className="news-card-date">{formatDate(datetime)}</span>
          )}
        </div>

        <p className="news-card-description">{description}</p>

        <div className="news-card-actions">
          <Link to={`/noticia/${id}`} className="news-card-button">
            Ler notícia
          </Link>

          {!possuiImagem && possuiPrompt && (
            <button
              className="news-card-generate-button"
              onClick={handleGerarImagem}
              disabled={gerandoImagem}
            >
              {gerandoImagem ? (
                <>
                  <span className="button-spinner"></span>
                  Gerando...
                </>
              ) : (
                <>
                  <span className="ai-sparkle">✦</span>
                  Gerar com IA
                </>
              )}
            </button>
          )}
        </div>
      </div>
    </article>
  );
}

export default NewsCard;