# Crawler - Desafio  Data Lawyer
## Desafio Crawler
- Primeiramente Clone o projeto e faça o build da solução, caso venha dar erro, dar build individualmente nas dependência;
- A `ConnectionStrings` está setada para o banco SQLite.\
Para gerar o banco de dados, basta rodar o migrations na pasca `Crawler` com os comando seguintes: \
 `dotnet ef migrations add init` \
 `dotnet ef database update` \
 Caso queria utilizar os dados ja existente na pasta migrations utilize o `update-database` 
- O arquivo `Crawler.db` será gerado na pasta que foi indicada e ja pode ser aberto no `DB Browser(SQLCipher)`.
## Criando Usuario
- Executando o projeto, o swagger irá lhe apresentar os campos de consulta dos Processos, mas para fazer isso crie um usuario para estar gerando um Token JWT;
- Com o Token gerado clique no campo onde possui o nome `Authorize` preencha o campo com `Bearer + TOKEN` ;
- Se o token estiver correto ja pode estar fazendo as demais consultas;
## Utilização de cada Verbo Rest: 
- Get: temos dois, onde um irá trazer todos os registros de Processos salvo no banco e outro uma consulta por `ID` do Processo;
- Post : Irá passar o codigo do Processo por extenso, por exemplo: `0809979-67.2015.8.05.0080`, no final é retornado o objeto preenchido caso nao tenha nenhum erro na hora de salvar;
- Put : Ele irá pergunta qual sera o `ID` do processo a ser modificado e o objeto com as devidas modificações e retornara o obeto modificado;
- Delete : Necessita apenas informar o `ID` que deseja deletar e o retorno será *Deletado* ou Status 500 InternalServerError;
