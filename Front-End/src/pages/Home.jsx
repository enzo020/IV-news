import NewsCard from "../components/NewsCard";
import Footer from "../components/Footer";

function Home() {
    return (
        <main>
            <h1>Últimas notícias</h1>

            <section className="news-grid">
                <NewsCard
                    title="Primeira notícia"
                    description="Descrição da primeira notícia."
                />

                <NewsCard
                    title="Segunda notícia"
                    description="Descrição da segunda notícia."
                />
            </section>
        </main>
    );
}

export default Home;