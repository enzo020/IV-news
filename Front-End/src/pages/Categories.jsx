// Temporário
const categorias = [
  {
    nome: "Tecnologia",
    descricao:
      "Inovação, ciência da computação, inteligência artificial e novidades do mundo digital.",
  },
  {
    nome: "Economia",
    descricao:
      "Mercado, negócios, finanças e principais acontecimentos econômicos.",
  },
  {
    nome: "Esportes",
    descricao:
      "Resultados, competições, atletas e principais notícias esportivas.",
  },
  {
    nome: "Política",
    descricao:
      "Decisões políticas, governo, eleições e acontecimentos nacionais.",
  },
  {
    nome: "Ciência",
    descricao: "Descobertas, pesquisas e novidades do mundo científico.",
  },
  {
    nome: "Outros",
    descricao: "Notícias que não se encaixam nas demais categorias.",
  },
];

import { Link } from "react-router-dom";

function Categories() {
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