import apiFetch from "./api";

export async function getCategorias() {

    return apiFetch("/categorias");
}