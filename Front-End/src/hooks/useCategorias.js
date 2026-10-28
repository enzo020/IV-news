import { useEffect, useState } from "react";
import { getCategorias } from "../services/categoriasService";

export function useCategorias() {
  const [categorias, setCategorias] = useState([]);
  const [loading, setLoading] = useState(true);
  const [erro, setErro] = useState(false);

  useEffect(() => {
    async function carregarCategorias() {
      try {
        setLoading(true);
        setErro(false);

        const dados = await getCategorias();
        setCategorias(dados);
      } catch (error) {
        console.error("Erro ao carregar categorias:", error);
        setErro(true);
      } finally {
        setLoading(false);
      }
    }

    carregarCategorias();
  }, []);

  return {
    categorias,
    loading,
    erro,
  };
}
