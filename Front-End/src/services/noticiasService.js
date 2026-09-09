import apiFetch from "./api";

export async function getNoticias() {
    return apiFetch("/api/noticias");
}

export async function getNoticiaById(id) {
    return apiFetch(`/api/noticias/${id}`);
}
