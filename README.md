# ApiUsuarios
Essa API foi desenvolvida para criação de usuários e login.

Para o desenvolvimento dessa aplicação foram utilizadas as seguintes tecnologias:
- [x] **Asp.Net Core**: Para o desenvolvimento da API.
- [x] **EntityFramework Core**: Para a conexão, consultas e inserções ao banco de dados.
- [x] **Postgres**: Banco SQL utilizado para armazenamento de dados.
- [x] **Testes unitarios**: Responsavel por testar o funcionamento das regras de código.
- [x] **Docker**: Usado para rodar a aplicação em um container.
- [x] **Docker Compose**: Rodar o banco de dados em conjunto com a aplicação.

Em conjunto com o desenvolvimento da aplicação, realizei a estruturação utilizando conceitos de DDD, Clean Architecture e SOLID.

## Rodando a aplicação

Para rodar a aplicação, é preciso possuir `Docker` e `docker-compose` instalados na sua máquina local.

[Aqui está um breve tutorial para instalação caso precise](https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-compose-on-ubuntu-20-04-pt).

Para rodar a aplicação, basta entrar na raiz do projeto (onde está localizado esse README.md) e rodar o seguinte comando:
```bash
docker-compose up
```

Aguarde até que apareça a seguinte mensagem confirmando que a aplicação está rodando:
```bash
user-api       | info: Microsoft.Hosting.Lifetime[0]
user-api       |       Now listening on: http://[::]:80
user-api       | info: Microsoft.Hosting.Lifetime[0]
user-api       |       Application started. Press Ctrl+C to shut down.
user-api       | info: Microsoft.Hosting.Lifetime[0]
user-api       |       Hosting environment: Production
user-api       | info: Microsoft.Hosting.Lifetime[0]
user-api       |       Content root path: /app
```
**OBS**: Por padrão, a rota inicializa na porta **8080**.

## Rotas
A aplicação possui algumas rotas para utilização, abaixo uma tabela com cada uma delas:

| Rota                       | Método | Descrição                                    |Body                   | 
|----------------------------|:------:|----------------------------------------------|-----------------------|
| /user                      |  GET   | ```Rota para a listagem de usuarios```       |`Sem Body`             |
| /login                     |  POST  | ```Rota para login de usuario```             |{ <br /> &nbsp;&nbsp; "email": "johndoe@gmail.com", <br />&nbsp;&nbsp; "password": "abc123" <br />}|
| /user/create               |  POST  | ```Rota para criação de usuário```           |{ <br /> &nbsp;&nbsp; "name": "John Doe", <br /> &nbsp;&nbsp; "email": "johndoe@gmail.com", <br /> &nbsp;&nbsp; "password": "abc123"<br/>}|

A aplicação possui algumas validações, elas são:
- `/login`:
  - Valida se o email está registrado no banco, caso esteja retorna o usuario, caso contrario, retorna um erro de validação.
  - Valida se a senha está correta, caso esteja retorna o usuario, caso contrario, retorna um erro de validação.
- `/user/create`:
  - Valida se o usuario existe no banco, caso não exista, retorna os dados criados, caso contrario, retorna um erro de validação.

## Exemplos de uso
<br />

`/user`

*Uso*:
```http
http://localhost:8080/user
```

*Retorno*:
```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "johndoe@gmail.com",
    "password": "689307D2FC53AF0FB941BC1BB42737CE4F3EF540",
    "created": "2021-12-27T17:36:46.933761",
    "modified": "2021-12-27T17:36:46.933761",
    "lastLogin": "2021-12-27T17:36:46.933761"
  },
  {
    "id": 2,
    "name": "Marry Any",
    "email": "marry.any@hotmail.com",
    "password": "423SD2FC53AF0FB941BC1BB42737CE4F56SG1",
    "created": "2021-12-27T17:36:46.933761",
    "modified": "2021-12-27T17:36:46.933761",
    "lastLogin": "2021-12-27T17:36:46.933761"
  },
]
```

`/login`

*Uso*:
```http
http://localhost:8080/login
```
*Body*:
```json
{
  "email": "marry.any@hotmail.com",
  "password": "anypassword",
}
```

*Retorno*:
```json
{
    "id": 1,
    "name": "John Doe",
    "email": "johndoe@gmail.com",
    "password": "689307D2FC53AF0FB941BC1BB42737CE4F3EF540",
    "created": "2021-12-27T17:36:46.933761",
    "modified": "2021-12-27T17:36:46.933761",
    "lastLogin": "2021-12-27T17:36:46.933761"
}

```
`/create/user`

*Uso*:
```http
http://localhost:8080/login
```
*Body*:
```json
{
  "user": "Marry any",
  "email": "marry.any@hotmail.com",
  "password": "anypassword",
}
```

*Retorno*:
```json
{
    "id": 2,
    "name": "Marry Any",
    "email": "marry.any@hotmail.com",
    "password": "423SD2FC53AF0FB941BC1BB42737CE4F56SG1",
    "created": "2021-12-27T17:36:46.933761",
    "modified": "2021-12-27T17:36:46.933761",
    "lastLogin": "2021-12-27T17:36:46.933761"
}
```