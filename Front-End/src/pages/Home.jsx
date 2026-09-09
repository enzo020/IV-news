import NewsCard from "../components/NewsCard";
import FeaturedNews from "../components/FeaturedNews";

// Tmeporário:
import noticias from "../data/NewsMock";

function Home() {

    var newsSection
        if (noticias.length === 0 || !noticias) {
          newsSection = <><h1>Nenhuma notícia encontrada</h1></>
        } else newsSection = noticias.map((noticia) => (
                    <NewsCard
                        key={noticia.id}
                        id={noticia.id}
                        title={noticia.title}
                        description={noticia.description}
                        image={noticia.image}
                        category={noticia.category}
                        location={noticia.location}
                    />
                ))

    return (
        <main>
            <FeaturedNews news={noticias} />
            <h1 className="last-news-title">Últimas notícias</h1>

            <div className="news-grid">
                {newsSection}
            </div>
        </main>
    );
}

export default Home;