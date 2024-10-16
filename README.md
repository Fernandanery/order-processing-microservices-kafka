# Order Processing - Microservices com Kafka

Este é um projeto com fins de estudo referente ao desenvolvimento de uma aplicação orientada a eventos utilizando microserviços e Apache Kafka como broker de mensagens. O projeto simula um sistema de pedidos com comunicação assíncrona entre serviços, abordando o fluxo completo de criação e entrega de pedidos.

## Tecnologias Utilizadas
- .NET 8
- C#
- ASP.NET Core
- Apache Kafka
- Entity Framework
- Redis
- SQL Server
- MediatR

## Estrutura do Projeto
- **Aplicativo Mobile Varejo:** Interface para clientes realizarem pedidos.
- **Aplicativo Mobile Entregadores:** Interface para consulta de status de entregas.
- **Serviço de Pedido:** Recebe e processa pedidos, armazenando no banco de dados e publicando mensagens no Kafka.
- **Serviço de Notificação:** Escuta mensagens do Kafka e envia notificações por e-mail, SMS ou push notifications.
- **Broker de Mensagens:** Kafka, para garantir comunicação eficiente entre serviços.
