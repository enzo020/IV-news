import apiFetch from "./api";

export async function getNoticias() {
    return apiFetch("/noticias");
}

export async function getNoticiaById(id) {
    return apiFetch(`/noticias/${id}`);
}
