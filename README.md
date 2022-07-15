#Run Project Todo.Challenge

## 1. Configure DataBase

In root directory run the command:
```
docker-compose up -d
```

## 2. Run Scripts to Create DataBase and Tables

```
/Modules/Infra/Infra.Data.Migrations/Scripts/Sql/20220715_11_00_01_create_database.sql
/Modules/Infra/Infra.Data.Migrations/Scripts/Sql/20220715_11_00_02_create_table_todo_s.sql
```

## 3. Run Project 

```
Todo.Challenge.Api
```

## 4. Run Integrated Tests

```
Change connection in = appsettings.Test.json
```


#Anotações

Eu utilizei uma POC que eu já desenvolvi algum tempo atrás para facilitar a implementação.

Optei por utilizar o DDD, tendo minha entidades e regras de negócio do meu domínio.

Dentro da Application realizo validações de aplicação como parametros de entrada e também mapeamentos entre entidades.

Dentro da Infra.Data está os meus mapeamentos de banco, repositório e configuração com o Entity
Infra.CrossCutting existem algumas funcionalidades compartilhada entre toda a aplicação, como generic repository, controle de notificações usando o media TR...

Infra.IoC está com todas as injeções de depêndencias.

E a API está no projeto Todo.Challenge.Api com o Swagger.

Adicionei dois projetos também para testes.

UnitTest - Testes unitários

IntegratedTest = Testes integrados batendo direto nos end-point da aplicação.



