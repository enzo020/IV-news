# Banco de Dados do IV News — Guia Simples

Esse documento explica, de um jeito bem direto, como é o banco de dados do projeto e como qualquer pessoa consegue criar ele do zero na própria máquina.

---

## 1. O que é o banco e onde ele fica

O banco usado é o **SQLite**. Ele é só um arquivo (`ivnews.db`), não precisa instalar servidor nem nada disso.

Esse arquivo **não fica salvo no Git** — cada pessoa do time cria o próprio banco na sua máquina, seguindo o passo a passo lá embaixo (seção 4).

Os arquivos importantes ficam em:
- `Back-End/Model/` → onde ficam as "classes" que viram as tabelas
- `Back-End/Data/AppDbContext.cs` → o arquivo que organiza tudo e insere os dados de teste
- `Back-End/Migrations/` → o histórico de mudanças do banco (não mexer nesses arquivos na mão)

---

## 2. As 4 tabelas do banco, explicadas simples

### Tabela: Categorias
Serve pra dizer qual é o assunto da notícia (Política, Esporte, etc).

| Campo | O que é |
|---|---|
| Id | número que identifica a categoria |
| Nome | nome da categoria, tipo "Esporte" |
| Descricao | uma frase explicando a categoria |

### Tabela: Localizacoes
Serve pra dizer de onde é a notícia.

| Campo | O que é |
|---|---|
| Id | número que identifica a localização |
| Cidade | nome da cidade |
| Estado | nome ou sigla do estado |
| Pais | nome do país (fica "Brasil" se não informar outro) |

### Tabela: Noticias
Essa é a tabela principal — cada linha é uma notícia.

| Campo | O que é |
|---|---|
| Id | número que identifica a notícia |
| Titulo | título da notícia |
| Conteudo | o texto completo da notícia |
| Autor | quem escreveu |
| Fonte | de onde veio a notícia |
| ImagemUrl | link da imagem da notícia |
| PublicadoEm | data que foi publicada |
| CategoriaId | liga essa notícia a uma linha da tabela Categorias |
| LocalizacaoId | liga essa notícia a uma linha da tabela Localizacoes |

### Tabela: Resumos
Cada notícia pode ter um resumo (por exemplo, feito por IA).

| Campo | O que é |
|---|---|
| Id | número que identifica o resumo |
| Texto | o texto do resumo |
| NoticiaId | liga esse resumo a uma linha da tabela Noticias |

---

## 3. Como as tabelas se conectam

Pensa assim:

- Toda **notícia** precisa estar ligada a **uma categoria** e a **uma localização**.
- Uma **categoria** pode ter várias notícias (ex: várias notícias de "Esporte").
- Uma **localização** também pode ter várias notícias.
- Cada **notícia** pode ter um ou mais **resumos** ligados a ela.

É tipo assim:

```
Categoria   --->  várias Notícias
Localizacao --->  várias Notícias
Notícia     --->  vários Resumos
```

Essa ligação é feita pelos campos `CategoriaId`, `LocalizacaoId` e `NoticiaId` — eles guardam o "Id" da linha da outra tabela que estão ligados.

---

## 4. Como criar o banco do zero (passo a passo)

Sempre que alguém do time baixar o projeto pela primeira vez, precisa seguir esses passos pra ter o banco funcionando na própria máquina.

**Passo 1 — Entrar na pasta do backend**

```
cd Back-End
```

**Passo 2 — Instalar a ferramenta que roda as migrations**

```
dotnet tool restore
```

(só precisa fazer isso uma vez por computador)

**Passo 3 — Criar o banco de verdade**

```
dotnet ef database update
```

Esse comando sozinho já:
- cria o arquivo `ivnews.db`
- cria as 4 tabelas
- já coloca uns dados de exemplo dentro (pra não precisar cadastrar nada na mão)

**Passo 4 — Testar se funcionou, rodando a API**

```
dotnet run
```

Vai aparecer uma linha tipo `Now listening on: http://localhost:5022`. É só copiar esse endereço e abrir no navegador.

---

## 5. Quando eu mudar alguma coisa no banco (ex: add um campo novo)

Se você mexer em algum arquivo dentro de `Model/` (por exemplo, adicionar um campo novo na Notícia), o banco não atualiza sozinho. Você precisa avisar o sistema com esses 2 comandos:

**Passo 1 — Avisar o que mudou**

```
dotnet ef migrations add NomeCurtoDaMudanca
```

(troca "NomeCurtoDaMudanca" por algo que explique o que você fez, tipo `AddCampoAutor`)

**Passo 2 — Aplicar a mudança de verdade no banco**

```
dotnet ef database update
```

Pronto, o banco já está atualizado com a mudança.

---

## 6. Coisas importantes de lembrar

- O arquivo `ivnews.db` **não vai pro Git**. Cada um cria o seu.
- **Nunca edite na mão** os arquivos dentro da pasta `Migrations/` — eles são gerados sozinhos pelos comandos acima.
- Se você mexer nos dados usando o programa **DB Browser for SQLite**, isso fica só no seu computador — não é salvo em lugar nenhum pro time. É só pra você olhar/testar.