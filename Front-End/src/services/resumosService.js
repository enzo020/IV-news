import apiFetch from "./api";

export async function getResumoByNoticiaId(noticiaId) {
  return apiFetch(`/noticias/${noticiaId}/resumo`);
}
