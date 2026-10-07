import { useEffect, useMemo, useState } from "react";
import { useSearchParams } from "react-router-dom";

import { getNoticias } from "../services/noticiasService";

import NewsCard from "../components/NewsCard";
import Loading from "../components/Loading/Loading";
import ErrorMessage from "../components/ErrorMessage";

function News() {
  const [noticias, setNoticias] = useState([]);

  const [loading, setLoading] = useState(true);
  const [erro, setErro] = useState(false);

  const [searchParams] = useSearchParams();

  const categoriaSelecionada = searchParams.get("categoria");
  const busca = searchParams.get("busca");

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

  const noticiasFiltradas = useMemo(() => {
    let resultado = [...noticias];

    // Filtro por categoria

    if (categoriaSelecionada) {
      resultado = resultado.filter(
        (noticia) =>
          noticia.categoria?.nome?.toLowerCase() ===
          categoriaSelecionada.toLowerCase(),
      );
    }

    // Filtro por busca

    if (busca) {
      const termo = busca.toLowerCase();

      resultado = resultado.filter(
        (noticia) =>
          noticia.titulo?.toLowerCase().includes(termo) ||
          noticia.conteudo?.toLowerCase().includes(termo),
      );
    }

    return resultado;
  }, [noticias, categoriaSelecionada, busca]);

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

  return (
    <main className="news-page">
      <section className="news-page-header">
        <span className="home-section-label">IV NEWS</span>

        <h1>Notícias</h1>

        <p>
          Explore as notícias mais recentes e encontre conteúdos por categoria.
        </p>
      </section>

      {/* Filtros */}

      <section className="news-filters">
        <div className="news-filter-group">
          <button
            className={!categoriaSelecionada ? "active" : ""}
            onClick={() => {
              window.history.pushState({}, "", "/noticias");

              window.location.reload();
            }}
          >
            Todas
          </button>
          <a
            href="/noticias?categoria=Tecnologia"
            className={categoriaSelecionada === "Tecnologia" ? "active" : ""}
          >
            Tecnologia
          </a>

          <a
            href="/noticias?categoria=Economia"
            className={categoriaSelecionada === "Economia" ? "active" : ""}
          >
            Economia
          </a>

          <a
            href="/noticias?categoria=Esportes"
            className={categoriaSelecionada === "Esportes" ? "active" : ""}
          >
            Esportes
          </a>

          <a
            href="/noticias?categoria=Política"
            className={categoriaSelecionada === "Política" ? "active" : ""}
          >
            Política
          </a>

          <a
            href="/noticias?categoria=Ciência"
            className={categoriaSelecionada === "Ciência" ? "active" : ""}
          >
            Ciência
          </a>

          <a
            href="/noticias?categoria=Outros"
            className={categoriaSelecionada === "Outros" ? "active" : ""}
          >
            Outros
          </a>
        </div>
      </section>

      {/* Resultado */}

      <section className="news-results">
        <div className="news-results-header">
          <h2>
            {categoriaSelecionada
              ? categoriaSelecionada
              : busca
                ? `Resultados para "${busca}"`
                : "Todas as notícias"}
          </h2>

          <span>
            {noticiasFiltradas.length} notícia
            {noticiasFiltradas.length !== 1 ? "s" : ""}
          </span>
        </div>

        {noticiasFiltradas.length === 0 ? (
          <div className="news-empty">
            <h2>Nenhuma notícia encontrada</h2>

            <p>Tente utilizar outra categoria ou termo de busca.</p>
          </div>
        ) : (
          <div className="news-grid">
            {noticiasFiltradas.map((noticia) => (
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
    </main>
  );
}

export default News;
