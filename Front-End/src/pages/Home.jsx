import { useEffect, useState } from "react";
import { getNoticias } from "../services/noticiasService";

import NewsCard from "../components/NewsCard";
import FeaturedNews from "../components/FeaturedNews";

function Home() {

    const [noticias, setNoticias] = useState([]);

    useEffect(() => {
        async function carregarNoticias() {
            const dados = await getNoticias();
            setNoticias(dados);
        }

        carregarNoticias();
    }, []);

    console.log(noticias);

    let newsSection;

    if (noticias.length === 0) {
        newsSection = <h1>Nenhuma notícia encontrada</h1>;
    } else {
        newsSection = noticias.map((noticia) => (
        <NewsCard
            key={noticia.id}
            id={noticia.id}
            title={noticia.titulo}
            description={noticia.conteudo}
            category={noticia.categoria?.nome}
            location={noticia.localizacao?.cidade}
            image={noticia.imagemUrl}
        />
));
    }

    return (
        <main>
            {/* <FeaturedNews news={noticias} /> */}

            <h1 className="last-news-title">
                Últimas notícias
            </h1>

            <div className="news-grid">
                {newsSection}
            </div>
        </main>
    );
}

export default Home;