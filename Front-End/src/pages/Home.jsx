import NewsCard from "../components/NewsCard";
import FeaturedNews from "../components/FeaturedNews";

// Tmeporário:
import noticias from "../data/NewsMock";

function Home() {

    return (
        <main>
            <FeaturedNews news={noticias} />
            <h1 className="last-news-title">Últimas notícias</h1>

            <div className="news-grid">
                {noticias.map((noticia) => (
                    <NewsCard
                        key={noticia.id}
                        id={noticia.id}
                        title={noticia.title}
                        description={noticia.description}
                        image={noticia.image}
                        category={noticia.category}
                        location={noticia.location}
                    />
                ))}
            </div>
        </main>
    );
}

export default Home;