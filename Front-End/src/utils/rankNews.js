function calcularRelevancia(noticia) {
  let pontos = 0;

  const titulo = noticia.titulo?.trim() ?? "";
  const conteudo = noticia.conteudo?.trim() ?? "";
  const data = new Date(noticia.publicadoEm);
  const agora = Date.now();

  // Notícias com título válido são necessárias para o destaque.
  if (titulo.length < 15) {
    return -1;
  }

  pontos += 10;

  // Notícias com conteúdo útil recebem pontos adicionais.
  if (conteudo.length >= 100) pontos += 5;
  if (conteudo.length >= 300) pontos += 5;

  // Prioriza notícias recentes.
  if (!Number.isNaN(data.getTime())) {
    const idadeEmHoras = (agora - data.getTime()) / 3_600_000;

    if (idadeEmHoras >= 0 && idadeEmHoras <= 6) {
      pontos += 30;
    } else if (idadeEmHoras <= 24 && idadeEmHoras >= 0) {
      pontos += 20;
    } else if (idadeEmHoras <= 72 && idadeEmHoras >= 0) {
      pontos += 10;
    } else if (idadeEmHoras < 0) {
      // Data futura ou inconsistente.
      pontos -= 10;
    }
  }

  return pontos;
}

function normalizarTitulo(titulo = "") {
  return titulo
    .normalize("NFD")
    .replace(/[\u0300-\u036f]/g, "")
    .toLowerCase()
    .replace(/[^\p{L}\p{N}]+/gu, " ")
    .trim();
}

export function selecionarNoticiasDestaque(noticias, limite = 5) {
  const classificadas = noticias
    .map((noticia) => ({
      noticia,
      pontuacao: calcularRelevancia(noticia),
    }))
    .filter((item) => item.pontuacao >= 0)
    .sort((a, b) => {
      if (b.pontuacao !== a.pontuacao) {
        return b.pontuacao - a.pontuacao;
      }

      // Desempate por data de publicação.
      return (
        new Date(b.noticia.publicadoEm).getTime() -
        new Date(a.noticia.publicadoEm).getTime()
      );
    });

  const titulosUtilizados = new Set();
  const selecionadas = [];

  for (const item of classificadas) {
    const tituloNormalizado = normalizarTitulo(item.noticia.titulo);

    if (titulosUtilizados.has(tituloNormalizado)) {
      continue;
    }

    titulosUtilizados.add(tituloNormalizado);
    selecionadas.push(item.noticia);

    if (selecionadas.length >= limite) {
      break;
    }
  }

  return selecionadas;
}
