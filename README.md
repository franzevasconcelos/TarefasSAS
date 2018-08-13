# TarefasSAS


### Processo de Deploy

##### Passo 1

Fazer um publish dos projetos API e View

##### Passo 2

**Instalar o IIS**

Em Recursos do Windows selecionar o recurso "Serviços de Informações da Internet". 
Necessário também selecionar as opções "ASP.NET 4.7" e "Extensibilidade .NET 4.7" (ambas ficam dentro de "Recursos de Desenvolvimento de Aplicativos")

**Configurar os sites no IIS**

É necessário criar 2 sites para os projetos API e View.
Para criar siga as seguintes instuções: 
- Em Sites clique com o botão direito na opção "Adicionar site". 
- Na opção "Nome do Site" informar "API". 
- Na opção "caminho Fisico" indicar uma pasta (Ex. C:\Inetpub\API) ou outra pasta da sua escolha
- Na opção Porta indicar um valor da sua escolha. 

*Repita o processo para criar o site View em outra porta.*

#### Passo 3
Copiar as pastas publicadas de View e API para dentro das pastas configuradas nos sites. 

#### Passo 4
Criar um banco SQLite e colocar na pasta da sua escolha (Ex. tarefas_sas.db). 
Após criar o banco será necessário executar o projeto migrador e apontar para o arquivo. 
Esse projeto é um executável que realizar as migrações das tabelas.

#### Passo 5
**Realizar duas configurações**

* No arquivo web.config mais externo do site API, procurar pela chave Banco.Arquivo e colocar o caminho do arquivo SQLite criado
* No arquivo web.config mais externo do site View, procurar pela chave Endereco.API e preencher com o endereço web do site API

API => `<add key="Banco.Arquivo" value="caminho/diretorio/tarefas_sas.db" />`

View => `<add key="Endereco.API" value="http://localhost:81/api" />`
 
 #### Passo 6
 Reiniciar os sites API  e View aatravés do Gerenciador de Serviço de Informações 9(IIS)
