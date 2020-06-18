# webapi
Exame Prático .NET – Web API

Instalação: Rodar o script de banco no servidor Sql Server

Rotas para testes utilizando o PostMan:

Metodo  URL                                                       Descrição
Get     localhost:xxxx/api/seguro/{idSegurado}                    Mostrará todos os seguros do segurado
Get     localhost:xxxx/api/seguro                                 Mostrará todos os seguros e seus segurados
Post    localhost:xxxx/api/seguro/Cadastrarveiculo                Utilizado para fazer cadastro de veículos
Post    localhost:xxxx/api/seguro/CadastrarSegurado               Utilizado para cadastrar segurado
Post    localhost:xxxx/api/seguro/CadastrarSeguro                 Utilizado para cadastrar seguro
Get     localhost:xxxx/api/seguro/Veiculo                         Mostrará todos veículos cadastrados


Retorno do CadastrarSeguro:
   {
        "Id": 24,
        "ValorSeguro": 20000,
        "TaxaRisco": 2.5,
        "PremioRisco": 2777777.77,
        "PremioPuro": 2861111.10,
        "PremioComercial": 3004166.66,
        "VeiculoRefId": 18,
        "Veiculo": {
            "Id": 18,
            "MarcaModelo": "Fiesta/Hatch",
            "valor": 111111111
        },
        "SeguradoRefId": 12,
        "Segurado": {
            "Id": 12,
            "Nome": "João de Oliveira",
            "CPF": 06478718890,
            "Idade": 38
        }
    }
