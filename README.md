# Open Food Facts Challenge - Coodesh

## API Rest desenvolvida para visualizar dados do site https://world.openfoodfacts.org/ que contém informações nutricionais de alimentos do mundo todo

### Features

- [x] API Rest (Get, Post, Put)
- [x] Atualização de dados diária
- [ ] Aplicação Web para visualização dos dados

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina a seguinte ferramenta:

- Visual Studio 2022 (https://visualstudio.microsoft.com/pt-br/vs/community/)

Após fazer o download de todos arquivos deste repositório, abra o arquivo "Open Food Facts.sln".

Você precisará alterar as seguintes configurações para testar o funcionamento do sistema CRON (Atualização da base de dados diária):

1. Acessar o arquivo appsettings.json dentro da pasta Open Food Facts
  1.1 Alterar o valor da propriedade LocalDirectory para uma pasta local onde o sistema CRON fará o download dos arquivos JSON para a atualização dos dados
2. Acessar o arquivo Program.cs e alterar a linha 29, onde possui a expressão  que define o horário para execução da atualização. Para referência de como escrever o horário em expressão Cron, acesse o seguinte link: https://crontab.guru/

### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [.NET 6.0]
- [C#]
- [Visual Studio]
- [MongoDB Atlas]
- [Cronos]

### Autor
---


 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/55306962?v=4" width="100px;" alt=""/>
 Vinicius Taparosky
(https://www.linkedin.com/in/vinicius-taparosky/) 

 


