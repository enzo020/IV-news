import { useParams } from "react-router-dom";
import { getNoticiaById } from "../services/noticiasService";
import { useEffect, useState } from "react";
import Loading from "../components/Loading/Loading";
import ErrorMessage from "../components/ErrorMessage";
import ImageWithFallback from "../components/ImageWithFallBack";

function NewsDetails() {

    const { id } = useParams();

    const [noticia, setNoticia] = useState(null);
    const [loading, setLoading] = useState(true);
    const [erro, setErro] = useState(false);

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

    if (loading)
        return <Loading />

    if (erro)
        return <ErrorMessage 
            title="Erro ao carregar notícia"
            message="Tente novamente mais tarde"
        />

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

            <p className="news-details-description">{noticia.conteudo}</p>

            <hr />

            <div className="news-details-body">
              <p>{noticia.conteudo}</p>
            </div>

            <div className="news-details-source mt-5">
              <span className="text-secondary">
                Fonte: <a href={noticia.urlNoticia} target="_blank">{noticia.fonte}</a>
              </span>
            </div>
          </div>
        </article>
      </main>
    );
}

export default NewsDetails;
