import { Link } from "react-router-dom";
import { formatDate } from "../utils/formatDate";
import ImageWithFallback from "./ImageWithFallBack";

function NewsCard({
  id,
  title,
  description,
  image,
  category,
  locations = [],
  datetime,
}) {
  const primeiraLocalizacao = locations[0];
  const outrasLocalizacoes = locations.slice(1);

  return (
    <article className="news-card">
      <div className="news-card-image">
        <ImageWithFallback src={image} alt={title} />
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

        <Link to={`/noticia/${id}`} className="news-card-button">
          Ler notícia
        </Link>
      </div>
    </article>
  );
}

export default NewsCard;