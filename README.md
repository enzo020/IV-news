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

Now listening on: http://localhost:5000

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

---
