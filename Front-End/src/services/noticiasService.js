import apiFetch from "./api";

export async function getNoticias(filtros = {}) {
  const params = new URLSearchParams();

  if (filtros.busca) params.append("busca", filtros.busca);

  if (filtros.localizacao) params.append("localizacao", filtros.localizacao);

  const query = params.toString();

  return apiFetch(`/noticias${query ? `?${query}` : ""}`);
}

export async function getNoticiaById(id) {
    return apiFetch(`/noticias/${id}`);
}

export async function gerarImagemDaNoticia(id) {
    return apiFetch(`/gerar-imagens?noticiaId=${id}`, {
      method: "POST"
    });
}
        