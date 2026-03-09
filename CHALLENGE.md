# BNP Backend Challenge

Este projeto é a resolução do desafio técnico para desenvolvedor backend. A API foi construída utilizando **.NET**, **FastEndpoints** (padrão REPR) e **Entity Framework Core (InMemory)**, com foco em código limpo, separação de responsabilidades e resiliência contra comportamentos atípicos de bancos em memória.

## Como Executar o Projeto

1. Certifique-se de ter o **.NET SDK** instalado.
2. Clone o repositório.
3. Navegue até a pasta raiz do projeto via terminal.
4. Execute o comando:
   ```bash
   dotnet run
   ```
Acesse o Swagger UI na URL indicada no terminal (http://localhost:3000/swagger) para testar os endpoints.

## Arquitetura e Decisões Técnicas
A arquitetura do projeto foi desenhada para seguir o padrão REPR (Request-Endpoint-Response), utilizando o FastEndpoints para eliminar o inchaço dos Controllers tradicionais. Cada rota possui seu próprio ecossistema isolado (Endpoint, UseCase, Repository, Mapper, Request e Response), respeitando estritamente o princípio de Responsabilidade Única (SOLID).


## Resolução dos Challenges
**Challenge 1: Validation Challenge (Hero Description)**

*Abordagem*: Implementação de regras de validação nos contratos de POST e PUT da entidade Hero para garantir a obrigatoriedade do campo description. O sistema intercepta dados inválidos logo na entrada (padrão Fail-Fast), retornando 400 Bad Request antes mesmo de acionar os UseCases.

*Obs: Validar na borda da aplicação (Endpoint/Request) protege o domínio de dados sujos e economiza processamento, mas exige manter o contrato de validação rigorosamente alinhado com o schema do banco de dados.


**Challenge 2: Improving Challenge (HTTP 404 no PUT)**

*Abordagem*: Refatoração da lógica de atualização do Herói. A aplicação passou a realizar uma checagem prévia (busca) no repositório pelo ID fornecido. Se o registro não existir, a execução é interrompida utilizando o padrão Early Return e o endpoint devolve o status 404 Not Found.

*Obs: Executar um SELECT antes do UPDATE adiciona uma leve latência de leitura, mas é a abordagem mais sólida para APIs REST, pois previne exceções sistêmicas do Entity Framework (como DbUpdateConcurrencyException) e entrega respostas semânticas ao cliente.


**Challenge 3: Relation Challenge + Bônus (Superpoderes, Uniforme e Filtros)**

*Abordagem*: Modelagem das propriedades de relacionamento 1:N (UniformColor) e N:N (Superpowers) na entidade Hero. Como o desafio não exigia rotas de vínculo explícitas, a solução arquitetural adotada para popular esses dados e viabilizar o uso do Bônus foi implementar a injeção/sorteio automático dessas propriedades no momento da criação (POST) do herói. Com os heróis devidamente populados, o Bônus do endpoint readList foi resolvido utilizando Eager Loading (.Include()) e queries LINQ dinâmicas (.ToLower().Contains()) para permitir buscas ágeis e Case-Insensitive por nome e superpoder.

*Obs: Automatizar o vínculo no momento do cadastro simplificou o contrato da API (o cliente não precisa enviar IDs complexos de cores e poderes no Request), mas exigiu contornar as limitações de rastreamento (Tracking) do Entity Framework InMemory. Foi necessário forçar a atribuição da Chave Estrangeira (UniformColorId) e refatorar a projeção de dados nos Mappers (DTOs) para que o JSON refletisse as associações com perfeição. O uso de Eager Loading na busca é excelente para o escopo atual, embora exija paginação em cenários de dados massivos.


**Challenge 4: To-Do Challenge (CRUD de Tarefas)**

*Abordagem*: Construção de um módulo independente para tarefas (TodoItem). A lógica de negócio foi isolada nos UseCases para garantir que novas tarefas nasçam estritamente como Pending. A rota de finalização (PATCH /todos/{id}/complete) foi implementada utilizando a classe abstrata EndpointWithoutRequest para permitir atualizações de status via URL, sem a necessidade de um Payload JSON. Cobertura de testes unitários foi adicionada para casos de sucesso e de erro (ex: editar tarefa inexistente).

*Obs: Adotar um PATCH sem Request Body pode gerar o erro HTTP 415 em frameworks restritos. Adotar o EndpointWithoutRequest contornou essa falha estrutural com elegância, resultando em uma rota extremamente limpa, embora exija conhecimento profundo das engrenagens do FastEndpoints.


**Challenge 5: Refactor Challenge (Clean Code na classe Calculator)**

*Abordagem*: O método monolítico Calc() da classe Calculator foi reestruturado seguindo princípios rígidos de Clean Code. Utilizou-se o padrão de Extração (Extraction) para quebrar a lógica em submétodos focados manipulando o estado via ref para evitar retornos complexos. Além disso, a complexidade ciclomática e os aninhamentos (Nested Ifs) foram eliminados utilizando o padrão Early Return (Guard Clauses), tornando a função completamente linear (flat) e de fácil manutenção, sem quebrar os testes unitários fornecidos.

*Obs: A passagem de parâmetros por referência (ref) pode ser vista como menos funcional do que retornar Tuplas, mas neste contexto matemático específico, evitou a criação de objetos desnecessários na memória e manteve a legibilidade solicitada pelo desafio.