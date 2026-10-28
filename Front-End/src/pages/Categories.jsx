import { Link } from "react-router-dom";
import { useCategorias } from "../hooks/useCategorias";
import Loading from "../components/Loading/Loading";
import ErrorMessage from "../components/ErrorMessage";

function Categories() {
  const { categorias, loading, erro } = useCategorias();

  if (loading) {
    return <Loading />;
  }

  if (erro) {
    return (
      <ErrorMessage
        title="Não foi possível carregar as categorias"
        message="Tente novamente mais tarde."
      />
    );
  }

  return (
    <main className="categories-page">
      <section className="categories-header">
        <span className="home-section-label">IV NEWS</span>

        <h1>Categorias</h1>

        <p>Explore as notícias do IV News organizadas por assunto.</p>
      </section>

      <section className="categories-grid">
        {categorias.map((categoria) => (
          <article className="category-card" key={categoria.nome}>
            <h2>{categoria.nome}</h2>

            <p>{categoria.descricao}</p>

            <Link to={`/noticias?categoria=${categoria.nome}`}>
              Ver notícias
              <span>→</span>
            </Link>
          </article>
        ))}
      </section>
    </main>
  );
}

export default Categories;
