# IV News

O **IV News** é uma plataforma web de notícias que utiliza APIs externas para obter notícias, extrai e organiza o conteúdo dos artigos e utiliza Inteligência Artificial para gerar resumos executivos. As notícias e seus resumos são armazenados em um banco de dados e exibidos em uma interface desenvolvida com React.

## 1. Tecnologias utilizadas

| Camada | Tecnologias |
|---|---|
| Frontend | React, JavaScript, Vite, Bootstrap |
| Backend | C#, ASP.NET Core, .NET 8 |
| Banco de dados | SQLite, Entity Framework Core |
| Fonte de notícias | APITube |
| Extração de conteúdo | SmartReader |
| Processamento de texto | Google Gemini API |
| Versionamento e gestão | Git, GitHub e Trello |

## 2. Fluxo de processamento das notícias

O fluxo esperado conecta a obtenção das notícias, a extração do conteúdo, o processamento por Inteligência Artificial, o armazenamento dos resultados e a exibição no frontend.

### Etapa 1 — Notícia armazenada no banco de dados

As notícias são obtidas da APITube e processadas pelo backend. Cada registro contém informações como título, descrição, autor, fonte, URL original, imagem, data de publicação, categoria e localização, quando disponíveis.

O banco utilizado é o SQLite, acessado por meio do Entity Framework Core.

O identificador externo da APITube (`IdExterno`) é utilizado para verificar se uma notícia já foi importada, evitando duplicações.

### Etapa 2 — Extração do conteúdo pela URL

O backend utiliza o serviço de extração baseado no **SmartReader** para acessar a URL original da notícia e tentar extrair seu conteúdo textual.

O objetivo é obter o texto principal do artigo, reduzindo a presença de anúncios, menus, elementos de navegação e outros conteúdos que não fazem parte da notícia.

O resultado dessa etapa é o texto extraído, que será utilizado como entrada para o processamento por Inteligência Artificial.

A extração depende de a URL estar acessível e de o site permitir a obtenção do conteúdo. Páginas indisponíveis, paywalls e outras restrições podem impedir uma extração bem-sucedida.

### Etapa 3 — Envio do texto ao Google Gemini

Após a extração, o backend envia o texto obtido para o serviço de processamento de notícias, que utiliza a API do Google Gemini.

A IA recebe instruções para preservar o sentido original do artigo, organizar o conteúdo e produzir informações estruturadas, incluindo:

- **Descrição:** texto curto para apresentação da notícia.
- **Conteúdo tratado:** versão limpa e organizada do conteúdo extraído.
- **Resumo executivo:** síntese dos principais acontecimentos.
- **Pontos-chave:** destaques relevantes do artigo.
- **Sentimento:** classificação do tom da notícia, conforme o processamento implementado.

A resposta é processada e validada pelo backend antes de ser utilizada. Respostas inválidas ou incompletas podem impedir que a notícia seja salva, conforme as validações da aplicação.

### Etapa 4 — Armazenamento da notícia e do resumo

Depois que a extração e o processamento são concluídos com sucesso, o backend cria os registros correspondentes no banco de dados.

A entidade `Noticia` armazena os dados do artigo, incluindo o conteúdo tratado e a data de publicação. A entidade `Resumo` armazena o resumo executivo e as informações complementares geradas pela IA.

O relacionamento entre as entidades permite associar o resumo à notícia correspondente.

O Entity Framework Core persiste os registros por meio de `SaveChangesAsync()`. A aplicação também verifica duplicidades antes de inserir novas notícias.

**Observação:** no fluxo de importação atual, a extração e o processamento são realizados antes da persistência da nova notícia e de seu resumo. Para notícias já armazenadas, o fluxo de extração e processamento pode ser executado separadamente, conforme os endpoints e serviços disponíveis no backend.

### Etapa 5 — Exibição no frontend

O frontend React consulta os endpoints do backend para obter as notícias e seus dados relacionados.

As informações recebidas são utilizadas pelos componentes da interface para apresentar:

- Cards com título, imagem, descrição e data de publicação.
- Página de detalhes com o conteúdo da notícia.
- Resumo executivo e pontos-chave, quando disponíveis.
- Categorias, localização e demais informações fornecidas pela API.

O frontend é responsável pela apresentação dos dados. A extração do artigo, o processamento com IA e a persistência ficam sob responsabilidade do backend.

### Visão geral do fluxo

```text
APITube
   |
   v
Backend (.NET 8)
   |
   v
Obtenção dos dados e URL da notícia
   |
   v
Extração do conteúdo com SmartReader
   |
   v
Envio do texto ao Google Gemini
   |
   v
Validação da resposta
   |
   v
SQLite
   |
   +---- Noticia
   |
   +---- Resumo
   |
   v
API do backend
   |
   v
Frontend React
   |
   v
Exibição da notícia e do resumo
```

## 3. Pré-requisitos

Antes de executar o projeto, instale:

- **.NET SDK 8.0:** necessário para o backend. [Download do .NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- **Node.js e npm:** necessários para o frontend React.
- **Git:** necessário para clonar e versionar o projeto.

Não é necessário instalar manualmente as dependências do projeto. Os comandos abaixo realizam a restauração ou instalação.

O VS Code é recomendado, mas qualquer editor compatível pode ser utilizado.

## 4. Configuração das APIs

As chaves de API **não devem ser adicionadas diretamente ao código nem enviadas ao GitHub**.

No terminal, entre no diretório do backend:

```bash
cd backend
```

Inicialize o armazenamento de segredos do .NET, caso ainda não esteja configurado:

```bash
dotnet user-secrets init
```

Configure o token da APITube:

```bash
dotnet user-secrets set "ApiTubeSettings:Token" "SUA_API_KEY"
```

Configure a chave da API do Google Gemini:

```bash
dotnet user-secrets set "Gemini:ApiKey" "SUA_API_KEY"
```

Substitua `SUA_API_KEY` pela chave correspondente de cada serviço.

Os principais serviços de integração com APIs externas estão na pasta `Services` do backend.

## 5. Execução do backend

No terminal, entre na pasta do backend:

```bash
cd backend
```

Restaure as dependências:

```bash
dotnet restore
```

Inicie a aplicação:

```bash
dotnet run
```

O terminal exibirá o endereço utilizado pelo servidor, por exemplo:

```text
Now listening on: http://localhost:5022
```

A porta pode variar de acordo com a configuração local. Utilize o endereço exibido no terminal.

### Swagger

Com o backend em execução, acesse o Swagger pelo endereço configurado para a documentação da API.

Por exemplo, se a documentação estiver habilitada na raiz:

[http://localhost:5022/](http://localhost:5022/)

Caso seja utilizada outra porta ou rota, consulte a configuração do projeto e a saída do terminal.

O Swagger permite consultar e testar os endpoints disponíveis no backend.

## 6. Execução do frontend

O frontend utiliza React e Vite.

Abra outro terminal e entre na pasta do frontend:

```bash
cd frontend
```

Instale as dependências:

```bash
npm install
```

Inicie o servidor de desenvolvimento:

```bash
npm run dev
```

O terminal exibirá um endereço semelhante a:

[http://localhost:5173](http://localhost:5173)

Abra o endereço no navegador. Para utilizar as funcionalidades que dependem de notícias, mantenha o backend em execução e confira se a URL da API está configurada corretamente no frontend.


## 7. Estrutura geral do projeto

O projeto é dividido em duas partes principais:

- **`backend/`**: API ASP.NET Core, serviços de integração, extração e processamento de notícias, entidades, DTOs e acesso ao banco SQLite.
- **`frontend/`**: aplicação React, componentes visuais, páginas e consumo dos endpoints do backend.

A estrutura interna pode variar conforme a evolução do projeto. Consulte as pastas e os arquivos existentes no repositório para identificar os serviços e endpoints disponíveis.
## 8. Objetivos do MVP

O MVP do IV News busca oferecer:

1. Obtenção e armazenamento de notícias por meio de uma API externa.
2. Extração e organização do conteúdo dos artigos.
3. Geração e armazenamento de resumos com Inteligência Artificial.
4. Exibição das notícias em uma interface web responsiva.
5. Filtros e organização por categorias e localização, conforme as funcionalidades implementadas.

Funcionalidades adicionais, como geração de imagens por IA, podem ser incorporadas conforme o planejamento e a evolução do projeto.
