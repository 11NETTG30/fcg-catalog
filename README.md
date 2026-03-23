# FIAP Cloud Games (FCG) - Microsserviço de Catálogo - fcg-catalog

## 📚 Sobre o Projeto

Este repositório faz parte do Tech Challenge da Pós-Graduação em Arquitetura de Sistemas .NET da FIAP, Turma 11NETT – Grupo 30.

Este é o microsserviço responsável pelo Catálogo de Jogos da plataforma FIAP Cloud Games (FCG).

Além de gerenciar os jogos (CRUD), este serviço é responsável por iniciar o fluxo de compra, publicando eventos para o microsserviço de pagamentos.

---

## 🎯 Objetivos do Tech Challenge

- Desenvolver um Microsserviço de Catálogo
- Disponibilizar dados dos jogos para outros microsserviços
- Integrar via mensageria (RabbitMQ)

---

## 🛠️ Tecnologias Utilizadas

| Categoria        | Tecnologia / Ferramenta |
|-----------------|------------------------|
| Plataforma      | .NET 10                |
| Framework Web   | ASP.NET Core           |
| Linguagem       | C# 14                  |
| ORM             | Entity Framework Core  |
| Banco de Dados  | PostgreSQL             |
| Mensageria      | RabbitMQ               |
| Documentação    | OpenAPI / Swagger      |
| Containerização | Docker / Docker Compose|

---

## 📡 Comunicação por Eventos

Este microsserviço participa do fluxo de compra utilizando mensageria (RabbitMQ).

### Eventos publicados

- OrderPlacedEvent  
  Disparado ao iniciar a compra de um jogo

### Eventos consumidos

- PaymentProcessedEvent  
  Quando aprovado, o jogo é adicionado à biblioteca do usuário

---

## 🚀 Setup Inicial

### 1. Configurar variáveis de ambiente

```bash
cp .env.example .env
```

Preencha os valores no .env.

---

### 2. Configurar token do GitHub (NuGet privado)

```bash
cp nuget_token.example.txt nuget_token.txt
```

Adicione seu token no arquivo.

---

### 3. Subir infraestrutura

```bash
docker-compose up -d
```

Verificar:

```bash
docker-compose ps
```

Logs:

```bash
docker-compose logs -f
```

Parar:

```bash
docker-compose down
```

---

### 4. Build da aplicação

```bash
docker-compose up --build -d
```

---

### 6. Acessos

- API: http://localhost:8080  
- Swagger: http://localhost:8080/swagger/index.html 
- RabbitMQ: http://localhost:15672  

---

## 🔐 Variáveis de Ambiente

```env
Postgres_User=<user>
Postgres_Password=<password>
Postgres_DB=<db>

RabbitMQ__Username=<user>
RabbitMQ__Password=<password>
RabbitMQ__Host=<host>
RabbitMQ__VirtualHost=/

ConnectionStrings__DefaultConnection=Host=<host>;Port=5432;Database=<db>;Username=<user>;Password=<password>
```

---

## 🔑 Secrets

Arquivo:

```txt
nuget_token.txt
```

Conteúdo:

```txt
SEU_TOKEN_DO_GITHUB_COLADO_NO_ARQUIVO
```

---

## 🐳 Docker

Este projeto possui um Dockerfile otimizado com multi-stage build para execução em produção.


