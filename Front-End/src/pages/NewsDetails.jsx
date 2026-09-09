import { useParams } from "react-router-dom";
import noticias from "../data/NewsMock";

function NewsDetails() {

    const { id } = useParams();

    const noticia = noticias.find(
        (noticia) => noticia.id === Number(id)
    );

    if (!noticia) {
        return (
            <main className="container py-5">
                <div className="text-center">
                    <h1 className="mb-3">Notícia não encontrada</h1>
                    <p className="text-secondary">
                        A notícia que você está procurando não existe.
                    </p>
                </div>
            </main>
        );
    }

    return (
        <main className="container py-5">

            <article className="news-details mx-auto">

                <img
                    src={noticia.image}
                    alt={noticia.title}
                    className="news-details-image"
                />

                <div className="news-details-content">

                    <span className="badge news-details-category mb-3">
                        {noticia.category}
                    </span>

                    <h1 className="news-details-title">
                        {noticia.title}
                    </h1>

                    <p className="news-details-location">
                        📍 {noticia.location}
                    </p>

                    <p className="news-details-description">
                        {noticia.description}
                    </p>

                    <hr />

                    <div className="news-details-body">
                        <p>{noticia.content}</p>
                    </div>

                    <div className="news-details-source mt-5">
                        <span className="text-secondary">
                            Fonte: {noticia.sourceName}
                        </span>

                        <a
                            href={noticia.sourceUrl}
                            target="_blank"
                            rel="noopener noreferrer"
                            className="btn news-details-source-button"
                        >
                            Ler matéria original
                        </a>
                    </div>

                </div>

            </article>

        </main>
    );
}

export default NewsDetails;
