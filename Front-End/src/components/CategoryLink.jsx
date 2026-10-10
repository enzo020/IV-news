import { Link } from "react-router-dom";

function CategoryLink({ categoria, onClick, className = "" }) {
  return (
    <Link
      to={`/noticias?categoria=${encodeURIComponent(categoria.nome)}`}
      onClick={onClick}
      className={`category-link ${className}`}
    >

      <span className="category-link-name">{categoria.nome}</span>

      {categoria.descricao && (
        <span className="category-tooltip" role="tooltip">
          {categoria.descricao}
        </span>
      )}
    </Link>
  );
}

export default CategoryLink;
