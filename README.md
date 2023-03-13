# LojaDiversidadeComuniki

Esse projeto foi desenvolvido para a Comuniki para um processo seletivo.
Optei em usar a arquitetura asp net MVC e Razor Views para o desenvolvimento desse projeto.

Este projeto é uma pequena Loja de Diversidades com funções basicas de carrinho de compras e um fluxo de comprar e checkout sem intregação com gateways ou implementações fake, é apenas para demonstrar o fluxo de compra para o cliente.

A loja tambem possui uma area administrativa para adicionar produtos, categorias e visualizar os pedidos efetuados

---



para executar o projeto voce pode tanto usar o Visual Studio e dar um Play na aplicação

quanto usar a CLI digitando o comando

```
dotnet run --project LojaDiversidadeComuniki.Application
```

acessando pela url: **https://localhost:7292/**

esta sendo enviado um arquivo .db na raiz do projeto da aplicação, é uma arquivo sqlite3 com alguns produtos, categorias e usuarios cadastrados para uso das funções de Admin

o historico de migrations também esta disponivel no projeto, foi usado o EntityFrameWork para realização do crud da aplicação

o usuario admin

user: admin@lojacomuniki.com.br

senha: Admin@2023@

---



para a criação dos usuario, foi usado um metodo de Seed que se encontra no LojaComuniki.Application/Services/SeedUserRoleInit

os menus de admistração só aparecem para usuarios com a Role Administrador, não podendo ser vista para clientes, e podendo ser visuzalizada no menu superior da aplicação caso o perfil corresponda a Role

o projeto tambem possui politicas de seguranção e trata caso alguem tente acessar o endereço diretamente pela url, retornando acesso negado

foi Usado o IdentityUser para o desenvolvimento desse projeto.
