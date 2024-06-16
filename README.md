# RamenGo
O RamenGo é uma API web onde usuários podem montar pedidos escolhendo entre opções de caldos e proteínas.

## ⚙️ Arquitetura do sistema
O sistema foi desenvolvido com base no DDD e é constituído por 4 camadas com responsabilidades distintas, detalhadas abaixo. Como o sistema é simples, não haveria necessidade de uma arquitetura muito robusta, entretanto optei por uma arquitetura de alta escalabilidade para demonstrar um cenário onde a aplicação vai crescer.

- 1.API: Essa camada é a responsável por lidar com as nuances do protocolo HTTP, autenticação e detalhes como mapeamento entre entidades e DTOs. Não existe lógica de negócio nessa camada, os controllers simplesmente chamam métodos dos serviços da camada de aplicação.
  - Autenticação: A autenticação do RamenGo é feita a partir de api-key, eu implementei um Middleware de autenticação que realiza a validação da chave passada no Header caso o endpoint solicitado possua o decorador de autenticação (seguindo o padrão do asp net para autenticação).
- 2.Aplicacao: A camada de aplicação é onde estão os serviços do sistema, eles são implementados com base nos objetos da camada de domínio e é neles onde está a lógica de aplicação.
- 3.Dominio: Na camada de domínio do RamenGo estão presentes as entidades de Broth, Protein e Order, assim como as interfaces de repositório usadas pelos serviços da camada de aplicação. Essas classes são o core do sistema.
- 4.Infraestrutura: Essa camada é a responsável pela infraestrutura, no RamenGo, representada pelo acesso ao banco de dados, no caso o PostgreSQL, e implementa as interfaces de repositorio definidas no domínio, vale salientar que as outras camadas são independentes da camada de infraestrutura.

- *Diagrama exemplificando a arquitetura*
  
![untitled(1)](https://github.com/Gustavo-maia-gst/RamenGo/assets/128264896/a12ad3a8-3cb9-4f2d-9b14-2b2c25909f15)

 
## 🔩 Contrato da API
- A contrato da api pode ser encontrado na página: https://gustavonogueira.duckdns.org
- No guia era pedido o endpoint /orders para a criação de pedidos, entretanto a página de teste (https://tech.redventures.com.br/) faz as requisições para o endpoint /order, devido a isso eu dupliquei o endpoint de criação para aceitar os dois formatos
- Foi gerado um problema devido aos content-type, a página de teste estava enviando content-type:text/plain, o que causava uma resposta 415 direta do asp net, foi configurado para todas as requests serem tratadas como application/json

## 📦 Deploy
- Eu fiz o deploy em uma vm no google cloud, a aplicação está rodando em um contêiner docker, e o banco também tá em um contêiner do docker. Os arquivos do docker estão disponíveis no repositório do projeto.
- A aplicação foi desenvolvida usando HTTP, entretanto era esperado HTTPS pela página de teste, para suportar o HTTPS foi configurado um servidor de proxy reverso do nginx, com um certificado SSL gerado pelo letsencrypt, redirecionando para o serviço rodando com HTTP.

## 📋 Stack de tecnologias
- ASP NET Core | Framework web usado como base
- Entity Framework Core | ORM usado como interface ao banco de dados
- PostgreSQL | Banco de dados usado
- Docker | Usado para contêinerização do aplicativo
