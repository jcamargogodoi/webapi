# webapi
Exame Prático .NET – Web API

Instalação: Rodar o script de banco no servidor Sql Server

Rotas para testes utilizando o PostMan:

Metodo  URL                                              Parametros                             Descrição
Get     localhost:xxxx/api/seguro/{idSegurado}              Id                         Mostrará todos os seguros do segurado
Get     localhost:xxxx/api/seguro                           Id                         Mostrará todos os seguros e seus segurados
Get     localhost:xxxx/api/seguro/{Id}                                                 Mostrará os seguros de um segurado
Get     localhost:xxxx/api/Seguro/ConsultaVeiculo           MarcaModelo,Valor          Mostrará todos veículos cadastrados
Post    localhost:xxxx/api/seguro/Cadastrarveiculo                                     Utilizado para fazer cadastro de veículos
Post    localhost:xxxx/api/seguro/CadastrarSegurado         Nome,CPF,Idade             Utilizado para cadastrar segurado
Post    localhost:xxxx/api/seguro/CadastrarSeguro           SeguradpRefId,VeiculoRefId Utilizado para cadastrar seguro
Get     localhost:xxxx/api/seguro/Veiculo                                              Mostrará todos veículos cadastrados

