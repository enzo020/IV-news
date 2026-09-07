import { useParams } from "react-router-dom";
import noticias from "../data/NewsMock";

function NewsDetails() {

    const { id } = useParams();

    const noticia = noticias.find(
        (noticia) => noticia.id === Number(id)
    );

    if (!noticia) {
        return (
            <main>
                <h1>Notícia não encontrada</h1>
            </main>
        );
    }

    return (
        <main>

            <img
                src={noticia.image}
                alt={noticia.title}
            />

            <span>
                {noticia.category}
            </span>

            <h1>
                {noticia.title}
            </h1>

            <p>
                {noticia.location}
            </p>

            <p>
                {noticia.description}
            </p>

        </main>
    );
}

export default NewsDetails;
