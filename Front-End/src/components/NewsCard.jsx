import { Link } from "react-router-dom";

function NewsCard({
    id,
    title,
    description,
    image,
    category,
    location
}) {

    return (
        <article className="news-card">

            <div className="news-card-image">
                <img
                    src={image}
                    alt={title}
                />
            </div>

            <div className="news-card-content">

                <span className="news-card-category">
                    {category}
                </span>

                <h2 className="news-card-title">
                    {title}
                </h2>

                <span className="news-card-location">
                    {location}
                </span>

                <p className="news-card-description">
                    {description}
                </p>

                <Link
                    to={`/noticia/${id}`}
                    className="news-card-button"
                >
                    Ler notícia
                </Link>

            </div>

        </article>
    );
}

export default NewsCard;