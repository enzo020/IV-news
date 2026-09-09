const API_BASE_URL = "http://localhost:5022";

async function apiFetch(endpoint, options = {}) {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        ...options,
        headers: {
            "Content-Type": "application/json",
            ...options.headers
        }
    });

    if (!response.ok) {
        throw new Error(`Erro na requisição: ${response.status}`);
    }

    return response.json();
}

export default apiFetch;