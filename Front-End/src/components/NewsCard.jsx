import { Link } from "react-router-dom";
import { formatDate } from "../utils/formatDate";

function NewsCard({
    id,
    title,
    description,
    image,
    category,
    location,
    datetime
}) {


    return (
      <article className="news-card">
        <div className="news-card-image">
          {image ? (
            <img src={image} alt={title} />
          ) : (
            <div className="news-card-image-placeholder">
              <span>IV NEWS</span>
            </div>
          )}
        </div>

        <div className="news-card-content">
            {category && <span className="news-card-category">{category}</span>}

            <h2 className="news-card-title">{title}</h2>

            <div className="news-card-meta">
                {location && (
                <span className="news-card-location">📍 {location}</span>
                )}

                {datetime && <span className="news-card-date">{formatDate(datetime)}</span>}
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