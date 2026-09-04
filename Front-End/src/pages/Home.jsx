import NewsCard from "../components/NewsCard";
import Footer from "../components/Footer";

function Home() {
    return (
        <main>
            <h1>Últimas notícias</h1>

            <section className="news-grid">
                <NewsCard
                    title="Manoel Gomes é eleito como deputado federal"
                    description="Agora vai ser tudo bom de maise"
                    image="/imgNoticia.jpg" 
                />

                <NewsCard
                    title="Segunda notícia"
                    description="Descrição da segunda notícia."
                    image=""
                />

                <NewsCard
                    title="Segunda notícia"
                    description="Descrição da segunda notícia."
                    image=""
                />

                <NewsCard
                    title="Segunda notícia"
                    description="Descrição da segunda notícia."
                    image=""
                />
            </section>
        </main>
    );
}

export default Home;