# Guia de execução 
- Sempre execute o comando `dotnet restore` após uma alteração estrutural para restaurar as dependências
.
- Para iniciar o backend, utilize o comando `dotnet run` no diretório raiz do backend.

- Para acessar a API, abra o navegador e vá para `http://localhost:5000` (ou a porta especificada no terminal com ctrl+click em cima do link gerado). 

- O swagger é uma ferramenta incluída no projeto, acessível em `http://localhost:5000/swagger` quando o servidor for iniciado, e serve para para testar os endpoints da API.

- Para iniciar o frontend, inicie um servidor local (como o Live Server do VS Code) no diretório raiz do frontend clicando com o botão direito no código do arquivo e selecionando "open with live server". Para isso, certifique-se de que a extensão Live Server esteja instalada no VS Code.

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
* **Junior** - Frontend / Documentação
* **Murilo** - Banco de Dados (SQLite)
* **Maria Clara** - Integração e Testes (Google Maps / IA)

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
