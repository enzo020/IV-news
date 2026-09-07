import { useEffect, useState } from "react";

function previousNews() {
    setDirection("previous");

    setCurrentIndex((current) =>
        current === 0 ? news.length - 1 : current - 1
    );
}

function nextNews() {
    setDirection("next");

    setCurrentIndex((current) =>
        (current + 1) % news.length
    );
}

function FeaturedNews({ news, interval = 8000 }) {
    const [currentIndex, setCurrentIndex] = useState(0);
    const [direction, setDirection] = useState("next");

    // Avança automaticamente
    useEffect(() => {

    const timer = setInterval(() => {
            setDirection("next");

            setCurrentIndex((current) =>
                (current + 1) % news.length
            );
        }, interval);

        return () => clearInterval(timer);
    }, [news.length, interval]);

    function previousNews() {
        setDirection("previous");

        setCurrentIndex((current) =>
            current === 0 ? news.length - 1 : current - 1
        );
    }

    function nextNews() {
        setDirection("next");

        setCurrentIndex((current) =>
            (current + 1) % news.length
        );
    }

    const currentNews = news[currentIndex];

    return (
    <article className="featured-news">
    <div
        key={currentIndex}
        className={`featured-news-slide ${direction}`}
    >
        <div className="featured-news-image">
            <img
                src={currentNews.image}
                alt={currentNews.title}
            />
        </div>

        <div className="featured-news-content">
            <h1 className="featured-news-title">
                {currentNews.title}
            </h1>

            <p className="featured-news-description">
                {currentNews.description}
            </p>
        </div>
    </div>

            <button
                className="featured-news-arrow featured-news-arrow-left"
                onClick={previousNews}
                aria-label="Notícia anterior"
            >
                ‹
            </button>

            <button
                className="featured-news-arrow featured-news-arrow-right"
                onClick={nextNews}
                aria-label="Próxima notícia"
            >
                ›
            </button>

            <div className="featured-news-content">
                <h1 className="featured-news-title">
                    {currentNews.title}
                </h1>

                <p className="featured-news-description">
                    {currentNews.description}
                </p>
            </div>

            <div className="featured-news-indicators">
                {news.map((_, index) => (
                    <button
                        key={index}
                        className={
                            index === currentIndex
                                ? "indicator active"
                                : "indicator"
                        }
                        onClick={() => setCurrentIndex(index)}
                        aria-label={`Ir para notícia ${index + 1}`}
                    />
                ))}
            </div>

        </article>
    );
}

export default FeaturedNews;
