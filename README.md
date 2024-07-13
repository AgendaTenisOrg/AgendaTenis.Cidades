# AgendaTenis.Cidades


## Sobre<a name = "sobre"></a>
AgendaTenis.Cidades é um microsserviço da aplicação AgendaTenis cujo objetivo é abstrair o uso da API externa de localidades do IBGE (https://servicodados.ibge.gov.br/api/docs/localidades)
Este serviço é constituído por uma Web Api (autenticação JWT) e uma Internal Api (autenticação via chave secreta) escritos em .NET 8.
Este serviço não possui um banco de dados próprio, ele apenas consulta os municípios do brasil na API externa de localidades e salva os dados em cache no Redis.

## Endpoints da Web Api<a name = "endpoints"></a>

### Obter Cidades
O objetivo deste endpoint é fornece a lista de cidades do brasil.

**Rota**: Api/Cidades/Obter\
**Método HTTP**: GET\
**Autenticação**: Necessita token jwt gerado em Api/Identity/GerarToken, do contrário retorna status 401 (Unauthorized)\
**Autorização**: Não tem políticas de autorização, somente autenticação é suficiente

## Endpoints da Internal Api<a name = "endpoints"></a>

### Obter Cidade por Id
O objetivo deste endpoint é retornar os dados de uma cidade a partir de seu Id

**Rota**: Api/Cidades/Obter/{Id}\
**Método HTTP**: GET\
**Autenticação**: Necessita chave secreta, do contrário retorna status 401 (Unauthorized)\
**Autorização**: Não tem políticas de autorização, somente autenticação é suficiente

## Descrição técnica<a name = "descricao_tecnica"></a>
A API de cidades realiza uma chamada rest para a API de localidades na rota https://servicodados.ibge.gov.br/api/v1/localidades/municipios e salva os dados em cache no Redis.

Segue o modelo de dados retornado pela API de cidades:
- Id: int
- Nome: string

Sobre a API externa de localidades:
- Link para documentação: https://servicodados.ibge.gov.br/api/docs/localidades#api-_
- Autenticação: Não necessita de autenticação
- O endpoint utilizado em AgendaTenis.Cidades foi https://servicodados.ibge.gov.br/api/v1/localidades/municipios
 
### Docker
- Este repositório possui 2 executáveis (AgendaTenis.Cidades.WebApi e AgendaTenis.Cidades.InternalApi), por isso criei 2 Dockerfiles:
  - O Dockerfile de AgendaTenis.Jogadores.WebApi chama-se DockerfileWebApi
  - O Dockerfile de AgendaTenis.Jogadores.InternalApi chama-se DockerfileInternalApi
- Utilize as instruções presentes na seção *Como executar* do repositório [Agte](https://github.com/AgendaTenisOrg/AgendaTenis.WebApp) para executar a stack inteira da aplicação

Observação: Se eu tivesse separado os projetos AgendaTenis.Cidades.WebApi e AgendaTenis.Cidades.InternalApi em repositórios exclusivos, teria sido necessário criar um terceiro repositório para o AgendaTenis.Cidades.Core (que contém a lógica do Core do contexto de Cidades) e publicar um pacote Nuget para ser reaproveitável entre AgendaTenis.Cidades.WebApi e AgendaTenis.Cidades.InternalApi. Isso me pareceu muito trabalho para pouco ganho, então mantive ambos os executáveis neste repositório (por isso temos 2 Dockerfiles).
