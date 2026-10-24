import { useState } from "react";

const perguntas = [
  {
    pergunta: "O que é o IV News?",
    resposta:
      "O IV News é um portal web de notícias que reúne conteúdos informativos em um só lugar. O projeto integra uma API de notícias e recursos de Inteligência Artificial para auxiliar na organização e na apresentação das informações.",
  },
  {
    pergunta: "De onde vêm as notícias exibidas no portal?",
    resposta:
      "As notícias são obtidas por meio de uma API externa que fornece conteúdos de diferentes fontes. O IV News organiza essas informações para facilitar a consulta e permite que o usuário acesse a publicação original quando o link está disponível.",
  },
  {
    pergunta: "Como posso encontrar notícias sobre um assunto específico?",
    resposta:
      "Você pode utilizar a barra de pesquisa para procurar notícias por termos de interesse e navegar pelas categorias disponíveis para explorar diferentes assuntos.",
  },
  {
    pergunta: "Para que servem os resumos gerados por Inteligência Artificial?",
    resposta:
      "Os resumos apresentam os principais pontos de uma notícia de forma mais breve, ajudando o leitor a compreender rapidamente o assunto. Eles são um recurso complementar e não substituem a leitura da publicação original.",
  },
  {
    pergunta: "Posso ler a notícia completa?",
    resposta:
      "Sim. Quando a notícia possui um link de origem disponível, você pode acessá-lo para consultar a publicação no site da fonte original. A disponibilidade do texto completo depende do site responsável pela publicação.",
  },
  {
    pergunta: "O IV News funciona em celulares?",
    resposta:
      "Sim. O portal pode ser acessado pelo navegador de celulares, tablets e computadores com acesso à internet. A experiência pode variar conforme o dispositivo e o navegador utilizado.",
  },
];

function Faq() {
  const [aberta, setAberta] = useState(null);

  function alternarPergunta(index) {
    setAberta((atual) => (atual === index ? null : index));
  }

  return (
    <main className="faq-page container py-5">
      <header className="faq-header">
        <span className="faq-eyebrow">CENTRAL DE AJUDA</span>

        <h1 className="faq-title mt-3">Perguntas frequentes</h1>

        <p className="faq-intro">
          Tem alguma dúvida sobre o IV News? Confira as respostas abaixo para
          conhecer melhor o portal e seus recursos.
        </p>
      </header>

      <section
        className="faq-list mt-5"
        aria-label="Perguntas e respostas frequentes"
      >
        {perguntas.map((item, index) => {
          const isOpen = aberta === index;
          const idResposta = `faq-resposta-${index}`;
          const idPergunta = `faq-pergunta-${index}`;

          return (
            <article
              className={`faq-item ${isOpen ? "is-open" : ""}`}
              key={item.pergunta}
            >
              <h2 className="faq-question-heading">
                <button
                  id={idPergunta}
                  className="faq-question"
                  type="button"
                  aria-expanded={isOpen}
                  aria-controls={idResposta}
                  onClick={() => alternarPergunta(index)}
                >
                  <span className="faq-number">
                    {String(index + 1).padStart(2, "0")}
                  </span>

                  <span className="faq-question-text">{item.pergunta}</span>

                  <span className="faq-chevron" aria-hidden="true">
                    {isOpen ? "−" : "+"}
                  </span>
                </button>
              </h2>

              <div
                id={idResposta}
                className="faq-answer-wrapper"
                role="region"
                aria-labelledby={idPergunta}
                hidden={!isOpen}
              >
                <p className="faq-answer">{item.resposta}</p>
              </div>
            </article>
          );
        })}
      </section>

      <section className="faq-footer mt-5">
        <h2>A informação ficou mais clara?</h2>

        <p>Explore o IV News e descubra as notícias disponíveis no portal.</p>

        <a href="/noticias" className="faq-button">
          Explorar notícias <span aria-hidden="true">→</span>
        </a>
      </section>
    </main>
  );
}

export default Faq;
