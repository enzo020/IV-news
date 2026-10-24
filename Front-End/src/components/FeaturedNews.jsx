import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import ImageWithFallback from "./ImageWithFallBack";

function FeaturedNews({ news = [], interval = 5000 }) {
  const [currentIndex, setCurrentIndex] = useState(0);
  const [direction, setDirection] = useState("next");

  // Garante que o índice continue válido quando a lista mudar.
  useEffect(() => {
    setCurrentIndex(0);
  }, [news.length]);

  // Avança automaticamente pelo carrossel.
  useEffect(() => {
    if (news.length <= 1) return;

    const timer = setInterval(() => {
      setDirection("next");

      setCurrentIndex((current) => (current + 1) % news.length);
    }, interval);

    return () => clearInterval(timer);
  }, [news.length, interval]);

  function previousNews() {
    if (news.length <= 1) return;

    setDirection("previous");

    setCurrentIndex((current) =>
      current === 0 ? news.length - 1 : current - 1,
    );
  }

  function nextNews() {
    if (news.length <= 1) return;

    setDirection("next");

    setCurrentIndex((current) => (current + 1) % news.length);
  }

  if (news.length === 0) {
    return null;
  }

  const currentNews = news[currentIndex];

  return (
    <article className="featured-news">
      <div key={currentNews.id} className={`featured-news-slide ${direction}`}>
        <div className="featured-news-image">
          <ImageWithFallback src={currentNews.image} alt={currentNews.title} />
          <div className="featured-news-gradient" />
        </div>

        <div className="featured-news-content">
          <h3 className="featured-news-title">{currentNews.title}</h3>

          <p className="featured-news-description">{currentNews.description}</p>

          <Link
            to={`/noticia/${currentNews.id}`}
            className="featured-news-link"
          >
            Ler notícia completa
            <span aria-hidden="true"> →</span>
          </Link>
        </div>
      </div>

      {news.length > 1 && (
        <>
          <button
            type="button"
            className="featured-news-arrow featured-news-arrow-left"
            onClick={previousNews}
            aria-label="Notícia anterior"
          >
            ‹
          </button>

          <button
            type="button"
            className="featured-news-arrow featured-news-arrow-right"
            onClick={nextNews}
            aria-label="Próxima notícia"
          >
            ›
          </button>

          <div className="featured-news-indicators">
            {news.map((noticia, index) => (
              <button
                key={noticia.id}
                type="button"
                className={
                  index === currentIndex ? "indicator active" : "indicator"
                }
                onClick={() => {
                  setDirection(index > currentIndex ? "next" : "previous");
                  setCurrentIndex(index);
                }}
                aria-label={`Ir para notícia ${index + 1}`}
                aria-current={index === currentIndex ? "true" : undefined}
              />
            ))}
          </div>
        </>
      )}
    </article>
  );
}

export default FeaturedNews;
