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
## 📌 APIS
API de notícias:
1. NewsData.io
Link de acesso:https://newsdata.io/
...................................

Limite gratuito
200 requisições/créditos por dia
Cada crédito pode retornar até 10 artigos
Potencial teórico: até 2.000 artigos/dia
O limite é renovado diariamente.

Resultado

200 requisições/dia → até 2.000 artigos/dia
...................................
Cobertura brasileira: ✅ SIM

A NewsData.io possui uma página específica de Brazil News API, demonstrando que o Brasil é uma cobertura suportada. A API permite recuperar notícias brasileiras e trabalhar com fontes do país.

Conclusão:
✅ Cobertura brasileira confirmada
....................................
1. NewsData.io
Limite gratuito
200 requisições/créditos por dia
Cada crédito pode retornar até 10 artigos
Potencial teórico: até 2.000 artigos/dia
O limite é renovado diariamente.
Resultado

200 requisições/dia → até 2.000 artigos/dia
............................................
Filtro por país: ✅ SIM

A NewsData.io possui parâmetro específico de país.

É possível informar o código do país, por exemplo:

country=br

para restringir os resultados ao Brasil.

A documentação também informa filtros por idioma, localização, publisher e outros critérios.

Exemplo conceitual:

GET /api/1/latest?country=br&apikey=SUA_CHAVE
Resultado

🟢 Possui filtro de país nativo.
...................................
Cidade/região: 🟢 SIM — mas com uma limitação importante

A NewsData.io possui um parâmetro chamado region.

Ele pode representar:

cidade;
distrito;
condado;
estado;
região;
país;
continente.

Exemplo conceitual:

region=curitiba-parana-brazil

A própria documentação dá exemplos de cidades como Nashville e Seattle e explica que é possível combinar cidade + estado + país para evitar ambiguidades. Também permite até 5 localizações na mesma consulta.

⚠️ Porém:

O parâmetro region é disponibilizado somente para usuários Corporate.

Ou seja:

No Free: ❌ não podemos contar com esse filtro.

No projeto gratuito: a alternativa seria pesquisar o nome da cidade através de palavras-chave, mas isso não é equivalente a um filtro geográfico nativo.

Resultado

🟡 Possui filtro de cidade/região, mas não no plano gratuito.
................................................
Disponibilidade de imagem

🟢 SIM

A resposta dos artigos possui o campo:

image_url

Esse campo contém a URL da imagem associada ao artigo.

A API também possui video_url em determinados resultados.

Resultado

Imagem: 🟢 SIM
URL da imagem no JSON: 🟢 SIM
Campo: image_url
........................................................
Termos de uso

🟡 POSSUI RESTRIÇÕES IMPORTANTES

A própria NewsData.io alerta que as imagens e vídeos retornados pela API pertencem aos respectivos proprietários.

A orientação oficial é:

não publicar livremente imagens/vídeos recebidos;
utilizar thumbnail de tamanho reduzido quando apropriado;
respeitar os direitos autorais da fonte original.

A própria plataforma afirma que publicar imagens e vídeos integralmente pode gerar problemas de copyright.

Resultado

Uso dos dados: 🟢 SIM
Uso comercial: 🟡 depende dos termos/plano
Texto integral: 🟡 atenção aos direitos da fonte
Imagem original: 🔴 não é recomendável simplesmente republicar
Thumbnail: 🟢 opção indicada pela própria NewsData.io
Direitos autorais: ⚠️ responsabilidade do usuário
...........................................................
Necessidade de cartão

🟢 NÃO

O plano Free fornece os créditos gratuitos e não há indicação de necessidade de cartão para começar.

O plano gratuito atualmente oferece 200 créditos/dia.

Resultado

Cartão de crédito: 🟢 NÃO
.........................................................

2. GNews

Link de acesso: https://gnews.io/
...................................

Limite gratuito
100 requisições/dia
Até 10 artigos por requisição
Potencial teórico: até 1.000 artigos/dia
O contador é zerado diariamente.
Resultado

100 requisições/dia → até 1.000 artigos/dia
............................................
Cobertura brasileira: ✅ SIM

A própria GNews possui uma página específica de Brazil News API, com exemplo de requisição para obter as principais notícias do Brasil.

Também é possível utilizar o código de país br para direcionar a busca ao Brasil.

Conclusão:
✅ Cobertura brasileira confirmada
....................................
Filtro por país: ✅ SIM

O GNews possui explicitamente o parâmetro:

country

O valor utiliza códigos de duas letras.

Por exemplo:

country=br

para Brasil.

A documentação oficial lista Brazil (br) entre os países suportados. O parâmetro funciona tanto no endpoint de pesquisa quanto no endpoint de top headlines.

Há uma observação importante: no endpoint de pesquisa, o país indica onde a notícia foi publicada, e o conteúdo necessariamente não precisa ser sobre aquele país.

Resultado

🟢 Possui filtro de país nativo e documentado.
...................................
Cidade/região: 🟡 PARCIAL

O GNews permite filtro por:

país;
idioma;
tópico;
palavras-chave;
fontes;
datas.

Porém, não possui um parâmetro nativo equivalente a city=Curitiba ou region=Paraná.

Podemos pesquisar:

q=Curitiba

ou:

q="Curitiba"

mas isso é pesquisa textual, não geolocalização.

Por exemplo, uma matéria publicada em São Paulo que mencione Curitiba também poderá aparecer.

Resultado

🟡 Não possui filtro geográfico de cidade/região nativo.
...........................................................
Link de acesso: GNews

...................................

Disponibilidade de imagem

🟢 SIM

A resposta JSON possui o campo:

image

Ele contém a URL da imagem principal associada à notícia.

Resultado

Imagem: 🟢 SIM
URL da imagem no JSON: 🟢 SIM
Campo: image
.........................................................
Termos de uso

🟢 PERMITE USO COMERCIAL DA API

Os termos atuais, atualizados em 22 de junho de 2026, dizem expressamente que os dados obtidos pela API podem ser utilizados para fins comerciais.

Porém:

⚠️ o fato de a API permitir uso comercial não significa que o usuário adquira os direitos autorais das notícias ou imagens.

Os próprios termos determinam que:

direitos autorais de terceiros devem ser respeitados;
imagens e mídia podem estar protegidas por copyright;
o usuário é responsável por possuir os direitos necessários;
a origem dos dados não pode ser falsificada.

Resultado

Uso comercial: 🟢 SIM
Uso acadêmico: 🟢 SIM
Texto de terceiros: ⚠️ direitos autorais
Imagens: ⚠️ direitos autorais
Alterar atribuição/origem: 🔴 NÃO
Termos: 🟢 claros
..........................................................
Necessidade de cartão

🟢 NÃO

A própria GNews informa explicitamente:

“No credit card needed”

O cadastro gratuito fornece a API key imediatamente.

Resultado

Cartão de crédito: 🟢 NÃO
..........................................................
3. NewsAPI.org
Link de acesso: https://newsapi.org/
...................................
Limite gratuito
100 requisições/dia
Não há requisições adicionais gratuitas depois do limite.
O plano Developer possui atraso de 24 horas.
Resultado

100 requisições/dia

Atenção: o limite de 100/dia é bom para testes, mas o plano gratuito é destinado a desenvolvimento e testes, não produção comercial.
..................................
Cobertura brasileira: ✅ SIM

A NewsAPI possui uma página específica para Brazil News API. O endpoint top-headlines permite utilizar:

country=br

A própria documentação demonstra chamadas de notícias brasileiras e possui categorias como negócios, entretenimento, saúde, ciência, esportes e tecnologia.

A NewsAPI também informa que trabalha com mais de 150.000 fontes globalmente.

Conclusão:
✅ Cobertura brasileira confirmada.
...................................
Filtro por país: ✅ SIM

A NewsAPI possui o parâmetro:

country

no endpoint:

/v2/top-headlines

É possível utilizar:

country=br

para Brasil.

A documentação oficial diz explicitamente que o endpoint fornece headlines por país, categoria, fonte ou palavra-chave.

A documentação também lista mais de 50 países disponíveis para o endpoint de headlines.

Resultado

🟢 Possui filtro de país nativo.
...................................
Cidade/região: 🟡 PARCIAL

A NewsAPI possui:

country;
sources;
q;
category;
datas.

Mas o endpoint top-headlines não possui um parâmetro de cidade ou estado equivalente a:

city=Curitiba

Podemos pesquisar:

q=Curitiba

no endpoint de pesquisa, mas novamente isso é busca textual.

Exemplo
q=Curitiba

pode encontrar:

“Prefeitura de Curitiba anuncia...”

mas também:

“Empresa de São Paulo anuncia investimento em Curitiba.”

Não é uma filtragem geográfica real.

Resultado

🟡 Cidade/região apenas indiretamente por pesquisa textual.
...........................................................
Disponibilidade de imagem

🟢 SIM

A resposta possui:

urlToImage

Esse campo fornece a URL para uma imagem relevante do artigo.

Resultado

Imagem: 🟢 SIM
URL da imagem no JSON: 🟢 SIM
Campo: urlToImage
..........................................................
Termos de uso

🔴 RESTRITIVO PARA PRODUÇÃO/REPUBLICAÇÃO

Os termos proíbem, entre outras coisas:

reproduzir ou republicar material protegido por copyright;
remover avisos de copyright;
remover atribuições;
tentar contornar limites da API;
criar uma base de notícias concorrente.

Além disso, o conteúdo retornado pode conter:

texto;
imagens;
vídeos;

que continuam sujeitos aos direitos dos respectivos terceiros.

Resultado

Uso acadêmico/testes: 🟢 SIM
Uso comercial: 🔴 o plano Developer gratuito não é adequado para produção comercial
Republicar conteúdo protegido: 🔴 NÃO
Imagens: 🔴 não podem ser tratadas como suas
Criar base própria de notícias: 🔴 NÃO
Respeitar atribuição/copyright: 🔴 obrigatório

⚠️ Para o seu projeto, é uma das APIs que exige mais cuidado.
............................................................
Necessidade de cartão

🟢 NÃO

A própria NewsAPI informa que o plano Developer é gratuito para desenvolvimento e testes e que não é necessário cartão de crédito.

Resultado

Cartão de crédito: 🟢 NÃO

⚠️ O problema do NewsAPI não é o cartão; são as restrições do plano gratuito para produção.

4. APITube
Link de acesso: https://apitube.io/
...................................
Limite gratuito
100 requisições/dia
Até 10 artigos por requisição
10 requisições/minuto
O limite diário é renovado diariamente.
Depois de atingir o limite, as próximas chamadas retornam erro até a renovação.
Resultado

100 requisições/dia → até 1.000 artigos/dia

Além disso, há o limite de velocidade:

10 requisições/minuto.
...................................
Cobertura brasileira: ✅ SIM

A APITube informa cobertura de notícias em 200+ países e 60+ idiomas, com fontes verificadas.

A API permite filtrar pela localização da fonte utilizando códigos de país, portanto o Brasil pode ser selecionado como país de origem das fontes.

Conclusão:
✅ Cobertura brasileira confirmada.
...................................
Filtro por país: ✅ SIM

A APITube possui um sistema de filtros bastante amplo, com 65+ filtros, e trabalha com fontes de mais de 170/200 países, dependendo da página/documentação consultada.

O sistema permite filtrar as notícias de acordo com o país da fonte.

Portanto, é possível direcionar a consulta para determinado país, incluindo o Brasil.

Resultado

🟢 Possui filtro de país.

Além disso, é uma das APIs com maior quantidade de filtros disponíveis entre as opções pesquisadas.
Cidade/região: 🟢 SIM — EXCELENTE

Aqui temos uma das melhores soluções de toda a pesquisa.

A APITube possui um endpoint específico:

/v1/news/local

Ele permite procurar notícias próximas de uma localização. Podemos informar:

latitude;
longitude;
nome do local;
país para desambiguar;
raio em quilômetros.

Por exemplo, podemos conceitualmente pesquisar:

place=Curitiba
country=BR

ou utilizar coordenadas:

lat
lng
radius

O raio padrão é de 50 km e pode chegar a 20.000 km. Os resultados podem ser classificados por distância.

Além disso, o endpoint normal também permite:

location.name
location.lat
location.lng
location.radius
location.bbox

e o location.bbox permite definir uma área retangular.

Isso é excelente para:

cidade;
região metropolitana;
estado;
região geográfica;
área delimitada por coordenadas.
Exemplo

Para uma área de Curitiba:

location.lat = ...
location.lng = ...
location.radius = 50

Ou podemos usar uma bounding box.

Resultado

🟢 SIM — filtro geográfico extremamente completo.

🏆 Uma das melhores APIs para este requisito.
...................................
Disponibilidade de imagem

🟢 SIM

A API fornece dados estruturados dos artigos e possui documentação OpenAPI com os campos de resposta.

Os resultados podem incluir imagens associadas ao conteúdo da notícia.

Resultado

Imagem: 🟢 SIM
Imagem associada ao artigo: 🟢 SIM
URL/metadata de imagem: 🟢 disponível na resposta conforme o recurso utilizado.
...................................................
Termos de uso

🟢 PERMITE CONSTRUIR PRODUTOS COM OS DADOS

A documentação atual explica que a licença permite utilizar os dados da API para construir websites e produtos.

Porém, existem restrições:

não reproduzir/republicar conteúdo protegido sem autorização;
não remover atribuição/copyright;
não construir uma base de notícias concorrente;
APITube não transfere a propriedade intelectual do conteúdo para você.

A própria documentação diferencia claramente usar os dados da API de possuir os direitos autorais do conteúdo jornalístico.

Resultado

Uso em aplicação: 🟢 SIM
Uso comercial: 🟢 SIM, conforme licença/plano
Republicar artigo completo: 🔴 NÃO sem direitos adicionais
Imagem original: ⚠️ direitos do publisher
Atribuição: 🟢 deve ser preservada quando exigida
Criar produto: 🟢 SIM
...........................................................
Necessidade de cartão

🟢 NÃO

A APITube informa explicitamente que o Free Plan:

custa US$0;
oferece 100 requisições/dia;
não exige cartão de crédito.

Resultado

Cartão de crédito: 🟢 NÃO
............................................................
5. Mediastack
Link de acesso: https://mediastack.com/
...................................
Limite gratuito
100 requisições/mês
Não é 100 por dia.
Isso equivale, em média, a aproximadamente 3,3 requisições/dia se distribuído uniformemente pelo mês.
Resultado

100 requisições/mês

É um limite significativamente menor que NewsData.io, GNews e APITube.
................................
Cobertura brasileira: ✅ SIM

Aqui a confirmação é particularmente clara.

A Mediastack possui uma página específica de Brazil News API e informa que basta utilizar:

country=br

para recuperar notícias do Brasil. Também oferece categorias brasileiras como:

geral;
negócios;
entretenimento;
saúde;
ciência;
esportes;
tecnologia.

A plataforma informa cobertura em mais de 50 países e 7.500+ fontes.

Conclusão:
✅ Cobertura brasileira confirmada.
...................................
Filtro por país: ✅ SIM

A Mediastack possui parâmetro:

country

O sistema permite selecionar o país das notícias.

A própria Mediastack possui páginas específicas por país e demonstra a utilização do parâmetro country. Por exemplo, para Portugal:

country=pt

Para o Brasil, existe uma página específica de Brazil News API.

Resultado

🟢 Possui filtro de país nativo.
...................................
Cidade/região: 🔴 NÃO como filtro geográfico nativo

A Mediastack permite trabalhar com:

país;
idioma;
categoria;
fonte;
palavras-chave;
datas.

Mas não apresenta um filtro geográfico de cidade equivalente a:

city=Curitiba

É possível pesquisar:

keywords=Curitiba

mas isso é pesquisa textual.

Resultado

🟡 Não possui filtro nativo de cidade/região.
............................................
Disponibilidade de imagem

🟢 SIM

A API de notícias fornece informações estruturadas dos artigos, incluindo dados de mídia quando disponíveis.

Resultado

Imagem: 🟢 SIM
Imagem associada à notícia: 🟢 SIM

⚠️ A disponibilidade efetiva pode depender da fonte/artigo.
............................................................
Termos de uso

🟡 RESTRITIVO

Os termos concedem uma licença limitada, não exclusiva e revogável para acessar e utilizar a API.

A utilização é voltada para disponibilizar os dados aos usuários da aplicação, mas os termos restringem:

copiar;
modificar;
distribuir;
vender;
redistribuir;
sublicenciar;
explorar comercialmente os dados fora do escopo autorizado.

Também existe uma regra particularmente importante: os usuários finais devem utilizar os dados para uso pessoal, conforme os termos atuais.

Resultado

Uso em aplicação: 🟢 SIM
Uso comercial amplo: 🔴/🟡 restrito
Redistribuição: 🔴 NÃO
Venda dos dados: 🔴 NÃO
Imagens: ⚠️ direitos de terceiros
Uso acadêmico: 🟢 SIM, respeitando os termos
........................................................
Necessidade de cartão

🟡 NÃO CONFIRMADO DE FORMA CLARA PARA O CADASTRO FREE

A página atual confirma que existe o Free Plan com até 100 requisições/mês e lista cartões como formas de pagamento dos planos pagos.

Entretanto, não encontrei uma declaração atual tão explícita quanto:

“No credit card required for Free Plan.”

Portanto, para não inventar:

Cartão no plano gratuito: 🟡 não confirmado

Resultado

Cartão de crédito: 🟡 NÃO CONFIRMADO
..........................................................
6. The News API 
Link de acesso: https://www.thenewsapi.com/
...........................................
Limite gratuito
100 requisições/dia
Apenas 3 artigos por requisição
Portanto, no máximo teórico:
100 × 3
300 artigos/dia.
Resultado

100 requisições/dia → até 300 artigos/dia

A API confirma que cada acesso/pesquisa conta como uma requisição.
..........................
Cobertura brasileira: ✅ SIM

A The News API oferece acesso a notícias de diferentes países e fontes internacionais, incluindo conteúdo brasileiro.

Conclusão:
✅ Cobertura brasileira disponível.

Observação: para o projeto, eu classificaria a cobertura como compatível, mas a quantidade exata de fontes brasileiras deve ser validada diretamente na resposta da API quando fizermos o teste técnico.
...................................
The News API
Filtro por país: ✅ SIM

A documentação oficial da The News API informa que o endpoint de Top Stories permite:

filtrar as notícias por países específicos.

Também permite filtros por:

idioma;
categoria;
fonte;
data;
palavras-chave.

Portanto, é possível solicitar notícias de um país específico.

Resultado

🟢 Possui filtro de país.
............................................
Cidade/região: 🟡 PARCIAL

A The News API oferece filtros como:

país;
idioma;
categoria;
fonte;
palavras-chave;
data.

Para uma cidade específica, podemos procurar seu nome como termo.

Por exemplo:

search=Curitiba

Mas isso não garante que a notícia seja da cidade.

Resultado

🟡 Pesquisa textual, mas não geofiltro nativo de cidade.
...........................................................
Disponibilidade de imagem

🟢 SIM — muito bem documentada

A resposta possui explicitamente:

image_url

A documentação oficial define esse campo como a URL para a imagem do artigo.

Resultado

Imagem: 🟢 SIM
URL da imagem: 🟢 SIM
Campo: image_url
........................................................
Termos de uso

🟡 ATENÇÃO

Os termos de uso existem e estabelecem que o conteúdo disponibilizado no serviço pode envolver material de terceiros.

O usuário precisa possuir os direitos necessários para utilizar conteúdos protegidos.

Também existem restrições relacionadas ao uso do serviço e do conteúdo.

Os termos atuais disponíveis são de 8 de julho de 2021, portanto são relativamente antigos.

Resultado

Uso da API: 🟢 SIM
Uso acadêmico: 🟢 SIM
Conteúdo de terceiros: ⚠️ direitos autorais
Imagens: ⚠️ verificar direitos da fonte
Republicação integral: 🔴 não presumir autorização
Termos atualizados recentemente: 🔴 NÃO
..........................................................
Necessidade de cartão

🟢 NÃO

A própria documentação diz:

“No billing details required.”

E o FAQ informa que não são necessários dados de pagamento para utilizar o plano gratuito.

Resultado

Cartão de crédito: 🟢 NÃO
...........................................................
7. Currents API
Link de acesso: https://currentsapi.services/en
...........................................
Limite gratuito

A informação atual da própria Currents é:

250 requisições/dia
Até 20 resultados por requisição
Histórico recente de até 30 dias.
Resultado

250 requisições/dia → até 5.000 resultados/dia

Isso coloca a Currents acima de NewsData.io em número bruto de requisições, embora NewsData.io permita até 10 artigos por crédito no Free.
...............................................
Cobertura brasileira: ✅ SIM

A Currents API declara cobertura internacional em 70+ países e 20+ idiomas.

Portanto, o Brasil está dentro da cobertura internacional da plataforma.

Conclusão:
✅ Brasil coberto.

Observação: a documentação pública encontrada não apresenta, nesta consulta, um número específico de fontes brasileiras. Portanto, não vou inventar essa quantidade.
...................................
Filtro por país: ✅ SIM

Aqui a documentação é bastante clara.

A Currents API permite filtrar notícias por:

country
language
category
author
domain

A própria documentação de uso informa explicitamente:

Filter by country, language, category, author, and domain.

O endpoint de notícias mais recentes também permite filtrar por país.

Resultado

🟢 Possui filtro de país nativo.
............................................
Cidade/região: 🟡 PARCIAL

A Currents possui filtro por país e diversos filtros de conteúdo.

Entretanto, não encontramos na documentação atual uma funcionalidade equivalente ao sistema geográfico da APITube, como:

latitude
longitude
radius

ou um filtro nativo completo:

city=Curitiba

Podemos utilizar palavras-chave relacionadas à cidade.

Resultado

🟡 Não considerar como geofiltro completo.
................................................
Disponibilidade de imagem

🟢 SIM

A API retorna informações de mídia associadas às notícias, incluindo imagem quando disponível.

Resultado

Imagem: 🟢 SIM
Imagem associada ao artigo: 🟢 SIM

⚠️ Como ocorre com outras agregadoras, nem todo artigo necessariamente terá uma imagem válida.
...........................................................
Termos de uso

🟡 PERMITE USO, MAS COM RESTRIÇÕES

Os termos concedem uma licença para baixar e utilizar os dados enquanto o caso de uso estiver autorizado.

Entre as restrições estão:

não fazer engenharia reversa;
não remover avisos de copyright;
não criar um espelho dos dados “as-is” em outro servidor;
não registrar múltiplas API keys;
não contornar limites da conta.

Resultado

Uso em aplicação: 🟢 SIM
Uso acadêmico: 🟢 SIM
Redistribuição integral: 🔴 NÃO
Mirror dos dados: 🔴 NÃO
Remover copyright: 🔴 NÃO
Uso comercial: 🟡 verificar plano/licença
...........................................................
Necessidade de cartão

🟡 NÃO CONFIRMADO COM SEGURANÇA

A Currents oferece um plano gratuito, mas não encontrei, nas informações oficiais atuais consultadas, uma declaração suficientemente clara para afirmar categoricamente que o cadastro do Free Plan não solicita cartão.

Portanto:

Cartão: 🟡 não confirmado.

Não vou transformar a existência de um plano gratuito em “não exige cartão” sem confirmação explícita.

Resultado

Cartão de crédito: 🟡 NÃO CONFIRMADO
............................................................
8. World News API 
Link de acesso: https://worldnewsapi.com/
...........................................
Limite gratuito
50 pontos/dia
Não necessariamente 50 requisições.
Cada endpoint pode consumir uma quantidade diferente de pontos.
Limite de 1 requisição/segundo
1 requisição simultânea
1 mês de histórico no plano Free.
Resultado

50 pontos/dia

Não é correto transformar automaticamente os 50 pontos em 50 requisições, porque o custo em pontos depende do endpoint e dos parâmetros utilizados.
.........................
Cobertura brasileira: ✅ SIM — confirmada e quantificada

Essa é uma das APIs em que temos uma informação particularmente útil.

A World News API informa:

100 fontes de notícias monitoradas no Brasil;
aproximadamente 1.957 notícias adicionadas por dia;
código de país: br;
idioma: pt;
histórico de notícias brasileiras desde 1º de janeiro de 2022.

Conclusão:
✅ Cobertura brasileira forte e explicitamente documentada.
...................................
Filtro por país: ✅ SIM

A World News API possui cobertura de mais de 210 países e oferece recursos de pesquisa e filtragem geográfica.

É possível utilizar o país como critério de busca/filtragem.

O Brasil pode ser selecionado através do código correspondente.

Resultado

🟢 Possui filtro de país.
............................................
Cidade/região: 🟢 SIM

A World News API é uma das APIs que realmente se destaca nesse requisito.

O endpoint de pesquisa permite filtrar notícias por localização, além de:

texto;
data;
categoria;
idioma;
país;
entre outros.

A documentação descreve explicitamente a busca por notícias utilizando location.

A API também oferece busca semântica, o que ajuda a encontrar notícias relacionadas à localização mesmo quando a grafia exata não aparece no texto.

Resultado

🟢 Possui filtragem geográfica.

🏆 Excelente candidata.
...............................................
Disponibilidade de imagem

🟢 SIM

Os resultados de notícias podem incluir informações de imagem associadas aos artigos.

Resultado

Imagem: 🟢 SIM
Imagem associada à notícia: 🟢 SIM
..........................................................
Termos de uso

🟡 PERMITE USO EM APLICAÇÕES, MAS COM RESTRIÇÕES FORTES

A API permite utilizar os dados em:

websites;
aplicações;
projetos.

Porém, existem restrições importantes.

É proibido:

criar uma aplicação que simplesmente replique a experiência do próprio World News API;
revender os dados;
copiar/armazenar sistematicamente os dados;
fazer scraping da API;
continuar utilizando os dados após o encerramento do acesso;
redistribuir os dados como se fossem próprios.

Existe ainda uma regra de cache: com autorização escrita, dados solicitados pelo usuário podem ser armazenados por até 1 hora para melhorar desempenho.

Resultado

Uso em aplicação: 🟢 SIM
Uso acadêmico: 🟢 SIM
Uso comercial: 🟡 SIM, sujeito aos termos/plano
Revenda dos dados: 🔴 NÃO
Armazenamento permanente: 🔴 NÃO
Cache: 🟡 limitado
Criar concorrente: 🔴 NÃO
............................................................
Necessidade de cartão

🟢 NÃO

A documentação oficial informa que é possível obter uma API key gratuita e afirma expressamente:

“No credit card is required for the free tier.”

O Free Tier inclui os 50 pontos diários.

Resultado

Cartão de crédito: 🟢 NÃO
...........................................................
9. NewsCatcher 
Link de acesso:https://www.newscatcherapi.com/
...........................................
Plano Individual
US$ 0/mês
Pay-as-you-go/avaliação
A página atual apresenta buscas por eventos com:
profundidade de 2 semanas;
até 100 resultados por busca;
1 concorrência;
1 monitoramento ativo;
frequência diária.
Observação importante

A comparação atual também registra um nível de 6.000 créditos/mês associado ao Starter, que custa US$50/mês; portanto, não devemos confundir esse número com uma franquia gratuita mensal.

Resultado

Não há uma franquia gratuita simples comparável a “100 requisições/dia”.
....................................
Cobertura brasileira: ✅ SIM

A NewsCatcher trabalha com notícias globais e possui cobertura de diferentes tipos de fontes, incluindo:

veículos gerais;
veículos locais;
governo/municípios;
política;
finanças;
tecnologia;
esportes;
entre outras categorias.

A plataforma informa trabalhar com 75.000+ fontes globais, portanto o Brasil faz parte do universo de cobertura global.

Conclusão:
✅ Cobertura brasileira disponível.

Observação: não encontrei, nesta pesquisa, um número oficial atual de fontes exclusivamente brasileiras.
...................................
Filtro por país: ✅ SIM

A NewsCatcher possui filtros geográficos para restringir os resultados de notícias.

O sistema permite trabalhar com parâmetros relacionados ao país/localização da fonte.

Resultado

🟢 Possui filtragem geográfica por país.

Observação: a NewsCatcher tem um sistema de parâmetros mais complexo que uma API simples do tipo country=br, então é importante distinguir entre país do conteúdo e país da fonte ao implementar.
............................................
Cidade/região: 🟢 SIM — especialmente pela Local News API

Aqui a situação é muito interessante.

A NewsCatcher possui uma Local News API separada.

Ela permite pesquisar notícias utilizando:

cidades;
estados;
localização;
país;
fontes locais.

A documentação mostra, por exemplo, uma pesquisa contendo:

locations:
  - New York City, New York
  - Los Angeles, California

A API possui diferentes métodos para detectar a localização:

fonte dedicada à região;
seção local;
fonte regional;
formato “City, State”;
proximidade entre cidades/estados;
extração por IA.
Resultado

🟢 SIM — excelente para notícias locais.
............................................
Disponibilidade de imagem

🟢 SIM

A API disponibiliza informações de mídia associadas aos artigos, incluindo imagem.

Resultado

Imagem: 🟢 SIM
Imagem associada ao artigo: 🟢 SIM

⚠️ A existência da imagem depende da fonte original.
.......................................................
Termos de uso

🟡 USO CONTROLADO POR CONTRATO/PLANO

A NewsCatcher estabelece que seu serviço é uma plataforma de agregação de notícias baseada em assinatura e que o uso é regido pelo contrato de serviço aplicável.

Portanto, não é correto assumir que:

“Se consigo receber o artigo pela API, posso republicá-lo livremente.”

Os direitos do conteúdo original continuam sendo uma questão separada.

Resultado

Uso da API: 🟢 SIM
Uso acadêmico: 🟢 SIM
Uso comercial: 🟡 depende do contrato/plano
Republicação integral: ⚠️ verificar direitos
Imagens: ⚠️ direitos do publisher
Redistribuição: 🟡 depende da licença
.........................................................
Necessidade de cartão

🟢 NÃO

A NewsCatcher já informou explicitamente que seus planos gratuitos podem ser assinados sem cartão de crédito.

Além disso, a oferta atual do CatchAll da empresa também informa créditos gratuitos no cadastro e:

“no card required”.

Resultado

Cartão de crédito: 🟢 NÃO
............................................................
10. Perigon 
Link: https://perigon.io/
...........................................
Limite gratuito

O plano Free atual possui:

150 requisições/mês

A documentação contratual da Perigon especifica que o plano gratuito é destinado a uso individual/pessoal, avaliação e testes, e não a uso comercial ou produção contínua.

Resultado

150 requisições/mês

É um limite baixo para uma aplicação que consulta notícias frequentemente.
.......................................
Cobertura brasileira: ⚠️ SIM, mas quantidade não confirmada

O Perigon é uma plataforma de dados de notícias global, com cobertura internacional.

Entretanto, para este ponto, não encontrei uma página oficial atual que me permita afirmar com segurança quantas fontes brasileiras estão disponíveis.

Portanto:

Brasil: ✅ cobertura internacional compatível
Quantidade de fontes brasileiras: ❓ não confirmada

Conclusão:
🟡 Deve permanecer na pesquisa, mas sem atribuir uma quantidade de fontes brasileiras sem comprovação.
...................................
Filtro por país: ✅ SIM — muito forte

O Perigon é particularmente interessante neste ponto.

A documentação publicada em agosto de 2026 explica explicitamente a utilização de:

country
sourceCountry
city
sourceCity

O próprio Perigon diferencia:

country
→ país associado à notícia.

sourceCountry
→ país onde está o publisher/fonte.

Isso é extremamente útil para nosso projeto.

Por exemplo, podemos conceitualmente procurar:

country=br

ou:

sourceCountry=br

dependendo do que queremos encontrar.

Resultado

🟢 Possui filtro de país avançado.

É uma das melhores APIs neste quesito.
............................................
Cidade/região: 🟢 SIM — EXCELENTE

O Perigon possui filtros geográficos avançados.

A documentação atual diferencia:

country
sourceCountry
city
sourceCity

Ou seja:

city

Pode representar a cidade relacionada à notícia.

sourceCity

Representa a cidade associada à fonte/publicador.

Isso permite diferenciar:

notícia sobre Curitiba

de:

notícia publicada por um veículo localizado em Curitiba.

A própria documentação publicada em agosto de 2026 apresenta explicitamente esse modelo de filtragem local.

Resultado

🟢 SIM — excelente.

🏆 Uma das APIs mais completas para localização.
...........................................................
Disponibilidade de imagem

🟢 SIM

O modelo de dados oficial possui explicitamente:

imageUrl

A documentação afirma que Article image faz parte dos dados padrão disponíveis nos planos da API.

Inclusive o plano gratuito possui acesso à plataforma, com 150 requisições/mês e sem necessidade de cartão.

Resultado

Imagem: 🟢 SIM
URL da imagem: 🟢 SIM
Campo: imageUrl
Disponível no Free: 🟢 SIM
...........................................................
Termos de uso

🟡 RESTRIÇÕES IMPORTANTES SOBRE O CONTEÚDO

A Perigon possui termos específicos e um Cloud Service Agreement aplicável às APIs e aos produtos de dados.

A documentação sobre artigos é particularmente clara:

imagens pertencem ao respectivo proprietário/publisher;
thumbnails ou previews pequenos podem ser aceitáveis quando acompanhados de citação e link para a fonte;
o conteúdo completo do artigo geralmente é protegido;
reprodução/distribuição não autorizada do artigo completo é proibida.

Resultado

Uso da API: 🟢 SIM
Uso acadêmico: 🟢 SIM
Thumbnail: 🟢 opção recomendada
Imagem original: 🔴 não presumir autorização
Artigo completo: 🔴 não
Link para fonte: 🟢 recomendado
Direitos autorais: ⚠️ muito importante
............................................................
Necessidade de cartão

🟢 NÃO

A página oficial de preços informa:

“No credit card needed”

para o plano Free.

O Free atualmente oferece 150 requisições/mês.

A própria página da API também confirma que é possível criar uma conta gratuita e obter a API key sem cartão.

Resultado

Cartão de crédito: 🟢 NÃO

⚠️ Lembrete: o Free possui licença pessoal/educacional, não comercial.
..........................................................
11. The Guardian Open Platform API
Link de acesso: https://open-platform.theguardian.com/
...........................................
O The Guardian Open Platform é uma categoria diferente porque fornece conteúdo do próprio Guardian, não um agregador de toda a imprensa.

Para este ponto, o limite gratuito depende do nível de acesso/API key e das condições vigentes do Open Platform. Portanto, não vou transformar uma informação antiga de terceiros em um número atual sem confirmação oficial.

Resultado

Limite: verificar conforme a chave/plano atual do Open Platform.
Cobertura brasileira: ⚠️ PARCIAL

Aqui existe uma diferença importante.

O Guardian é um veículo de comunicação próprio, portanto sua API não representa um agregador de notícias brasileiras.

O Guardian publica matérias sobre o Brasil — inclusive há cobertura brasileira atual no site —, mas isso não significa que a API forneça uma ampla seleção de veículos brasileiros.

Conclusão:
🟡 Possui conteúdo sobre Brasil, mas não é uma boa solução para representar a imprensa brasileira como um todo.
...................................
Filtro por país: ❌ NÃO como filtro de país da mesma maneira

Aqui temos que fazer uma distinção.

A API do Guardian permite pesquisar o conteúdo do próprio Guardian usando vários campos e parâmetros, mas ela não funciona como um agregador mundial com um parâmetro country=br equivalente ao NewsData/GNews.

Você pode pesquisar por:

palavras;
seção;
data;
tags;
campos;
etc.

Mas isso não transforma a API em uma API de notícias por país.

Por exemplo, pesquisar:

Brazil

pode retornar matérias sobre o Brasil, mas isso é pesquisa de conteúdo, e não necessariamente:

country=br
Resultado

🟡 Não possui um filtro de país equivalente ao das News APIs agregadoras.
............................................
Cidade/região: 🟡 PARCIAL

A API do Guardian permite pesquisar o conteúdo do Guardian e utilizar:

palavras-chave;
tags;
seções;
campos;
datas;
etc.

Podemos procurar:

q=Curitiba

ou utilizar tags relacionadas a lugares quando disponíveis.

Mas isso não equivale a uma News API geográfica que diga:

city=Curitiba
radius=50km
Resultado

🟡 Pesquisa por local/conteúdo, mas não geofiltro completo.
............................................
Disponibilidade de imagem

🟢 SIM

O serviço Developer inclui explicitamente:

thumbnail images

ou seja, imagens em miniatura associadas ao conteúdo.

Resultado

Imagem: 🟢 SIM
Thumbnail: 🟢 SIM
Conteúdo: 🟢 SIM

⚠️ É importante lembrar que são imagens relacionadas ao conteúdo do The Guardian, não um banco de imagens de notícias de várias fontes.
..........................................................
Termos de uso

🟡 PERMITE ACESSO, MAS O CONTEÚDO CONTINUA SENDO DO GUARDIAN

É uma situação diferente das APIs agregadoras.

A API fornece conteúdo publicado pelo próprio The Guardian.

A documentação permite inclusive filtrar conteúdo por rights, incluindo conteúdo classificado como syndicatable, o que é importante para usos que envolvem redistribuição.

Portanto:

API: 🟢 SIM
Conteúdo: ⚠️ sujeito aos direitos do Guardian
Redistribuição: 🟡 depende dos direitos/licença
Imagem: 🟡 depende dos direitos aplicáveis
Uso acadêmico: 🟢 SIM
..........................................................
Necessidade de cartão

🟢 NÃO

A API possui uma chave de teste (api-key=test) e permite registro para obtenção de uma API key. A documentação demonstra inclusive chamadas utilizando a chave test.

Não há indicação de cartão de crédito como requisito para o acesso Developer.

Resultado

Cartão de crédito: 🟢 NÃO
...........................................................
12. Google News RSS
Link de acesso:https://rss.app/rss-feed/google-news-rss-feed
...........................................
Google News RSS não é uma API REST tradicional com uma franquia oficial de “X requisições por dia”.

Não existe um limite público oficial equivalente a:

“Você pode fazer exatamente 1.000 requisições/dia.”

Discussões da própria comunidade do Google indicam que o RSS não possui uma quota formal publicada como uma API tradicional e que uso excessivo pode resultar em bloqueio.

Resultado

Limite oficial de requisições/dia: não publicado.
...................................
Cobertura brasileira: ✅ SIM — muito forte

O Google News possui uma edição específica para o Brasil.

Além disso, existe documentação e pesquisa acadêmica sobre o Google News Brasil, demonstrando que a edição brasileira agrega fontes e notícias relacionadas ao país. Um estudo sobre a edição brasileira observou inclusive a presença de fontes brasileiras e de notícias sobre o Brasil.

A grande vantagem é que o Google News funciona como agregador de muitas fontes, em vez de depender de um único veículo.

Conclusão:
✅ Excelente cobertura brasileira.

Problema: não é uma API REST tradicional e a utilização via RSS possui limitações diferentes das News APIs.✅ SIM — muito forte
...................................
Filtro por país: 🟢 SIM, mas de forma diferente

O Google News não possui uma API REST oficial tradicional. Portanto, não temos um parâmetro REST universal do tipo:

country=br

Entretanto, o Google News possui edições/feeds regionais e o RSS pode ser construído utilizando parâmetros relacionados à localização/idioma.

Portanto, é possível direcionar o feed para determinado mercado/país.

Resultado

🟢 É possível direcionar por país/mercado, mas:

⚠️ não funciona como uma API REST tradicional.
...................................
Google News RSS
Cidade/região: 🟢/🟡 SIM, mas via pesquisa/localização

O Google News consegue fornecer resultados muito específicos para cidades e regiões através das consultas e edições locais.

Por exemplo, podemos construir uma consulta para:

Curitiba

e obter notícias relacionadas à cidade.

Também é possível utilizar a edição/mercado brasileiro.

Porém, assim como no caso do GNews, isso não deve ser confundido com uma API REST que ofereça formalmente:

latitude
longitude
radius
Resultado

🟡 Boa capacidade de pesquisa local, mas não é um geofiltro REST tradicional.
............................................
Disponibilidade de imagem

🟡 PARCIAL / NÃO GARANTIDA

O RSS do Google News não funciona como uma API REST tradicional com um campo padronizado image_url.

Dependendo do feed e da forma como o RSS é obtido/processado, podem existir elementos relacionados a mídia/imagem, mas não devemos considerar que todo item RSS possui uma URL de imagem confiável e padronizada.

Resultado

Imagem: 🟡 PARCIAL
Campo image_url padronizado: 🔴 NÃO
Garantia para todos os artigos: 🔴 NÃO

Para um projeto que precisa montar cards de notícias com imagens, isso é uma desvantagem.
.........................................................
Termos de uso

🔴 NÃO CONSIDERARIA SEGURO PARA O MVP SEM UMA ANÁLISE JURÍDICA/DE LICENÇA

O problema é que Google News RSS não funciona como uma API licenciada de conteúdo jornalístico no mesmo sentido de uma API comercial.

Não encontramos uma política oficial atual que diga simplesmente:

“Você pode republicar livremente o conteúdo dos feeds RSS do Google News.”

Há inclusive discussões no próprio fórum do Google sobre o uso de Google News RSS em sites e aplicações, justamente porque a questão de autorização não é simples.

Resultado

RSS: 🟢 tecnicamente disponível
Uso em aplicação: 🟡
Republicação de conteúdo: ⚠️
Imagens: 🔴 não presumir autorização
Uso comercial: ⚠️
Segurança jurídica para MVP: 🔴 baixa
...................................................
Necessidade de cartão

🟢 NÃO

Aqui existe uma diferença fundamental:

Google News RSS não possui um sistema convencional de:

“cadastre-se → escolha plano → informe cartão → receba API key”.

O RSS pode ser acessado diretamente, sem cadastro de cartão.

Resultado

Cartão de crédito: 🟢 NÃO

⚠️ Porém, isso não significa que seja a melhor solução para o projeto, porque o RSS possui outras limitações que já analisamos.
...........................................................
13. Bing News Search API
Link de acesso: https://www.searchapi.io/docs/bing-news
...........................................
O Bing News Search utiliza o sistema de transações do recurso.

Uma resposta de funcionário da Microsoft documenta que o nível gratuito tinha limite de aproximadamente:

1.000 transações/mês

e que cada chamada à API conta como uma transação.

Resultado

até 1.000 transações/mês no nível gratuito documentado.

Isso equivale aproximadamente a:

33 requisições/dia, se distribuído uniformemente. (Dados de 2023)
Cobertura brasileira: ✅ SIM

O Bing News Search faz parte do sistema de pesquisa de notícias da Microsoft e trabalha com mercados/idiomas diferentes.

O Brasil pode ser atendido através dos parâmetros de mercado/localização apropriados.

Conclusão:
✅ Cobertura brasileira disponível.
...................................
Filtro por país: ✅ SIM

A API do Bing News possui parâmetros específicos de mercado/localização.

A Microsoft documenta:

mkt

como o mercado dos resultados.

O formato é:

<idioma>-<país>

Por exemplo:

pt-BR

ou outros mercados suportados.

Também existe o parâmetro:

cc

que pode ser usado para especificar o código do país dos resultados. A própria documentação da Microsoft explica que cc é utilizado para retornar resultados específicos de uma localização, enquanto mkt define o mercado.

Resultado

🟢 Possui filtro de país/mercado.
.....................................
Cidade/região: 🟡 PARCIAL

O Bing News Search permite:

mercado;
país;
idioma;
pesquisa textual.

Portanto podemos pesquisar:

q=Curitiba

e definir o mercado:

mkt=pt-BR

Mas o mkt=pt-BR significa mercado brasileiro, não:

cidade de Curitiba.

Não encontramos um parâmetro nativo equivalente a:

city=Curitiba
radius=50
Resultado

🟡 Pesquisa textual por cidade, mas não geofiltro completo.
............................................................
Disponibilidade de imagem

🟢 SIM — muito boa

A API possui um objeto image.

Por padrão, ele pode fornecer:

thumbnail.contentUrl

Além disso, utilizando:

originalImg=true

a resposta pode incluir:

image.contentUrl

com a URL da imagem original do publisher.

Resultado

Imagem: 🟢 SIM
Thumbnail: 🟢 SIM
Imagem original: 🟢 SIM
URL: 🟢 SIM
Parâmetro: originalImg=true
...........................................................
Termos de uso

🟢 POSSUI TERMOS OFICIAIS ESPECÍFICOS DA MICROSOFT

A Microsoft possui termos específicos para as Bing Search APIs, incluindo regras aplicáveis ao uso da API e do conteúdo retornado. Os termos atualmente publicados foram atualizados em fevereiro de 2025.

Porém, novamente:

⚠️ a autorização para utilizar o serviço não transfere automaticamente os direitos autorais das notícias/imagens para o desenvolvedor.

Resultado

API: 🟢 SIM
Termos oficiais: 🟢 SIM
Uso em aplicação: 🟢 SIM, conforme os termos
Conteúdo jornalístico: ⚠️ direitos de terceiros
Imagem: ⚠️ verificar direitos
Uso comercial: 🟡 depende dos termos/tier
...........................................................
Necessidade de cartão

🔴/🟡 ATENÇÃO — NÃO TRATAR COMO UMA API GRATUITA SIMPLES

O Bing News Search tradicional utiliza infraestrutura de assinatura/chave da Microsoft/Azure. A documentação da Microsoft mostra que a API requer uma subscription key.

Historicamente existia um Free Tier de 1.000 transações/mês, mas a forma de provisionamento e cobrança do ecossistema Bing/Azure mudou.

A Microsoft também documenta que recursos Azure podem consumir créditos de uma conta Azure Free, e serviços que não sejam gratuitos podem gerar cobrança.

Portanto, não considero seguro classificar hoje o Bing News Search como “não exige cartão e possui uma franquia gratuita simples” sem qualificar o mecanismo de provisionamento atual.

Resultado

Cartão de crédito: 🟡 DEPENDE DA FORMA DE PROVISIONAMENTO/AZURE

⚠️ Para o nosso projeto, é uma desvantagem em relação a GNews/APITube/NewsAPI.
............................................................
14. FreeNewsAPI.io
Link de acesso: https://freenewsapi.ai/
...........................................
Limite gratuito

5.000 requisições/dia

Além disso:

requer API key;
possui cadastro;
o plano gratuito permite uso comercial, segundo a comparação publicada pelo próprio serviço;
fornece conteúdo de artigos.
Resultado

5.000 requisições/dia
...................................
Cobertura brasileira: ✅ SIM — muito bem documentada

Esta é uma das opções que mais se destacam neste ponto.

A FreeNewsAPI possui uma página específica:

News from Brazil

Atualmente ela informa aproximadamente:

571.062 artigos indexados na fatia brasileira;
218+ publishers/fontes brasileiras catalogadas;
g1.globo.com como um dos maiores publishers;
forte predominância de português;
aproximadamente 14.063 artigos/dia na média dos últimos dias analisados.

Ela também possui fontes brasileiras individuais disponíveis através de API aberta; por exemplo, o The Rio Times aparece como fonte brasileira em inglês.

Conclusão:
🏆 Cobertura brasileira excelente e quantitativamente demonstrada.
...................................
Filtro por país: ✅ SIM

A FreeNewsAPI possui páginas e datasets organizados por país.

Por exemplo, existe uma seção específica para:

Brazil

e o serviço permite consultar notícias associadas ao país.

Portanto, o país pode ser utilizado como critério de filtragem.

Resultado

🟢 Possui filtro de país.
..............................
Cidade/região: 🟡 PARCIAL

A FreeNewsAPI oferece filtros estruturados por:

país;
idioma;
categoria;
publisher;
autor;
datas;
pesquisa textual.

A documentação atual mostra filtros de país e outros metadados, mas não apresenta um filtro geográfico nativo de cidade/raio/latitude/longitude equivalente ao APITube.

Podemos pesquisar:

q=Curitiba

mas isso continua sendo pesquisa textual.

Resultado

🟡 Não possui geofiltro de cidade confirmado.
..............................................
Disponibilidade de imagem

🟢 SIM

A API retorna informações estruturadas dos artigos e disponibiliza imagem associada quando presente na fonte.

Resultado

Imagem: 🟢 SIM
Imagem associada ao artigo: 🟢 SIM

⚠️ A disponibilidade pode depender da fonte original.
...........................................................
Termos de uso

🟡 PONTO QUE PRECISA DE ATENÇÃO

O próprio serviço atualmente se apresenta como uma API gratuita de notícias e informa uso comercial no comparativo de serviços publicado em seu site.

Porém, para o projeto, eu não trataria isso como autorização automática para republicar integralmente texto e imagens de terceiros.

Resultado

Uso da API: 🟢 SIM
Uso comercial da API: 🟢 SIM, segundo o próprio serviço
Texto integral de terceiros: ⚠️ direitos autorais
Imagens: ⚠️ direitos autorais
Republicação: 🟡 verificar licença da fonte
Termos detalhados: 🟡 menos claros que APIs com contratos mais desenvolvidos
...........................................................
Necessidade de cartão

🟡 NÃO CONFIRMADO DE FORMA SUFICIENTE

A API possui plano gratuito e exige cadastro/API key, mas não encontrei uma declaração oficial suficientemente clara para afirmar categoricamente:

“No credit card required.”

Portanto, não vou marcar como 🟢 sem confirmação.

Resultado

Cartão de crédito: 🟡 NÃO CONFIRMADO
............................................................
15. Scrapingdog
Link de acesso:https://www.scrapingdog.com/
............................................

O Scrapingdog é diferente das anteriores.

Ele é uma plataforma de web scraping, não uma News API tradicional.

Portanto, seu limite deve ser analisado como:

requisições de scraping, e não como “requisições de notícias”.

Por isso, não considero correto colocá-lo no mesmo ranking das News APIs.

Resultado

Não classificar como News API no ranking principal.
..................................
Cobertura brasileira: 🟡 DEPENDE DO SITE SCRAPEADO

O Scrapingdog não é um agregador de notícias com uma base própria de fontes brasileiras.

Ele é uma plataforma de web scraping.

Portanto, você pode utilizá-lo para extrair:

G1;
UOL;
Folha;
Estadão;
CNN Brasil;
etc.,

desde que o site seja tecnicamente acessível e o uso esteja de acordo com seus termos/leis.

Conclusão:
🟡 Pode obter notícias brasileiras, mas não possui uma cobertura brasileira própria comparável a uma News API.
...................................
Filtro por país: 🟡 DEPENDE DA FONTE

Aqui novamente precisamos separar as categorias.

O Scrapingdog é uma ferramenta de web scraping.

Ela não possui uma grande base própria de notícias onde você simplesmente faça:

country=br

para receber notícias brasileiras.

Porém, você pode:

escolher um site brasileiro;
fazer scraping desse site;
obter notícias brasileiras.

Ou utilizar recursos de localização/proxy quando aplicável.

Resultado

🟡 Não possui um filtro de país próprio comparável às News APIs.

A seleção do país depende principalmente do site que está sendo raspado.
....................
Cidade/região: 🟡 DEPENDE DA FONTE

O Scrapingdog não possui uma base própria de notícias com geolocalização.

Você escolhe o site que deseja coletar.

Por exemplo:

https://site-brasileiro.com/noticias/curitiba

e faz scraping daquela página.

Portanto, a capacidade de encontrar notícias de Curitiba dependerá de como o próprio site organiza seu conteúdo.

Resultado

🟡 Possível, mas não é um filtro geográfico da API.
...................................................
Disponibilidade de imagem

🟢 SIM

Aqui existe uma diferença importante em relação às News APIs tradicionais.

O Scrapingdog pode extrair imagens diretamente das páginas/fontes que estão sendo processadas.

No caso específico de Google News, a própria plataforma informa que seu News API retorna:

headline;
source;
snippet;
thumbnail;
horário de publicação.

Portanto:

Imagem: 🟢 SIM
Thumbnail: 🟢 SIM

⚠️ Continua sendo uma plataforma de scraping, e não uma base jornalística própria.
..........................................................

Termos de uso

🟢 LICENÇA DE USO DOS DADOS RETORNADOS, COM CONDIÇÕES

Os termos atuais, revisados em 13 de janeiro de 2026, concedem aos assinantes uma licença mundial e não exclusiva para:

acessar;
armazenar;
exibir;
analisar;
redistribuir

os dados públicos retornados pela API, inclusive comercialmente.

Porém, existem responsabilidades importantes:

os dados vêm de sites públicos de terceiros;
Scrapingdog não reivindica propriedade sobre as informações subjacentes;
o usuário continua responsável por respeitar leis e direitos aplicáveis;
não deve utilizar dados para divulgar informações pessoais/sensíveis de forma indevida.

Resultado

Uso comercial: 🟢 SIM
Redistribuição: 🟢 SIM, dentro da licença
Armazenamento: 🟢 SIM
Dados públicos: 🟢 SIM
Direitos dos sites originais: ⚠️ continuam relevantes
É uma News API tradicional: 🔴 NÃO
.............................................
Necessidade de cartão

🟢 NÃO

A página oficial de preços é extremamente clara:

200 créditos gratuitos;
US$0;
acesso às APIs;
No Credit Card Required.

Resultado

Cartão de crédito: 🟢 NÃO
16. Webz.io

O Webz.io também é uma plataforma de web data/news data, e não simplesmente uma API de notícias gratuita convencional.

A documentação comercial encontrada apresenta, por exemplo, uma avaliação gratuita com:

1.000 requisições durante 10 dias

para determinados acessos de avaliação.

Resultado

1.000 requisições no trial, não uma franquia gratuita diária permanente.
.................................
Cobertura brasileira: ✅ SIM

O Webz.io é uma plataforma de dados da web/news data com cobertura internacional. O serviço coleta conteúdo de múltiplos países e idiomas.

Conclusão:
✅ Brasil faz parte da cobertura internacional.

Observação: não encontrei uma estatística oficial atual suficientemente específica para afirmar quantas fontes brasileiras estão disponíveis.

Filtro por país: ✅ SIM

O Webz.io é uma plataforma de dados da web com metadados geográficos e filtros de conteúdo.

É possível trabalhar com informações de localização/país na filtragem dos dados.

Resultado

🟢 Possui filtragem geográfica.

Entretanto, é uma plataforma mais complexa do que uma News API simples.
........................................................
Cidade/região: 🟢 SIM — especialmente para conteúdo local

O Webz.io possui classificação de fontes por tipo, incluindo:

trust.source.type:local_news

para sites relacionados a áreas geográficas específicas, incluindo:

cidades;
regiões;
condados.

A própria documentação informa que os metadados da fonte podem incluir:

cidade;
condado;
estado;
domínio.

Portanto, ele consegue trabalhar com conteúdo de fontes locais/regionalizadas.

Resultado

🟢 SIM, com capacidade geográfica relevante.
...........................................................
Disponibilidade de imagem

🟢 SIM — muito forte

O modelo de dados do Webz.io possui:

external_images

internal_images

e também:

main_image

A documentação define esses campos como URLs de imagens.

A plataforma, portanto, consegue retornar imagens associadas aos posts/artigos.

Resultado

Imagem: 🟢 SIM
Imagens externas: 🟢 SIM
Imagens internas: 🟢 SIM
Imagem principal: 🟢 SIM
.........................................................
Termos de uso

🔴 BASTANTE RESTRITIVO

Os termos atuais do Webz.io estabelecem uma licença limitada, não transferível e não sublicenciável para utilização dos dados dentro do propósito autorizado.

Há restrições especialmente importantes:

não transferir os dados para terceiros;
não sublicenciar;
não compartilhar livremente os dados;
não comercializar os Data Derivatives;
não utilizar os dados para competir com o Webz.io;
não utilizar determinados conteúdos para treinamento de IA quando o campo AI_allow indicar que isso é proibido.

O Webz.io permite apenas determinados usos para apresentação de resultados aos usuários, desde que as condições estabelecidas sejam cumpridas.

Para a versão Free News API Lite, também existem termos específicos e restrições próprias.

Resultado

Uso interno: 🟢 SIM
Uso em aplicação: 🟡 SIM, conforme finalidade
Redistribuição livre: 🔴 NÃO
Revenda dos dados: 🔴 NÃO
Compartilhamento com terceiros: 🔴 RESTRITO
Treinamento de IA: 🟡 depende do campo/licença
Uso comercial: 🟡 depende do contrato/plano
.............................................
Necessidade de cartão

🟡 NÃO CONFIRMADO COM SEGURANÇA PARA O TRIAL ATUAL

O Webz.io possui modalidades gratuitas/trial, mas não encontrei confirmação oficial atual suficientemente clara para classificar o cadastro do trial como definitivamente “sem cartão”.

Resultado

Cartão de crédito: 🟡 NÃO CONFIRMADO
...........................................................
17. API de veiculos oficiais

API Principal: APITUBE

API ALTERNATIVA: NewsData.io
.......................................
API de texto

1. Google Gemini API

Link de acesso: https://ai.google.dev/gemini-api/docs?hl=pt-br
....................................................

Limite gratuito

🟢 SIM — possui Free Tier

A API Gemini possui modelos com utilização gratuita, com limites que variam conforme o modelo.

Os limites são controlados principalmente por:

requisições por minuto;
tokens por minuto;
requisições por dia;
tokens utilizados.

⚠️ O limite não é simplesmente "X requisições por dia" para toda a plataforma. Ele depende do modelo utilizado.

Resultado

🟢 Possui camada gratuita oficial.

....................................................

Geração de resumo

🟢 SIM — excelente

O Gemini é um LLM generativo e pode receber uma notícia e produzir:

resumo;
resumo curto;
resumo detalhado;
tópicos principais;
título;
palavras-chave;
classificação;
tradução;
reescrita.

Para o projeto, podemos enviar o texto da notícia e solicitar, por exemplo:

Resuma a notícia abaixo em no máximo 5 linhas.

Resultado

🟢 Geração de resumo: SIM

....................................................

Tamanho máximo de entrada

🟢 SIM — suporta entradas grandes

O limite depende do modelo Gemini escolhido.

Os modelos atuais possuem janelas de contexto muito grandes, chegando a centenas de milhares ou milhões de tokens em determinados modelos.

⚠️ Portanto, não devemos colocar um número único para "Gemini API"; o limite deve ser associado ao modelo escolhido.

Resultado

🟢 Adequado para notícias individuais e também para grandes conjuntos de texto.

....................................................

Necessidade de cartão

🟢 NÃO para começar no Free Tier

É possível começar utilizando a API gratuita.

⚠️ A situação muda caso seja necessário habilitar faturamento para utilizar recursos pagos ou exceder determinados limites.

Resultado

🟢 Cartão para começar no Free Tier: não necessário

....................................................

Política de uso

🟡 ATENÇÃO

O uso da API está sujeito aos termos e políticas do Google.

É necessário respeitar:

políticas de uso;
direitos autorais;
conteúdo de terceiros;
políticas específicas dos modelos;
regras de tratamento de dados.

Além disso, há diferenças entre utilização gratuita e utilização faturada.

Resultado

Uso acadêmico: 🟢
Geração de resumo: 🟢
Conteúdo de terceiros: ⚠️ respeitar direitos
Uso indevido: 🔴 proibido

....................................................

Resultado

🟢 EXCELENTE CANDIDATA

A Gemini é uma das opções mais completas para o projeto.

2. Groq API

Link de acesso:https://console.groq.com/docs/api-reference#chat-create

....................................................

Limite gratuito

🟢 SIM

A Groq possui Free Plan com limites específicos por modelo.

Os limites incluem:

RPM — requisições por minuto;
RPD — requisições por dia;
TPM — tokens por minuto;
TPD — tokens por dia.

Por exemplo, a documentação atual mostra modelos como openai/gpt-oss-120b com:

30 RPM;
1.000 RPD;
8.000 TPM;
200.000 TPD.

Resultado

🟢 Free Tier: SIM
🟢 Alguns modelos: até 1.000 requisições/dia

....................................................

Geração de resumo

🟢 SIM

A Groq disponibiliza LLMs capazes de:

resumir;
explicar;
classificar;
transformar;
gerar texto.

É perfeitamente adequada para receber o conteúdo de uma notícia e devolver um resumo.

Resultado

🟢 Resumo: SIM

....................................................

Tamanho máximo de entrada

🟢 SIM — depende do modelo

Cada modelo possui seu próprio limite de contexto.

Portanto, o tamanho máximo deve ser consultado para o modelo selecionado.

Para o nosso caso, mesmo modelos com contexto relativamente menor são suficientes para uma notícia individual.

Resultado

🟢 Adequado para notícias.

....................................................

Necessidade de cartão

🟢 NÃO para utilização do Free Plan

A API pode ser utilizada dentro dos limites gratuitos sem transformar o projeto imediatamente em uma utilização paga.

Resultado

🟢 Cartão: não necessário para começar no Free Plan

....................................................

Política de uso

🟡 ATENÇÃO

É necessário obedecer aos termos da Groq e às políticas dos modelos utilizados.

Além disso, alguns modelos possuem suas próprias condições.

Resultado

Uso acadêmico: 🟢
Resumo: 🟢
Conteúdo de terceiros: ⚠️
Uso abusivo: 🔴

....................................................

Resultado

🟢 EXCELENTE CANDIDATA

Principal vantagem: velocidade extremamente alta.

3. OpenRouter

Link de acesso:https://openrouter.ai/

....................................................

Limite gratuito

🟢 SIM

O OpenRouter possui modelos gratuitos.

Atualmente existe o:

openrouter/free

que seleciona automaticamente modelos gratuitos disponíveis.

A página de preços atual apresenta:

mais de 25 modelos gratuitos;
4 provedores gratuitos;
50 requisições/dia no plano Free.

Resultado

🟢 Free: SIM
🟢 Limite indicado: 50 requisições/dia

....................................................

Geração de resumo

🟢 SIM

Como o OpenRouter fornece acesso a LLMs generativos, podemos selecionar um modelo e solicitar:

resumo;
classificação;
tradução;
análise;
geração de texto.

O openrouter/free também aceita texto e, em modelos compatíveis, imagens.

Resultado

🟢 Resumo: SIM

....................................................

Tamanho máximo de entrada

🟢 EXCELENTE

O openrouter/free atualmente possui:

200.000 tokens de contexto.

Isso é muito mais do que o necessário para uma notícia normal.

Resultado

🟢 Entrada: até 200.000 tokens no Free Models Router

....................................................

Necessidade de cartão

🟢 NÃO para utilização dos modelos gratuitos

O plano Free possui modelos com preço zero.

⚠️ Se o usuário escolher modelos pagos ou utilizar créditos pagos, passa a existir faturamento.

Resultado

🟢 Cartão para utilizar modelos Free: não necessário

....................................................

Política de uso

🟡 ATENÇÃO

O OpenRouter funciona como intermediário entre diferentes provedores.

Consequentemente, além das políticas do OpenRouter, podem existir políticas específicas dos modelos/provedores.

Resultado

Uso acadêmico: 🟢
Resumo: 🟢
Modelos gratuitos: 🟢
Políticas específicas: ⚠️

....................................................

Resultado

🟢 EXCELENTE CANDIDATA

Grande vantagem: se um modelo gratuito deixar de funcionar, podemos trocar de modelo sem necessariamente trocar a arquitetura da API.

4. Cerebras Inference API

Link de acesso: https://www.cerebras.ai/

....................................................

Limite gratuito

🟡 TRIAL

A Cerebras atualmente oferece:

US$ 5 em créditos gratuitos

após a criação da conta.

O Free Trial permite acesso aos modelos da Cerebras.

⚠️ Não é a mesma coisa que uma franquia gratuita permanente.

Resultado

🟡 Free Trial: US$5

....................................................

Geração de resumo

🟢 SIM

Os modelos disponibilizados pela Cerebras são LLMs generativos.

Podem realizar:

resumo;
geração;
análise;
transformação de texto.

Resultado

🟢 Resumo: SIM

....................................................

Tamanho máximo de entrada

🟢 DEPENDE DO MODELO

Cada modelo possui sua própria janela de contexto.

Resultado

🟢 Adequado para notícias.

....................................................

Necessidade de cartão

🟢 não para obter o Free Trial

A Cerebras oferece o trial de US$5 para novos usuários.

....................................................

Política de uso

🟡

É necessário respeitar os termos da Cerebras e as políticas dos modelos.

Além disso, a própria empresa diferencia modelos destinados à avaliação daqueles destinados à produção.

Resultado

Uso acadêmico: 🟢
Produção com Free Trial: 🟡
Uso indevido: 🔴

....................................................

Resultado

🟡 Boa candidata para testes, mas não é a melhor para depender de gratuidade permanente.

5. Mistral AI API

Link de acesso:https://mistral.ai/

....................................................

Limite gratuito

🟡 DEPENDE DA MODALIDADE

A Mistral oferece acesso à API e diferentes níveis de utilização.

É necessário diferenciar:

experimentação;
API gratuita;
créditos;
uso pago.

Não devemos tratar toda a API Mistral como ilimitadamente gratuita.

Resultado

🟡 Gratuidade: depende do nível/modelo

....................................................

Geração de resumo

🟢 SIM

Os modelos Mistral são adequados para:

resumo;
classificação;
geração de texto;
análise;
transformação.

....................................................

Tamanho máximo de entrada

🟢 DEPENDE DO MODELO

Modelos Mistral diferentes possuem diferentes context windows.

....................................................

Necessidade de cartão

🟡 DEPENDE DO tipo de acesso

Para experimentação pode haver acesso sem faturamento, mas não devemos assumir que todo nível de API é permanentemente gratuito.

....................................................

Política de uso

🟡

Aplicam-se os termos da Mistral e as licenças/condições específicas dos modelos.

....................................................

Resultado

🟡 Boa candidata, mas menos simples para nosso requisito de Free Tier previsível.

6. Cohere API

Link de acesso: https://cohere.com/models-overview

....................................................

Limite gratuito

🟢 SIM — Trial API Key

A Cohere informa que cada conta recebe uma Trial API Key.

Chamadas feitas utilizando a Trial Key são gratuitas, mas possuem limitações de taxa.

Resultado

🟢 API de avaliação gratuita.

....................................................

Geração de resumo

🟢 SIM

A Cohere fornece modelos generativos adequados para:

resumo;
geração;
análise;
classificação;
NLP.

....................................................

Tamanho máximo de entrada

🟢 DEPENDE DO MODELO

Cada modelo possui sua própria janela de contexto.

....................................................

Necessidade de cartão

🟢 NÃO para Trial API Key

A conta recebe uma Trial API Key automaticamente.

....................................................

Política de uso

🔴 RESTRIÇÃO IMPORTANTE

A própria Cohere afirma que as Trial API Keys:

são gratuitas;
possuem rate limits;
não podem ser utilizadas para produção ou fins comerciais.

Resultado

Acadêmico/teste: 🟢
Produção: 🔴 com Trial Key
Comercial: 🔴 com Trial Key

....................................................

Resultado

🟡 Boa para experimentação, mas limitada para produção.

7. Hugging Face Inference Providers

Link de acesso: https://huggingface.co/

....................................................

Limite gratuito

🟡 MUITO PEQUENO

Usuários Free recebem atualmente:

US$ 0,10/mês em créditos

para Inference Providers.

A grande vantagem é poder acessar 200+ modelos/provedores.

Resultado

🟡 Free: US$0,10/mês

....................................................

Geração de resumo

🟢 SIM

Existem centenas de modelos de linguagem capazes de executar resumo.

....................................................

Tamanho máximo de entrada

🟡 DEPENDE DO MODELO

Como existem muitos modelos, não existe um único limite global.

....................................................

Necessidade de cartão

🟢 não para os créditos gratuitos

O modelo de cobrança só entra em funcionamento quando o usuário compra créditos/ativa utilização paga.

....................................................

Política de uso

🟡

A política depende:

da Hugging Face;
do provedor;
do modelo;
da licença do modelo.

....................................................

Resultado

🟡 Excelente ecossistema, mas os US$0,10 mensais são insuficientes para ser nossa principal API.

8. Cloudflare Workers AI

Link de acesso:https://www.cloudflare.com/products/workers-ai/

....................................................

Limite gratuito

🟢 10.000 Neurons/dia

A documentação atual confirma:

10.000 Neurons por dia gratuitamente.

O limite é reiniciado diariamente.

⚠️ Neurons não equivalem diretamente a requisições, pois o consumo depende do modelo.

Resultado

🟢 10.000 Neurons/dia

....................................................

Geração de resumo

🟢 SIM

Existem modelos de linguagem para geração e transformação de texto.

....................................................

Tamanho máximo de entrada

🟡 DEPENDE DO MODELO

Cada modelo possui seu próprio contexto.

....................................................

Necessidade de cartão

🟢 Free Workers AI não exige pagamento para os 10.000 Neurons gratuitos.

⚠️ Alguns modelos específicos exigem plano pago/método de pagamento. A documentação atual lista vários modelos que não podem ser usados apenas no Free.

....................................................

Política de uso

🟡

É necessário respeitar:

termos Cloudflare;
política do Workers AI;
licença dos modelos.

....................................................

Resultado

🟢 Excelente candidata gratuita.

9. SambaNova Cloud

Link de acesso:https://sambanova.ai/?utm_source=bing&utm_medium=cpc&utm_campaign=&utm_term=sambanova&utm_content=&hsa_acc=9878570407&hsa_cam=23161114681&hsa_grp=1230354808853925&hsa_ad=&hsa_src=o&hsa_tgt=kwd-76897616017192&hsa_kw=sambanova&hsa_mt=e&hsa_net=adwords&hsa_ver=3&msclkid=86fb0c91e76d102336a966312bd3cb35

....................................................

Limite gratuito

🟡

A SambaNova oferece acesso de avaliação/inferência, mas a quantidade gratuita deve ser tratada conforme a oferta vigente da conta.

Não vou transformar uma oferta promocional em uma franquia permanente sem confirmação oficial.

Resultado

🟡 Free: disponibilidade depende da oferta/account

....................................................

Geração de resumo

🟢 SIM

Os modelos disponibilizados são capazes de geração e transformação de texto.

....................................................

Tamanho máximo de entrada

🟡 DEPENDE DO MODELO

....................................................

Necessidade de cartão

🟡 DEPENDE DO cadastro/oferta

Não classificaria como "não exige cartão" sem confirmação específica para o nível atual da conta.

....................................................

Política de uso

🟡

Termos da SambaNova + condições do modelo utilizado.

....................................................

Resultado

🟡 Candidata secundária.

10. NVIDIA API / NIM

Link de acesso: https://build.nvidia.com/

....................................................

Limite gratuito

🟡

A plataforma disponibiliza acesso de avaliação a vários modelos.

Entretanto, o limite depende do modelo e do serviço.

Não é correto atribuir uma única franquia gratuita à NVIDIA API inteira.

....................................................

Geração de resumo

🟢 SIM

Os LLMs disponíveis podem executar:

resumo;
geração;
classificação;
análise.

....................................................

Tamanho máximo de entrada

🟡 DEPENDE DO MODELO

....................................................

Necessidade de cartão

🟡 DEPENDE DO serviço/modelo

....................................................

Política de uso

🟡

Existem:

termos NVIDIA;
licenças dos modelos;
condições específicas de cada modelo.

....................................................

Resultado

🟡 Excelente para experimentar modelos, mas não é a opção mais simples para nosso Free Tier.

11. Together AI

Link de acesso: https://www.together.ai/

....................................................

Limite gratuito

🟡 CRÉDITOS/TRIAL

A plataforma é principalmente baseada em inferência paga, com modalidades promocionais/créditos de avaliação.

Portanto, não considero correto classificá-la como uma API com uma franquia gratuita diária permanente.

....................................................

Geração de resumo

🟢 SIM

Grande catálogo de LLMs.

....................................................

Tamanho máximo de entrada

🟢/🟡 DEPENDE DO MODELO

....................................................

Necessidade de cartão

🟡 depende do método de acesso e créditos

....................................................

Política de uso

🟡

Depende:

Together AI;
modelo;
licença do modelo.

....................................................

Resultado

🟡 Boa plataforma de modelos, mas não está entre as melhores para o requisito de gratuidade permanente.

12. DeepInfra

Link de acesso: https://deepinfra.com/

....................................................

Limite gratuito

🟡 DEPENDE DA OFERTA

A plataforma oferece inferência de diversos modelos, mas o nível gratuito/créditos não é tão previsível quanto Gemini, Groq ou Cloudflare.

....................................................

Geração de resumo

🟢 SIM

Os modelos disponíveis podem executar geração e resumo.

....................................................

Tamanho máximo de entrada

🟡 DEPENDE DO MODELO

....................................................

Necessidade de cartão

🟡 depende do nível de utilização

....................................................

Política de uso

🟡

É necessário respeitar os termos do DeepInfra e a licença do modelo.

....................................................

Resultado

🟡 Boa alternativa técnica, mas não uma das primeiras escolhas para o Free Tier.

13. AI21 Labs

Link de acesso: https://www.ai21.com/
....................................................

Limite gratuito

🟡 TRIAL/CRÉDITOS

A disponibilidade gratuita está associada principalmente a avaliação/créditos, e não a uma franquia gratuita diária permanente equivalente à Groq.

....................................................

Geração de resumo

🟢 SIM

Os modelos da AI21 são destinados a geração e processamento de linguagem.

....................................................

Tamanho máximo de entrada

🟢/🟡 DEPENDE DO MODELO

....................................................

Necessidade de cartão

🟡 depende do tipo de conta/uso

....................................................

Política de uso

🟡

Termos da AI21 + condições do modelo.

....................................................

Resultado

🟡 Boa candidata para avaliação, mas não prioritária.

14. Fireworks AI

Link de acesso: https://fireworks.ai/

....................................................

Limite gratuito

🟡 CRÉDITOS/TRIAL

A Fireworks disponibiliza acesso de avaliação a modelos de IA.

Não devemos considerar esse acesso como uma franquia gratuita permanente.

....................................................

Geração de resumo

🟢 SIM

Possui diversos LLMs generativos.

....................................................

Tamanho máximo de entrada

🟢/🟡 DEPENDE DO MODELO

....................................................

Necessidade de cartão

🟡 depende da modalidade

....................................................

Política de uso

🟡

Termos da Fireworks + licença do modelo utilizado.

....................................................

Resultado

🟡 Boa plataforma de inferência, mas não prioritária para o MVP gratuito.

15. SiliconFlow

Link de acesso: https://www.siliconflow.com/

....................................................

Limite gratuito

🟡 DEPENDE DO MODELO/OFERTA

A SiliconFlow possui diversos modelos e modalidades de utilização, mas não considero seguro tratar toda a plataforma como uma única franquia gratuita permanente.

....................................................

Geração de resumo

🟢 SIM

Os modelos disponíveis podem gerar e resumir texto.

....................................................

Tamanho máximo de entrada

🟡 DEPENDE DO MODELO

....................................................

Necessidade de cartão

🟡 depende da modalidade de conta

....................................................

Política de uso

🟡

É necessário considerar:

termos da SiliconFlow;
licença individual do modelo;
eventuais restrições do provedor.

....................................................

API Principal: Google Gemini API
API Reserva: Groq Groq API
....................................................
API de imagem
1. Google Gemini API / Gemini Image

Link de acesso: https://ai.google.dev/gemini-api/docs/image-generation

....................................................

Limite gratuito

🟡 DEPENDE DO MODELO

A API Gemini possui modelos atuais de geração de imagens, como:

Gemini 3.1 Flash Image;
Gemini 3.1 Flash Lite Image;
Gemini 3 Pro Image.

Porém, os modelos atuais de geração de imagens não possuem nível gratuito de API para geração de imagens na tabela atual de preços. A geração de imagens é cobrada conforme o modelo e a quantidade de imagens/tokens gerados.

⚠️ Isso é diferente da IA de texto Gemini, que possui modelos com Free Tier.

Resultado

🟡 Geração de imagem via API: disponível, mas sem Free Tier de geração atualmente.

....................................................

Geração de imagem

🟢 SIM — excelente

A Gemini pode gerar imagens a partir de prompts e também realizar edição/transformação de imagens.

Os modelos atuais são destinados a:

geração de imagens;
edição;
composição;
criação visual;
geração com contexto textual.

A documentação atual recomenda os modelos Nano Banana para geração de imagens.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟢 SIM — depende do modelo

Cada modelo possui suas próprias características de resolução e limites.

Por exemplo, o Gemini 3.1 Flash Image possui geração em:

512 px;
1K;
2K;
4K,

dependendo da configuração/modelo.

Resultado

🟢 Adequado para geração de imagens do projeto.

....................................................

Necessidade de cartão

🟡 DEPENDE

Para geração de imagens via API, os modelos atuais são cobrados.

Portanto, para utilizar a geração de imagens além de testes disponíveis no Google AI Studio, pode ser necessário habilitar faturamento.

⚠️ Não devemos considerar a geração de imagens do Gemini como "gratuita" apenas porque a API Gemini possui Free Tier para determinados modelos de texto.

Resultado

🟡 Para uso gratuito via API: não é uma opção segura.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Gemini API possui API HTTP/REST e pode ser integrada diretamente ao MVC.

É possível enviar o prompt para a API e receber o resultado da geração.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário respeitar:

termos do Google;
políticas de uso;
políticas de conteúdo;
direitos autorais;
regras relacionadas às imagens geradas;
regras de tratamento dos dados enviados.

Resultado

Uso acadêmico: 🟢
Geração de imagens: 🟢
Conteúdo de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA API TECNICAMENTE, MAS NÃO É A MELHOR PARA O REQUISITO GRATUITO.

2. Stability AI API

Link de acesso: https://platform.stability.ai/

....................................................

Limite gratuito

🟢 SIM — 25 créditos iniciais

A Stability AI oferece 25 créditos gratuitos para novos usuários. A própria documentação informa que a criação da conta utilizando Google concede esses créditos.

Os créditos são consumidos conforme o modelo utilizado.

Por exemplo, a tabela atual apresenta diferentes custos para os modelos de geração de imagem.

Resultado

🟢 Free: SIM — 25 créditos iniciais

....................................................

Geração de imagem

🟢 SIM — excelente

A Stability AI é especializada em geração de imagens.

A API oferece serviços para:

geração;
edição;
transformação;
inpainting;
criação visual.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟢 DEPENDE DO MODELO

Cada serviço possui sua própria resolução e consumo de créditos.

A quantidade de imagens possíveis depende diretamente do modelo escolhido e do custo em créditos.

Resultado

🟢 Adequado para geração de imagens.

....................................................

Necessidade de cartão

🟢 NÃO para receber os créditos iniciais

A documentação informa que é possível criar a conta utilizando Google e receber 25 créditos gratuitos.

⚠️ Depois que os créditos gratuitos terminarem, será necessário comprar créditos para continuar utilizando a API.

Resultado

🟢 Cartão para começar: não é necessário para os créditos iniciais.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Stability possui API HTTP com autenticação por API Key.

As requisições utilizam o header Authorization e podem retornar a imagem diretamente ou como dados codificados.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário respeitar:

termos da Stability AI;
políticas de conteúdo;
propriedade intelectual;
regras de utilização da API;
condições específicas dos modelos.

Resultado

Uso acadêmico: 🟢
Geração de imagem: 🟢
Conteúdo de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟢 EXCELENTE CANDIDATA

3. Cloudflare Workers AI

Link de acesso: https://developers.cloudflare.com/workers-ai/

....................................................

Limite gratuito

🟢 SIM — 10.000 Neurons/dia

A Cloudflare disponibiliza atualmente:

10.000 Neurons por dia gratuitamente.

O limite é reiniciado diariamente às 00:00 UTC.

⚠️ Neurons não correspondem diretamente a "número de imagens". O consumo depende do modelo utilizado.

Por exemplo, o FLUX.1 Schnell possui preços em Neurons por tile e por step.

Resultado

🟢 Free: 10.000 Neurons/dia

....................................................

Geração de imagem

🟢 SIM

O Workers AI disponibiliza modelos específicos para geração de imagens.

Entre eles estão modelos da família FLUX e outros modelos de geração visual.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟢 DEPENDE DO MODELO

O consumo varia conforme:

modelo;
resolução;
número de steps;
tamanho da imagem;
processamento utilizado.

Por exemplo, o preço atual do FLUX.1 Schnell é calculado em Neurons por tile/step.

Resultado

🟢 Adequado para o projeto.

....................................................

Necessidade de cartão

🟢 NÃO para a alocação gratuita básica

O Workers AI possui uma alocação gratuita de 10.000 Neurons/dia.

⚠️ Alguns modelos específicos exigem plano pago ou método de pagamento. A própria Cloudflare identifica modelos que exigem billing.

Resultado

🟢 Free básico: não
⚠️ Alguns modelos: podem exigir pagamento

....................................................

Possibilidade de integração via HTTP

🟢 SIM

O Workers AI pode ser utilizado através da API e integrado a aplicações web.

Isso permite que o MVC envie o prompt e receba a imagem gerada.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário considerar:

termos da Cloudflare;
políticas do Workers AI;
políticas de conteúdo;
licença do modelo utilizado.

Resultado

Uso acadêmico: 🟢
Geração de imagem: 🟢
Modelos gratuitos: 🟢
Restrições específicas de modelos: ⚠️

....................................................

Resultado

🟢 EXCELENTE CANDIDATA

4. Hugging Face Inference Providers

Link de acesso: https://huggingface.co/docs/inference-providers

....................................................

Limite gratuito

🟡 MUITO PEQUENO

Usuários gratuitos recebem atualmente:

US$ 0,10/mês

em créditos para Inference Providers. O valor está sujeito a alteração.

A plataforma oferece acesso a mais de 200 modelos/provedores e inclui modelos de geração de imagem.

Resultado

🟡 Free: US$ 0,10/mês

....................................................

Geração de imagem

🟢 SIM

A Hugging Face possui suporte à tarefa Text-to-Image.

Entre os modelos recomendados atualmente estão:

FLUX;
Qwen Image;
Hyper-SD.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟡 DEPENDE DO MODELO

Como existem muitos modelos e provedores, não existe um único limite global de resolução ou quantidade.

Resultado

🟡 Depende do modelo escolhido.

....................................................

Necessidade de cartão

🟢 NÃO para utilizar os créditos gratuitos

O usuário pode utilizar os créditos incluídos sem precisar contratar um plano pago.

⚠️ Depois de consumir os créditos, é necessário comprar créditos para continuar usando a infraestrutura paga.

Resultado

🟢 Cartão para começar: não necessário

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Hugging Face permite utilizar os Inference Providers através de API, inclusive com requisições HTTP utilizando User Access Token.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

Além dos termos da Hugging Face, é necessário verificar:

licença do modelo;
provedor utilizado;
restrições específicas;
direitos sobre o conteúdo gerado.

Resultado

Uso acadêmico: 🟢
Geração de imagem: 🟢
Modelos de terceiros: ⚠️
Licença individual: ⚠️

....................................................

Resultado

🟡 BOA TECNICAMENTE, MAS O LIMITE GRATUITO É MUITO PEQUENO.

5. Pollinations AI

Link de acesso: https://pollinations.ai/

....................................................

Limite gratuito

🟡 DEPENDE DO MODELO/SERVIÇO

A Pollinations oferece geração de imagens através de sua infraestrutura de IA.

Porém, os limites, modelos disponíveis e condições de utilização podem variar.

⚠️ Portanto, não devemos tratar a plataforma como tendo uma franquia fixa de imagens por dia sem considerar o serviço/modelo específico.

Resultado

🟡 Gratuidade: disponível em determinadas modalidades, mas limite variável.

....................................................

Geração de imagem

🟢 SIM

A plataforma oferece geração de imagens através de prompts.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟡 DEPENDE DO MODELO

Os limites dependem do serviço utilizado.

Resultado

🟡 Limite variável.

....................................................

Necessidade de cartão

🟢 NÃO como requisito geral para começar

A plataforma possui modalidades de utilização que não exigem contratação imediata de um serviço pago.

Resultado

🟢 Cartão: não é requisito geral para começar.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Pollinations disponibiliza endpoints HTTP para geração de imagens.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário observar:

termos da plataforma;
políticas de conteúdo;
direitos autorais;
regras sobre utilização do conteúdo gerado;
condições específicas de recursos experimentais.

Resultado

Uso acadêmico: 🟢
Geração de imagem: 🟢
Conteúdo de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA CANDIDATA PARA PROTÓTIPO/EXPERIMENTAÇÃO.

6. Replicate

Link de acesso: https://replicate.com/

....................................................

Limite gratuito

🟡 DEPENDE DA CONTA

A Replicate disponibiliza inúmeros modelos de geração de imagens.

Porém, a utilização é predominantemente baseada em consumo/modelo, e não em uma grande franquia gratuita permanente.

Resultado

🟡 Free: depende de créditos/ofertas da conta.

....................................................

Geração de imagem

🟢 SIM

A Replicate oferece diversos modelos de geração de imagens através de uma API unificada.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟢/🟡 DEPENDE DO MODELO

Cada modelo possui suas próprias características de:

resolução;
steps;
tempo de execução;
consumo.

Resultado

🟡 Depende do modelo.

....................................................

Necessidade de cartão

🟡 DEPENDE DA CONTA E DO USO

Como a plataforma é baseada em cobrança por utilização, a necessidade de faturamento depende da modalidade utilizada.

Resultado

🟡 Não é a opção mais simples para um projeto estritamente gratuito.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Replicate disponibiliza API para executar modelos remotamente.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário considerar:

termos da Replicate;
política de uso;
licença do modelo;
eventuais restrições do modelo escolhido.

Resultado

Uso acadêmico: 🟢
Geração de imagem: 🟢
Modelos de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 EXCELENTE TECNICAMENTE, MAS NÃO É A MELHOR PARA GRATUIDADE.

7. fal.ai

Link de acesso: https://fal.ai/

....................................................

Limite gratuito

🟡 DEPENDE DE CRÉDITOS/OFERTA

A fal.ai oferece acesso a vários modelos de geração de imagem.

A utilização é predominantemente baseada no consumo dos modelos.

Resultado

🟡 Free: depende da oferta/créditos da conta.

....................................................

Geração de imagem

🟢 SIM

A fal.ai possui diversos modelos de geração de imagens.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟢/🟡 DEPENDE DO MODELO

Cada modelo possui suas próprias especificações de:

resolução;
steps;
geração;
consumo.

Resultado

🟡 Depende do modelo.

....................................................

Necessidade de cartão

🟡 DEPENDE DA MODALIDADE

A utilização paga é baseada em consumo, portanto o método de faturamento depende da configuração da conta.

Resultado

🟡 Não é a opção mais simples para Free Tier.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A fal.ai disponibiliza API para integração com aplicações.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

Devem ser observados:

termos da fal.ai;
políticas de conteúdo;
licenças dos modelos;
restrições específicas de cada modelo.

Resultado

Uso acadêmico: 🟢
Geração de imagem: 🟢
Modelos de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA CANDIDATA TÉCNICA, MAS NÃO PRIORITÁRIA PARA GRATUIDADE.

8. Together AI

Link de acesso: https://www.together.ai/

....................................................

Limite gratuito

🟡 DEPENDE DE CRÉDITOS/OFERTAS

A Together AI oferece modelos de geração de imagem através de API.

A utilização é predominantemente baseada em consumo.

Resultado

🟡 Gratuidade: depende da modalidade/créditos disponíveis.

....................................................

Geração de imagem

🟢 SIM

A plataforma disponibiliza modelos generativos de imagem.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟡 DEPENDE DO MODELO

Cada modelo possui suas próprias limitações de:

resolução;
quantidade;
processamento.

Resultado

🟡 Depende do modelo.

....................................................

Necessidade de cartão

🟡 DEPENDE DO ACESSO

Pode variar conforme a modalidade da conta e a utilização de créditos.

Resultado

🟡 Não é uma opção ideal se o requisito for ausência de cobrança.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Together AI disponibiliza API para integração com aplicações.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário considerar:

termos da Together AI;
política de uso;
licença do modelo;
restrições específicas.

Resultado

Uso acadêmico: 🟢
Geração de imagem: 🟢
Modelos de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA TECNICAMENTE, MAS NÃO PRIORITÁRIA PARA O PROJETO GRATUITO.

9. DeepInfra

Link de acesso: https://deepinfra.com/

....................................................

Limite gratuito

🟡 DEPENDE DA OFERTA

O DeepInfra disponibiliza vários modelos de geração de imagem, normalmente com cobrança baseada na utilização.

Resultado

🟡 Free: depende da oferta/créditos disponíveis.

....................................................

Geração de imagem

🟢 SIM

A plataforma oferece modelos de geração de imagens através de API.

Resultado

🟢 Geração de imagem: SIM

....................................................

Limite/tamanho

🟡 DEPENDE DO MODELO

Os limites de resolução e utilização dependem do modelo escolhido.

Resultado

🟡 Depende do modelo.

....................................................

Necessidade de cartão

🟡 DEPENDE DA CONTA

A utilização paga pode exigir configuração de faturamento.

Resultado

🟡 Não é a opção mais simples para um projeto totalmente gratuito.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

O DeepInfra disponibiliza APIs para integração com aplicações.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário respeitar:

termos do DeepInfra;
política de uso;
licença dos modelos;
restrições de cada modelo.

Resultado

Uso acadêmico: 🟢
Geração de imagem: 🟢
Modelos de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA ALTERNATIVA TÉCNICA, MAS NÃO PRIORITÁRIA PARA O FREE TIER.

API Principal e Reserva

Considerando gratuidade, limites, cartão, HTTP e termos, as duas que ficam mais interessantes para o seu projeto são:

API Principal: Cloudflare Workers AI

API Reserva: Stability AI API

....................................................

🖼️ Definição

API de imagem principal: Cloudflare Workers AI

API de imagem reserva: Stability AI API

A Cloudflare tem uma vantagem importante para o seu requisito: 10.000 Neurons gratuitos todos os dias, enquanto a Stability fornece 25 créditos iniciais.
....................................................
API de vídeo
1. fal.ai

Link de acesso: https://fal.ai/

....................................................

Limite gratuito

🟢 SIM — 5 gerações gratuitas por dia

A fal.ai oferece atualmente 5 gerações gratuitas de vídeo por dia para o modelo MiniMax H3 Max.

Cada geração gratuita pode ter até 15 segundos e pode incluir áudio nativo. A franquia é disponibilizada para usuários conectados no Sandbox.

Além disso, a geração pode ser feita tanto por:

texto → vídeo;
imagem → vídeo.

Resultado

🟢 Free: SIM — 5 gerações/dia

....................................................

Geração de vídeo

🟢 SIM — excelente

A fal.ai disponibiliza diversos modelos de geração de vídeo.

O H3 Max oferece:

Text-to-Video;
Image-to-Video;
geração com áudio;
vídeos de até 15 segundos.

Também existem outros modelos e fornecedores dentro da plataforma.

Resultado

🟢 Geração de vídeo: SIM

....................................................

Limite/tamanho

🟢 DEPENDE DO MODELO

No H3 Max, a geração gratuita chega a:

768p;
até 15 segundos por geração.

A versão H3 Max também possui opções de 480p, 768p e 1080p quando utilizada de forma paga.

Resultado

🟢 Adequado para o MVP.

....................................................

Necessidade de cartão

🟢 NÃO para a franquia gratuita do H3 Max

A fal informa que as cinco gerações diárias gratuitas estão disponíveis para usuários conectados sem assinatura.

A utilização paga somente passa a ser necessária quando o usuário deseja ultrapassar a franquia gratuita.

Resultado

🟢 Cartão para começar: não necessário para o Free do H3 Max

....................................................

Tempo de geração

🟢 MUITO RÁPIDO

A fal.ai informa que um vídeo de 5 segundos em 768p pode ser gerado em menos de 3 segundos pelo H3 Max em determinadas condições.

Resultado

🟢 Tempo de geração: excelente

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A fal.ai disponibiliza endpoints de API para os modelos.

O mesmo modelo H3 Max possui endpoints para:

Text-to-Video;
Image-to-Video.

A API utiliza autenticação e permite integrar a geração diretamente ao backend do projeto.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário considerar:

termos da fal.ai;
política de uso;
políticas de conteúdo;
licença do modelo;
condições específicas do modelo utilizado.

Como a plataforma reúne modelos de diferentes fornecedores, a licença pode variar conforme o modelo.

Resultado

Uso acadêmico: 🟢
Geração de vídeo: 🟢
Modelos de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟢 EXCELENTE CANDIDATA — MELHOR OPÇÃO ENCONTRADA

2. Magic Hour API

Link de acesso: https://magichour.ai/api

....................................................

Limite gratuito

🟢 SIM — 400 créditos iniciais + 100 créditos/dia

A camada Free atualmente oferece:

400 créditos no cadastro;
100 créditos por dia;
resolução máxima de 576 px.

A documentação também informa que a conta Free continua tendo acesso à API.

Resultado

🟢 Free: SIM — 400 créditos + 100 créditos/dia

....................................................

Geração de vídeo

🟢 SIM — excelente

A Magic Hour oferece diversos recursos de geração e edição de vídeo.

A plataforma possui, entre outros:

geração de vídeo;
animação;
Face Swap;
Lip Sync;
Talking Photo;
ferramentas de transformação.

A própria documentação demonstra a criação de vídeo através da API.

Resultado

🟢 Geração de vídeo: SIM

....................................................

Limite/tamanho

🟡 LIMITADO NO FREE

O plano gratuito possui limite de 576 px.

Planos superiores aumentam a resolução para:

1024 px;
1472 px;
até 4K em determinados modos.

Resultado

🟡 Gratuito: 576 px

....................................................

Necessidade de cartão

🟢 NÃO para começar no Free

A conta pode começar utilizando a franquia gratuita.

Depois que os créditos forem consumidos, é necessário comprar créditos ou mudar de plano.

Resultado

🟢 Cartão para começar: não necessário para utilizar a camada Free

....................................................

Tempo de geração

🟡 DEPENDE DO MODELO/MODO

A geração de vídeo é um processo que pode levar algum tempo.

A Magic Hour trabalha com processamento assíncrono e permite utilização de webhooks, evitando que a aplicação precise ficar esperando continuamente pela resposta.

Resultado

🟢 Adequado para aplicações MVC.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Magic Hour possui API e documentação para integração programática.

A própria documentação apresenta erros HTTP e autenticação através de API Key, além de permitir utilização de webhooks para resultados assíncronos.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário verificar:

termos da Magic Hour;
política de conteúdo;
licença de utilização;
condições comerciais;
limitações da camada gratuita.

Um ponto importante é que o plano Creator adiciona explicitamente uma licença para uso comercial, enquanto o Free é voltado principalmente para utilização limitada/testes.

Resultado

Uso acadêmico: 🟢
Geração de vídeo: 🟢
Uso comercial no Free: ⚠️
Conteúdo de terceiros: ⚠️

....................................................

Resultado

🟢 EXCELENTE CANDIDATA — PRINCIPAL RESERVA

3. Runway API

Link de acesso: https://dev.runwayml.com/

....................................................

Limite gratuito

🔴 NÃO — API baseada em créditos pagos

A API Runway Dev atualmente cobra cada geração em créditos.

Os créditos custam US$ 0,01 por crédito.

Exemplos atuais:

H3 Max 480p: 5 créditos/s;
H3 Max 768p: 8 créditos/s;
Gen-4.5: 12 créditos/s;
Veo 3.1 sem áudio: 20 créditos/s;
Veo 3.1 com áudio: 40 créditos/s.

Resultado

🔴 Não possui Free Tier de API comparável ao fal.ai.

....................................................

Geração de vídeo

🟢 SIM — excelente

A Runway oferece:

Text-to-Video;
Image-to-Video;
Video-to-Video;
Character Performance;
Avatar Video;
outros recursos.

Resultado

🟢 Geração de vídeo: SIM

....................................................

Limite/tamanho

🟢 DEPENDE DO MODELO

A API possui diferentes modelos e resoluções.

Por exemplo, o Gen-4.5 permite geração em diferentes proporções e duração de vídeo. A API demonstra geração de vídeos de 5 segundos.

Resultado

🟢 Tecnicamente excelente.

....................................................

Necessidade de cartão

🔴 PAGAMENTO NECESSÁRIO PARA USO CONTÍNUO

Como a API utiliza créditos pagos, não é uma opção adequada para o requisito de API totalmente gratuita.

Resultado

🔴 Não atende ao requisito principal.

....................................................

Tempo de geração

🟢 ASSÍNCRONO

A Runway cria uma tarefa e devolve um ID.

Depois o sistema consulta o status até a conclusão.

A própria documentação mostra o fluxo:

POST → task ID → consulta → resultado.

Resultado

🟢 Excelente para MVC.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Runway disponibiliza endpoints HTTP.

Por exemplo:

POST /v1/image_to_video

A documentação apresenta inclusive uma chamada curl diretamente contra a API.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

Devem ser observados:

termos da Runway;
política de uso;
direitos sobre os conteúdos;
condições comerciais;
licença dos modelos.

Resultado

Uso acadêmico: 🟢
Geração de vídeo: 🟢
Uso comercial: 🟢/⚠️ conforme plano/termos
Uso indevido: 🔴

....................................................

Resultado

🟡 EXCELENTE TECNICAMENTE, MAS NÃO É A MELHOR PARA GRATUIDADE.

4. Luma API / Dream Machine

Link de acesso: https://lumalabs.ai/

....................................................

Limite gratuito

🟡 LIMITADO/DEPENDE DA CONTA

A Luma possui utilização gratuita/teste em seus produtos, porém não considero isso uma franquia gratuita de API recorrente equivalente aos cinco vídeos diários do fal.ai.

Resultado

🟡 Gratuidade: limitada

....................................................

Geração de vídeo

🟢 SIM — excelente

A Luma é especializada em geração de vídeo por IA.

Possui recursos de:

Text-to-Video;
Image-to-Video;
transformação de vídeo;
criação cinematográfica.

Resultado

🟢 Geração de vídeo: SIM

....................................................

Limite/tamanho

🟢 DEPENDE DO MODELO

A duração, resolução e capacidade de geração dependem do modelo e do plano.

Resultado

🟢 Tecnicamente adequada.

....................................................

Necessidade de cartão

🟡 DEPENDE DA MODALIDADE

O acesso gratuito/teste não deve ser confundido com uma franquia gratuita permanente da API.

Resultado

🟡 Não é a opção mais segura para o requisito sem pagamento.

....................................................

Tempo de geração

🟡 ASSÍNCRONO

A geração de vídeo envolve processamento e pode levar mais tempo que uma requisição convencional de texto ou imagem.

Resultado

🟢 Adequada tecnicamente.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Luma oferece acesso programático aos seus modelos.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

Devem ser observados:

termos da Luma;
política de conteúdo;
direitos autorais;
condições da API;
licença e utilização comercial.

Resultado

Uso acadêmico: 🟢
Geração de vídeo: 🟢
Conteúdo de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA TECNICAMENTE, MAS NÃO PRIORITÁRIA PARA O FREE TIER.

5. MiniMax / Hailuo Video API

Link de acesso: https://www.minimaxi.com/

....................................................

Limite gratuito

🟡 LIMITADO/CRÉDITOS

A MiniMax oferece modelos de vídeo através de API, mas a utilização da API é baseada principalmente em créditos/consumo.

Não considero uma franquia gratuita recorrente suficientemente clara para substituir o Free Tier da fal.ai.

Resultado

🟡 Gratuidade: limitada

....................................................

Geração de vídeo

🟢 SIM — excelente

Os modelos MiniMax/Hailuo oferecem:

Text-to-Video;
Image-to-Video;
geração de vídeo de maior duração;
diferentes resoluções.

Resultado

🟢 Geração de vídeo: SIM

....................................................

Limite/tamanho

🟢 DEPENDE DO MODELO

Os modelos possuem diferentes:

resoluções;
durações;
proporções;
capacidades de referência.

Resultado

🟢 Muito boa tecnicamente.

....................................................

Necessidade de cartão

🟡 DEPENDE DA CONTA/CRÉDITOS

A utilização contínua da API depende do saldo/créditos.

Resultado

🟡 Não é a opção mais simples para o requisito gratuito.

....................................................

Tempo de geração

🟢 RÁPIDO EM MODELOS OTIMIZADOS

A infraestrutura da fal.ai, por exemplo, informa que o H3 Max — baseado em MiniMax — pode gerar um vídeo de 5 segundos em menos de 3 segundos em 768p.

Resultado

🟢 Excelente quando utilizado através de infraestrutura otimizada.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

Os modelos MiniMax podem ser utilizados através de APIs.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário verificar:

termos da MiniMax;
política de uso;
licença do modelo;
direitos sobre conteúdo gerado;
utilização comercial.

Resultado

Uso acadêmico: 🟢
Geração de vídeo: 🟢
Conteúdo de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA TECNICAMENTE, MAS A FAL.AI É MAIS INTERESSANTE PARA O FREE TIER.

6. Pika API

Link de acesso: https://pika.art/

....................................................

Limite gratuito

🔴 NÃO NA API

É importante separar a plataforma Pika da Pika API.

A plataforma Pika possui atualmente um plano gratuito com 80 créditos mensais, incluindo Pika 2.5 em 480p e determinados recursos.

Porém, a API possui modelo Pay-as-you-go, sem assinatura obrigatória e sem mínimo, mas baseada em cobrança por utilização.

Resultado

🔴 API Free Tier: não

....................................................

Geração de vídeo

🟢 SIM

A Pika oferece diversos modelos de geração de vídeo.

Resultado

🟢 Geração de vídeo: SIM

....................................................

Limite/tamanho

🟢 DEPENDE DO MODELO

A plataforma API atualmente possui dezenas de modelos de vídeo e preços diferentes.

Por exemplo, o Happyhorse 1.0 é cobrado por segundo.

Resultado

🟢 Tecnicamente adequada.

....................................................

Necessidade de cartão

🟡 DEPENDE DA API/COBRANÇA

A API funciona por consumo.

Resultado

🟡 Não é adequada como solução gratuita principal.

....................................................

Tempo de geração

🟡 DEPENDE DO MODELO

A geração depende do modelo e da duração.

Resultado

🟡 Adequado tecnicamente.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Pika possui API para geração programática.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

Devem ser considerados:

termos da Pika;
política de conteúdo;
licenças dos modelos;
condições de uso comercial.

Resultado

Uso acadêmico: 🟢
Geração de vídeo: 🟢
Uso comercial: ⚠️ conforme serviço
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA TECNICAMENTE, MAS NÃO É A MELHOR PARA API GRATUITA.

7. Replicate

Link de acesso: https://replicate.com/

....................................................

Limite gratuito

🟡 DEPENDE DA CONTA/MODELO

A Replicate disponibiliza uma grande quantidade de modelos de vídeo, mas não possui uma franquia universal de vídeo gratuita comparável aos cinco vídeos diários da fal.ai.

A cobrança é normalmente baseada no modelo e no hardware/tempo utilizado.

Resultado

🟡 Free: depende da conta/oferta

....................................................

Geração de vídeo

🟢 SIM — excelente

A Replicate disponibiliza modelos de:

Text-to-Video;
Image-to-Video;
Video-to-Video;
animação;
geração multimodal.

Resultado

🟢 Geração de vídeo: SIM

....................................................

Limite/tamanho

🟢 DEPENDE DO MODELO

Como a plataforma oferece vários modelos, cada um pode ter:

resolução;
duração;
FPS;
limite de entrada;
custo;

diferentes.

Resultado

🟢 Grande flexibilidade.

....................................................

Necessidade de cartão

🟡 DEPENDE DA CONTA

A utilização contínua é baseada em consumo e pode exigir configuração de faturamento.

Resultado

🟡 Não é a opção mais simples para um MVP sem custos.

....................................................

Tempo de geração

🟡 DEPENDE DO MODELO

Modelos diferentes apresentam tempos de processamento diferentes.

Resultado

🟡 Adequado, mas variável.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A Replicate possui API para executar modelos remotamente.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

É necessário verificar:

termos da Replicate;
política de uso;
licença do modelo;
restrições específicas;
direitos sobre o conteúdo.

Resultado

Uso acadêmico: 🟢
Geração de vídeo: 🟢
Modelos de terceiros: ⚠️
Uso indevido: 🔴

....................................................

Resultado

🟡 EXCELENTE TECNICAMENTE, MAS NÃO É A MELHOR PARA GRATUIDADE.

8. GenRelay

Link de acesso: https://genrelay.ai/

....................................................

Limite gratuito

🟢 SIM — US$ 0,50 DE CRÉDITO INICIAL

A GenRelay atualmente oferece US$ 0,50 em crédito gratuito para novos usuários.

Depois disso, o serviço funciona por pré-pagamento.

Resultado

🟢 Free: US$ 0,50 inicial

....................................................

Geração de vídeo

🟢 SIM

A plataforma oferece modelos como:

Grok Imagine 1.0;
Grok Imagine 1.5;
Veo 3.1;
Gemini Omni Flash.

Resultado

🟢 Geração de vídeo: SIM

....................................................

Limite/tamanho

🟡 DEPENDE DO MODELO

Por exemplo, atualmente:

Grok Imagine 1.0: cobrança por segundo;
Grok Imagine 1.5: cobrança por geração;
Veo 3.1: cobrança por geração;
Gemini Omni Flash: cobrança por geração.

Resultado

🟡 Depende do modelo.

....................................................

Necessidade de cartão

🟢 NÃO para começar

A GenRelay informa que novos usuários recebem US$ 0,50 de crédito gratuito para testar os modelos.

Resultado

🟢 Cartão para começar: não necessário

....................................................

Tempo de geração

🟡 DEPENDE DO MODELO

O tempo varia conforme o modelo utilizado e a duração/resolução solicitada.

Resultado

🟡 Variável.

....................................................

Possibilidade de integração via HTTP

🟢 SIM

A GenRelay funciona como uma plataforma de API para os modelos disponibilizados.

Resultado

🟢 HTTP: SIM

....................................................

Termos de uso

🟡 ATENÇÃO

A plataforma informa que o usuário recebe licença comercial sobre os assets gerados, mas continuam existindo condições específicas de uso e da plataforma.

Resultado

Uso acadêmico: 🟢
Geração de vídeo: 🟢
Uso comercial: 🟢
Uso indevido: 🔴

....................................................

Resultado

🟡 BOA PARA TESTES, MAS O CRÉDITO INICIAL É PEQUENO.

API Principal e Reserva

Considerando gratuidade real, quantidade de gerações, ausência de cartão, qualidade, velocidade, HTTP e adequação ao MVP:

🥇 API Principal: fal.ai

A fal.ai é a melhor escolha porque atualmente oferece uma franquia realmente utilizável:

5 vídeos gratuitos por dia;
até 15 segundos;
até 768p no H3 Max;
áudio nativo;
Text-to-Video;
Image-to-Video;
geração extremamente rápida;
API;
integração HTTP;
sem necessidade de assinatura para utilizar a franquia gratuita.
🥈 API Reserva: Magic Hour API

A Magic Hour fica como reserva porque oferece:

400 créditos no cadastro;
100 créditos/dia;
API;
geração de vídeo;
processamento assíncrono;
webhooks;
sem necessidade de cartão para começar;
resolução gratuita de até 576 px.

....................................................

🎬 Definição

API de vídeo principal: fal.ai

API de vídeo reserva: Magic Hour API

....................................................
Tabela 1 — Comparativa das APIs de Notícias
+----+----------------------+----------------------+----------+---------+----------+----------------------+
| Nº | API                  | Limite Free          | Brasil   | País    | Cidade   | Imagem              |
+----+----------------------+----------------------+----------+---------+----------+----------------------+
|  1 | NewsData.io          | 200 créditos/dia     | SIM      | SIM     | SIM*     | SIM                 |
|  2 | GNews                | 100 req/dia          | SIM      | SIM     | NÃO      | SIM                 |
|  3 | NewsAPI.org          | 100 req/dia          | SIM      | SIM     | NÃO      | SIM                 |
|  4 | APITube              | 100 req/dia          | SIM      | SIM     | SIM      | SIM                 |
|  5 | Mediastack           | 100 chamadas/mês     | SIM      | SIM     | NÃO      | SIM                 |
|  6 | The News API         | 100 req/dia          | SIM      | SIM     | NÃO      | SIM                 |
|  7 | World News API       | 50 pontos/dia        | SIM      | SIM     | SIM      | SIM                 |
|  8 | Currents API         | 250 req/dia          | SIM      | SIM     | PARCIAL  | SIM                 |
|  9 | NewsCatcher          | Limitado             | SIM      | SIM     | SIM      | SIM                 |
| 10 | Perigon              | 150 req/mês          | SIM      | SIM     | SIM      | SIM                 |
| 11 | Guardian Open API    | Quota não confirmada | PARCIAL  | NÃO*    | NÃO      | SIM                 |
| 12 | Google News RSS      | Não especificado     | SIM      | SIM     | SIM**    | PARCIAL             |
| 13 | Bing News Search     | ~1000/mês***         | SIM      | SIM     | NÃO      | SIM                 |
| 14 | FreeNewsAPI.io       | 5000 req/dia         | SIM      | SIM     | NÃO      | SIM                 |
| 15 | Scrapingdog          | Depende do plano     | DEPENDE  | DEPENDE | DEPENDE  | DEPENDE             |
| 16 | Webz.io              | ~500 chamadas/mês    | SIM      | SIM     | SIM      | SIM                 |
+----+----------------------+----------------------+----------+---------+----------+---------

....................................................
2. Definir stack final
================================================================
                         STACK FINAL
================================================================

CAMADA DE NOTÍCIAS
    Principal: APITube
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

APITube — API Principal de Notícias

Foi escolhida como API principal por apresentar uma combinação adequada de cobertura de notícias, filtros geográficos, notícias locais, imagens, integração HTTP e camada gratuita. Também atende melhor à necessidade do projeto de trabalhar com notícias brasileiras e localização.
================================================================
NewsData.io foi definida como reserva por possuir boa cobertura, suporte ao Brasil, filtros, imagens e uma franquia gratuita relevante.
================================================================
Google Gemini API — API Principal de IA de Texto

Foi escolhida como principal porque possui modelos com Free Tier, capacidade de processamento de texto, geração de resumos, grande capacidade de contexto e integração por HTTP.
================================================================
Groq API foi definida como reserva principalmente pela alta velocidade de inferência e pela existência de limites gratuitos utilizáveis.
================================================================
Cloudflare Workers AI — API Principal de Imagem

Foi escolhida como principal porque possui uma franquia gratuita recorrente de 10.000 Neurons por dia, além de modelos de geração de imagens e integração via HTTP.

Stability AI API foi definida como reserva porque oferece 25 créditos iniciais gratuitos e é especializada em geração e edição de imagens.
================================================================
fal.ai — API Principal de Vídeo

Foi escolhida como principal porque possui uma modalidade com gerações gratuitas diárias, geração de vídeo, integração HTTP, processamento assíncrono e suporte a diferentes modelos.
================================================================
Magic Hour API foi definida como reserva porque possui uma camada gratuita própria, créditos iniciais, franquia diária e integração via API.
================================================================
Justificativa geral da arquitetura:

A utilização de API Principal + API Reserva evita que uma indisponibilidade de determinado serviço interrompa completamente a aplicação.

A arquitetura fica:

API Principal
     │
     ├── funcionando ──► utiliza normalmente
     │
     └── indisponível
              │
              ▼
        API Reserva
              │
              ▼
        continua funcionando
