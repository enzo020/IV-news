const integrantes = [
  {
    nome: "Murilo Gandin",
    cargo: "Scrum Master, Desenvolvedor Front-end e Back-End",
  },

  {
    nome: "Enzo Souza Almeida",
    cargo: "Desenvolvedor Back-end",
  },
  {
    nome: "Maria Clara Paiva",
    cargo: "Administradora de Banco de dados",
  },
  {
    nome: "Adam Farah de Almeida Prado",
    cargo: "Pesquisa e levantamento de dados",
  },
  {
    nome: "Valter Junior",
    cargo: "Analista de prompts e contexto",
  },
];

const recursos = [
  {
    numero: "01",
    titulo: "Acesso às notícias",
    descricao:
      "Consulte notícias de diferentes fontes em um único ambiente, facilitando o acesso a informações atuais.",
  },
  {
    numero: "02",
    titulo: "Inteligência Artificial",
    descricao:
      "Utilizamos recursos de IA para auxiliar no processamento e na apresentação do conteúdo das notícias.",
  },
  {
    numero: "03",
    titulo: "Organização e consulta",
    descricao:
      "Explore conteúdos organizados por categorias e utilize os recursos de pesquisa para encontrar assuntos de interesse.",
  },
];

function About() {
  return (
    <main className="about-page container py-5">
      {/* Apresentação */}
      <section className="about-hero mb-5">
        <span className="about-eyebrow">CONHEÇA O PROJETO</span>

        <h1 className="about-title mt-3">
          Informação e tecnologia,
          <br />
          <span>conectadas em um só lugar.</span>
        </h1>

        <p className="about-intro mt-4">
          Seja bem-vindo(a) ao IV News! Somos um portal de notícias que combina
          tecnologias de informação e Inteligência Artificial para proporcionar
          uma experiência digital de consulta e visualização de notícias.
        </p>

        <a href="/noticias" className="btn about-button mt-3">
          Explorar notícias
          <span aria-hidden="true"> →</span>
        </a>
      </section>

      {/* Proposta */}
      <section className="about-section mb-5">
        <div className="row g-4 align-items-start">
          <div className="col-md-4">
            <span className="about-eyebrow">NOSSA PROPOSTA</span>

            <h2 className="about-heading mt-2">Mais acesso à informação</h2>
          </div>

          <div className="col-md-8">
            <p className="about-text">
              Em um cenário no qual a informação está cada vez mais presente no
              cotidiano, o IV News busca reunir tecnologia e conteúdo
              jornalístico em uma plataforma web acessível e organizada.
            </p>

            <p className="about-text">
              Nosso objetivo é facilitar a consulta de notícias, permitindo que
              os usuários explorem diferentes assuntos em um ambiente
              centralizado. Para isso, o projeto integra uma API de notícias e
              recursos de Inteligência Artificial, demonstrando como essas
              tecnologias podem contribuir para a apresentação e a organização
              de informações.
            </p>

            <p className="about-text">
              Mais do que desenvolver um portal, buscamos aplicar na prática os
              conhecimentos de Engenharia de Software, desde a construção da
              interface até o desenvolvimento do backend e a integração entre
              serviços.
            </p>
          </div>
        </div>
      </section>

      {/* Recursos */}
      <section className="about-section mb-5">
        <div className="mb-4">
          <span className="about-eyebrow">O QUE NOS DEFINE</span>

          <h2 className="about-heading mt-2">
            Tecnologia a serviço da informação
          </h2>

          <p className="about-text">Conheça alguns dos pilares do IV News.</p>
        </div>

        <div className="row g-3">
          {recursos.map((recurso) => (
            <div className="col-md-4" key={recurso.numero}>
              <article className="about-card h-100">
                <span className="about-card-number">{recurso.numero}</span>

                <h3 className="about-card-title mt-4">{recurso.titulo}</h3>

                <p className="about-text mb-0">{recurso.descricao}</p>
              </article>
            </div>
          ))}
        </div>
      </section>

      {/* Equipe */}
      <section className="about-team">
        <div className="row g-4 align-items-start">
          <div className="col-lg-5">
            <span className="about-eyebrow">QUEM ESTÁ POR TRÁS</span>

            <h2 className="about-heading mt-2">
              Feito por estudantes,
              <br />
              construído em equipe.
            </h2>

            <p className="about-text mt-3">
              Somos cinco estudantes de Engenharia de Software da Universidade
              Positivo, Câmpus Osório, em Curitiba, Paraná.
            </p>

            <p className="about-text">
              O IV News nasceu como uma oportunidade de transformar
              conhecimentos acadêmicos em uma aplicação prática, colaborativa e
              conectada aos desafios do desenvolvimento de software.
            </p>
          </div>

          <div className="col-lg-7">
            <div className="about-team-list">
              {integrantes.map((integrante, index) => (
                <div className="about-member" key={integrante.nome}>
                  <div className="about-avatar">
                    {integrante.nome
                      .split(" ")
                      .filter(Boolean)
                      .slice(0, 2)
                      .map((parte) => parte[0])
                      .join("")
                      .toUpperCase()}
                  </div>

                  <div>
                    <h3 className="about-member-name">{integrante.nome}</h3>

                    <p className="about-member-role">{integrante.cargo}</p>
                  </div>

                  <span className="about-member-number">
                    {String(index + 1).padStart(2, "0")}
                  </span>
                </div>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* Encerramento */}
      <section className="about-footer mt-5">
        <h2>Conhecimento que vira projeto.</h2>

        <p className="about-text mt-3 mb-0">
          IV News — unindo desenvolvimento de software, inovação e acesso à
          informação.
        </p>
      </section>
    </main>
  );
}

export default About;
