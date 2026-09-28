# Guia de execução
## Pré-requisitos

Antes de executar o projeto, instale:

.NET SDK 8.0 — necessário para o backend.
https://dotnet.microsoft.com/pt-br/download/dotnet/8.0
Node.js e npm — necessários para o frontend React.
Git — para clonar e versionar o projeto.

Não é necessário instalar as dependências do projeto manualmente. Elas são instaladas pelos comandos descritos abaixo.

O projeto pode ser desenvolvido em qualquer editor de código. O VS Code é recomendado, mas não é obrigatório.

Backend

Entre no diretório do backend:

cd backend

Restaure as dependências:

dotnet restore

Inicie o servidor:

dotnet run

O terminal exibirá o endereço em que a API está sendo executada, por exemplo:

Now listening on: http://localhost:5022

A porta pode variar de acordo com a configuração local. Utilize sempre o endereço exibido no terminal.

# Swagger

Com o backend em execução, o Swagger pode ser acessado pelo endereço com ctrl+click:

http://localhost:5022/

Caso a API esteja utilizando outra porta, substitua 5022 pela porta exibida no terminal.

O Swagger permite visualizar e testar os endpoints disponíveis na API.

# Frontend

O frontend utiliza React + Vite.

Entre no diretório do frontend:

cd frontend

Instale as dependências:

npm install

Inicie o servidor de desenvolvimento:

npm run dev

O terminal exibirá um endereço semelhante a:

http://localhost:5173

Abra esse endereço no navegador com ctrl+click.

# Guia de branches e versionamento git
- O projeto utiliza o Git para versionamento de código. As branches seguem o padrão:
  - main: Branch principal onde o código é compartilhado por todos.
  - (Seu nome): Branch para desenvolvimento das novas funcionalidades.

O desenvolvimento deve seguir o fluxo:
- Antes de iniciar qualquer desenvolvimento, certifique-se de estar na branch main executando `git checkout main`.
- Execute `git pull origin main` para atualizar sua branch local com as últimas alterações da main.
- Crie uma branch a partir da main com o seu nome (crie com `git checkout -b <seu-nome>`).
- Ou acesse uma já existente com o comando `git checkout <seu-nome>`.
- Desenvolva suas funcionalidades na sua branch pessoal.
- Após concluir o desenvolvimento, faça commit das alterações com `git add .` e `git commit -m "Descrição do que foi feito"`.
- Para enviar suas alterações para o repositório remoto, utilize `git push origin <seu-nome>` (as alterações só serão enviadas para a sua branch).
- Para integrar suas alterações à branch main, acesse a branch main com `git checkout main`, atualize-a com `git pull origin main` e depois faça o merge da sua branch pessoal com `git merge <seu-nome>`.
- Por fim, envie as alterações integradas para o repositório remoto com `git push origin main`.

## Configuração das APIs

As API keys NÃO devem ser adicionadas ao código ou ao GitHub.

Após clonar o projeto, configure sua API key:

dotnet user-secrets init
(exemplo, para o APITube, substitua "SUA_API_KEY" pela sua chave real):
dotnet user-secrets set "ApiTubeSettings:Token" "SUA_API_KEY"

APITube: link padrão para noticias em portugues
https://api.apitube.io/v1/news/everything?language.code=pt

Os principais arquivos da pasta Services servirão para configurar as APIs externas.

# 📰 IV News

O **IV News** é uma plataforma web para exibição de notícias em tempo real, integrando Inteligência Artificial (Grok) para a geração automática de resumos e classificação indicativa, além de recursos de localização e mapas.

---

## 👥 Integrantes do Projeto
* **Enzo** - Backend (C# / ASP.NET)
* **Adam** - Frontend / Documentação
* **Junior** -  Documentação e IAs
* **Murilo** - Front-end(React)
* **Maria Clara** - Banco de Dados (SQLlite)

---

## 🛠️ Tecnologias Utilizadas

* **Backend:** C# / ASP.NET Core, Entity Framework Core
* **Frontend:** HTML, CSS, JavaScript, Bootstrap
* **Banco de Dados:** SQLite
* **Inteligência Artificial:** Grok (xAI)
* **APIs Externas:** News API, Google Maps API
* **Versionamento & Gestão:** Git/GitHub, Trello

---

## 🎯 Objetivo do Projeto (MVP)
O sistema tem como meta principal entregar:
1. Feed de notícias atualizado via API.
2. Interface responsiva com cards e filtros.
3. Resumos e classificação indicativa gerados por IA.
4. Integração básica com Google Maps para eventos da região.

---

## 📌 Padrão de Branches
* `main`: Branch principal de produção (código estável).
* `develop`: Branch para integração das novas funcionalidades.
* `feature/nome-da-feature`: Branches individuais para desenvolvimento de tarefas.

2. Definir stack final
================================================================
                         STACK FINAL
================================================================

CAMADA DE NOTÍCIAS
    Principal: APITube-100 Requisições (Qualidade boa de resumo)
    Reserva:   NewsData.io

CAMADA DE IA — TEXTO
    Principal: Google Gemini API
    Reserva:   Groq API

CAMADA DE IA — IMAGEM
    Principal: Cloudflare Workers AI
    Reserva:   Stability AI API

CAMADA DE IA — VÍDEO
    Principal: fal.ai
    Reserva:   Magic Hour API

================================================================

Formato Esperado do Resumo

Para padronizar o consumo pela sua aplicação, o retorno da IA deve seguir uma estrutura fixa em JSON validado:

{
  "titulo": "Título curto e chamativo (máx. 10 palavras)",
  "resumo_executivo": "Parágrafo com o contexto geral e principal fato da notícia (2 a 3 frases).",
  "pontos_chave": [
    "Destaque relevante 1",
    "Destaque relevante 2",
    "Destaque relevante 3"
  ],
  "categoria": "Ex: Tecnologia | Economia | Política",
  "sentimento": "Neutro | Positivo | Negativo"
}

Exemplo de Prompt (System Prompt)

Este prompt garante um retorno conciso e previne a inclusão de formatação Markdown extra fora do objeto JSON:

> Role: Você é um assistente especialista em síntese de notícias para feeds rápidos.
> Instrução: Analise o texto da notícia fornecido e crie um resumo objetivo. Retorne estritamente um objeto JSON sem blocos de código markdown ou texto explicativo extra.
> Regras:
> * Mantenha o tom neutro e baseado estritamente no texto fornecido.
> * O campo resumo_executivo não deve ultrapassar 60 palavras.
> * O campo pontos_chave deve conter exatamente 3 itens curtos.
> Texto da Notícia: {texto_noticia_aqui}
> 

Documentação da API Escolhida (Google Gemini API — Principal)

 * Modelo Recomendado: gemini-2.5-flash ou gemini-1.5-flash (ideais para inferência rápida e baixo custo).
 * Configuração de Resposta: Suporta response_mime_type: "application/json" para garantir o Schema JSON sem erros de parse.
 * Limite da Camada Gratuita:
   * 15 RPM (Requisições por minuto)
   * 1.000 a 1.500 RPD (Requisições por dia)
   * 1M Tokens de Janela de Contexto
 * Tamanho Máximo de Entrada: Suporta até ~700.000 palavras por requisição na camada gratuita (mais que suficiente para notícias).

Definição do Plano B (Groq API — Reserva)

 * Propósito: Assumir as chamadas caso o limite de requisições (429 Too Many Requests) ou instabilidade ocorra na Google Gemini API.
 * Modelo Recomendado: llama-3.3-70b-versatile ou gpt-oss-120b (rodando na infraestrutura ultra-rápida LPU da Groq).
 * Limite da Camada Gratuita:
   * 30 RPM (Requisições por minuto)
   * 1.000 RPD (Requisições por dia)
   * 200.000 TPD (Tokens por dia)
 * Comportamento de Failover: O backend deve interceptar o código HTTP 429 ou erros de timeout da API do Gemini e redirecionar o payload (noticia + prompt) para o endpoint da Groq via SDK da OpenAI / Groq.
================================================================
Portais de notícias:

G1
Link: G1. G1 – O portal de notícias da Globo. [S. l.], [s. d.]. Disponível em: https://g1.globo.com/. Acesso em: 27 set. 2026.
Elementos para o IV News: 
Conteúdo regionalizado — possibilidade de encontrar notícias específicas de diferentes estados e regiões, aproximando o conteúdo do público local.
Cobertura de um mesmo acontecimento — várias notícias relacionadas podem ser agrupadas em torno de um assunto/evento.
CNN
Link: CNN. Breaking News, Latest News and Videos. [S. l.], [s. d.]. Disponível em: https://edition.cnn.com/. Acesso em: 27 set. 2026.

Elementos para o IV News: 
Notícias relacionadas
Ao final ou durante uma matéria, apresentar outros conteúdos sobre o mesmo assunto. Isso permite que o leitor continue pesquisando um tema sem precisar voltar à página inicial.

Data e horário da publicação/atualização
Informar claramente quando a notícia foi publicada ou atualizada. Isso é especialmente útil em notícias que sofrem alterações ao longo do dia.


BBC
Link: BBC NEWS BRASIL. Notícias, vídeos, análise e contexto em português. [S. l.], [s. d.]. Disponível em: https://www.bbc.com/portuguese. Acesso em: 27 set. 2026.

Elementos para o IV News: 
Subtítulos dentro da matéria
As matérias são divididas em blocos por subtítulos, facilitando a leitura de textos longos e permitindo que o leitor identifique rapidamente cada parte do conteúdo.

Links para contextualização
Durante a matéria, determinados termos e acontecimentos podem direcionar o leitor para outros conteúdos que ajudam a entender o contexto da notícia.
================================================================
Google Notícias — recursos de IA

Google Notícias

O Google Notícias reúne diferentes fontes sobre um mesmo assunto e permite que o usuário acompanhe diversas publicações relacionadas.
Link: GOOGLE NOTÍCIAS. Google Notícias. [S. l.], [s. d.]. Disponível em: https://news.google.com/home?hl=pt-PT&gl=PT&ceid=PT%3Apt-150. Acesso em: 27 set. 2026.

Perplexity — interface que apresenta uma resposta resumida por IA acompanhada das fontes utilizadas.

Link: PERPLEXITY. Perplexity. [S. l.], [s. d.]. Disponível em: https://www.perplexity.ai/?login-new=false&login-source=oneTapHome. Acesso em: 27 set. 2026.

Google AI Overviews

Resumo gerado por IA: apresentar os principais pontos da notícia de forma curta.
Links para as fontes: permitir que o leitor consulte as fontes utilizadas.

Perplexity

Resumo em linguagem simples: facilitar a compreensão rápida do assunto.
Referências das fontes: mostrar de onde as informações foram obtidas.

Esses recursos poderiam ser adicionados à página das notícias como funcionalidades complementares, mantendo o layout, as cores, a tipografia e a identidade visual já definidos para o IV News.
================================================================
Resumo gerado por IA
Apresentar os principais pontos da notícia de forma rápida, permitindo que o leitor compreenda o assunto antes de ler a matéria completa.
Notícias relacionadas
Mostrar outras notícias sobre o mesmo assunto, facilitando a continuidade da leitura e o aprofundamento do tema.
Links para as fontes
Disponibilizar as fontes utilizadas na elaboração ou no resumo da notícia, permitindo que o leitor consulte as informações originais.



