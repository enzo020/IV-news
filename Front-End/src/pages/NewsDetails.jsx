import { useParams } from "react-router-dom";
import { getNoticiaById } from "../services/noticiasService";
import { useEffect, useState } from "react";
import Loading from "../components/Loading/Loading";
import ErrorMessage from "../components/ErrorMessage";
import ImageWithFallback from "../components/ImageWithFallBack";
import NewsSummary from "../components/NewsSummary";
import { getResumoByNoticiaId } from "../services/resumosService";

function NewsDetails() {

    const { id } = useParams();

    const [noticia, setNoticia] = useState(null);
    const [loading, setLoading] = useState(true);
    const [erro, setErro] = useState(false);
    const [resumo, setResumo] = useState(null);

    useEffect(() => {
        async function carregarNoticia() {

            try {
                setLoading(true);
                setErro(false);
                
                const dados = await getNoticiaById(Number(id));
                
                setNoticia(dados);
            } catch (error) {
                setErro(true);
            } finally {
                setLoading(false);
            }
        }

        carregarNoticia();
    }, [id]);

    useEffect(() => {
      let ativo = true;

      async function carregarResumo() {
        setResumo(null);

        try {
          const dadosResumo = await getResumoByNoticiaId(Number(id));

          if (ativo) {
            setResumo(dadosResumo);
          }
        } catch (error) {
          console.error("Erro ao carregar resumo:", error);
        }
      }

      if (id) {
        carregarResumo();
      }

      return () => {
        ativo = false;
      };
    }, [id]);

      if (loading) return <Loading />;

      if (erro)
        return (
          <ErrorMessage
            title="Erro ao carregar notícia"
            message="Tente novamente mais tarde"
          />
        );

  return (
    <main className="container py-5">
      <article className="news-details mx-auto">
        <div className="news-details-image">
          <ImageWithFallback src={noticia.imagemUrl} alt={noticia.titulo} />
        </div>
        <div className="news-details-content">
          <span className="badge news-details-category mb-3">
            {noticia.categoria
              ? noticia.categoria.nome
              : "Categoria não informada"}
          </span>

          <h1 className="news-details-title">{noticia.titulo}</h1>

          <p className="news-details-location">
            {noticia.localizacao
              ? noticia.localizacao.cidade
              : "Localização não informada"}
          </p>
          <NewsSummary resumo={resumo} />

          <details className="news-details-full-content">
            <summary>
              <i className="bi bi-file-text" aria-hidden="true"></i>
              Ler conteúdo completo
              <i
                className="bi bi-chevron-down news-details-chevron"
                aria-hidden="true"
              ></i>
            </summary>

            <div className="news-details-description">{noticia.conteudo}</div>
          </details>

          <hr />

          <div className="news-details-source mt-5">
            <span className="text-secondary">
              Fonte:{" "}
              <a href={noticia.urlNoticia} target="_blank">
                {noticia.fonte}
              </a>
            </span>
          </div>
        </div>
      </article>
    </main>
  );
}

export default NewsDetails;
