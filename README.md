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


#Anota��es

Eu utilizei uma POC que eu j� desenvolvi algum tempo atr�s para facilitar a implementa��o.

Optei por utilizar o DDD, tendo minha entidades e regras de neg�cio do meu dom�nio.

Dentro da Application realizo valida��es de aplica��o como parametros de entrada e tamb�m mapeamentos entre entidades.

Dentro da Infra.Data est� os meus mapeamentos de banco, reposit�rio e configura��o com o Entity
Infra.CrossCutting existem algumas funcionalidades compartilhada entre toda a aplica��o, como generic repository, controle de notifica��es usando o media TR...

Infra.IoC est� com todas as inje��es de dep�ndencias.

E a API est� no projeto Todo.Challenge.Api com o Swagger.

Adicionei dois projetos tamb�m para testes.

UnitTest - Testes unit�rios

IntegratedTest = Testes integrados batendo direto nos end-point da aplica��o.



