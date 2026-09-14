import { useEffect, useState } from "react";
import { getNoticias } from "../services/noticiasService";

import NewsCard from "../components/NewsCard";
import FeaturedNews from "../components/FeaturedNews";
import Loading from "../components/Loading";
import ErrorMessage from "../components/ErrorMessage";

function Home() {

    const [noticias, setNoticias] = useState([]);

    const [loading, setLoading] = useState(true);
    const [erro, setErro] = useState(false);

    useEffect(() => {
        async function carregarNoticias() {

            try {
                setLoading(true);
                setErro(false);

                const dados = await getNoticias();
                setNoticias(dados);

            } catch (error) {
                setErro(true);
            } finally {
                setLoading(false);
            }

        }

        carregarNoticias();
    }, []);

    if (loading) {
        return <Loading />
    }

    if (erro) {
        return <ErrorMessage
            title="Não foi possível carregar as notícias"
            message="Tente novamente mais tarde"
        />
    }

    // Log para debug - Retirar futuramente
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