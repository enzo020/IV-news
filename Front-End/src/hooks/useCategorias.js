import { useEffect, useState } from "react";
import { getCategorias } from "../services/categoriasService";

export function useCategorias() {
  const [categorias, setCategorias] = useState([]);
  const [loading, setLoading] = useState(true);
  const [erro, setErro] = useState(false);

  useEffect(() => {
    async function carregar() {
      try {
        const dados = await getCategorias();
        setCategorias(dados);
      } catch (error) {
        console.error("Erro ao carregar categorias:", error);
        setErro(true);
      } finally {
        setLoading(false);
      }
    }

    carregar();
  }, []);

  return { categorias, loading, erro };
}
