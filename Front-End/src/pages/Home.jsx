import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { getNoticias } from "../services/noticiasService";

import NewsCard from "../components/NewsCard";
import FeaturedNews from "../components/FeaturedNews";
import Loading from "../components/Loading/Loading";
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
    return <Loading />;
  }

  if (erro) {
    return (
      <ErrorMessage
        title="Não foi possível carregar as notícias"
        message="Tente novamente mais tarde"
      />
    );
  }

  const featuredNews = noticias.slice(0, 5).map((noticia) => ({
    id: noticia.id,
    title: noticia.titulo,
    description: noticia.conteudo,
    image: noticia.imagemUrl,
  }));

  const noticiasRecentes = noticias.slice(0, 6);

  return (
    <main className="home-page">
      {/* Introdução */}

      <section className="home-intro">
        <div className="home-intro-content">
          <span className="home-intro-label">BEM-VINDO AO IV NEWS</span>

          <h1>
            Notícias atualizadas,
            <span> onde você estiver.</span>
          </h1>

          <p>
            Encontre notícias de diferentes categorias e acompanhe os principais
            acontecimentos em um único lugar.
          </p>

          <Link to="/noticias" className="home-intro-button">
            Explorar notícias
          </Link>
        </div>
      </section>

      {/* Destaque */}

      {featuredNews.length > 0 && (
        <section className="home-featured">
          <div className="home-section-header">
            <div>
              <span className="home-section-label">EM DESTAQUE</span>

              <h2>Principais notícias</h2>
            </div>
          </div>

          <FeaturedNews news={featuredNews} />
        </section>
      )}

      {/* Notícias recentes */}

      <section className="home-latest">
        <div className="home-section-header">
          <div>
            <span className="home-section-label">ACOMPANHE</span>

            <h2>Últimas notícias</h2>
          </div>

          <Link to="/noticias" className="home-see-all">
            Ver todas →
          </Link>
        </div>

        {noticiasRecentes.length === 0 ? (
          <p className="home-empty">Nenhuma notícia encontrada.</p>
        ) : (
          <div className="news-grid">
            {noticiasRecentes.map((noticia) => (
              <NewsCard
                key={noticia.id}
                id={noticia.id}
                title={noticia.titulo}
                description={noticia.conteudo}
                category={noticia.categoria?.nome}
                location={noticia.localizacao?.cidade}
                image={noticia.imagemUrl}
                datetime={noticia.publicadoEm}
              />
            ))}
          </div>
        )}
      </section>

      {/* Categorias */}

      <section className="home-categories">
        <div className="home-section-header">
          <div>
            <span className="home-section-label">EXPLORE</span>

            <h2>Encontre notícias por categoria</h2>
          </div>

          <Link to="/categorias" className="home-see-all">
            Ver categorias →
          </Link>
        </div>

        <div className="home-category-grid">
          <Link
            to="/noticias?categoria=Tecnologia"
            className="home-category-card"
          >
            <span>💻</span>
            <strong>Tecnologia</strong>
            <small>Inovação e tecnologia</small>
          </Link>

          <Link
            to="/noticias?categoria=Esportes"
            className="home-category-card"
          >
            <span>⚽</span>
            <strong>Esportes</strong>
            <small>Os principais acontecimentos</small>
          </Link>

          <Link
            to="/noticias?categoria=Política"
            className="home-category-card"
          >
            <span>🏛️</span>
            <strong>Política</strong>
            <small>Brasil e mundo</small>
          </Link>

          <Link to="/noticias?categoria=Ciência" className="home-category-card">
            <span>🔬</span>
            <strong>Ciência</strong>
            <small>Descobertas e pesquisas</small>
          </Link>
        </div>
      </section>
    </main>
  );
}

export default Home;
