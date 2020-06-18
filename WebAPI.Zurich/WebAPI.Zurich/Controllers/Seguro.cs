using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Zurich.Models;
using WebAPI.Zurich.Repository;

namespace WebAPI.Zurich.Controllers
{
    public class SeguroController : ApiController
    {
        #region [ SEGUROS ]
        [Route("api/Seguro")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {                                       
                ISeguroRepository objRepository = new SeguroRepository();
                return Ok(objRepository.GetAll());
            }
            catch(Exception ex)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [Route("api/Seguro/{Id}")]
        [HttpGet]
        public IHttpActionResult GetbyId(int Id)
        {
            try
            {
                ISeguroRepository objRepository = new SeguroRepository();
                return Ok(objRepository.GetById(Id));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [Route("api/Seguro/CadastrarSeguro")]
        [HttpPost]
        public HttpResponseMessage CadastrarSeguro([FromBody]Seguro objSeguro)
        {
            ISeguroRepository objRepository = new SeguroRepository();
            try
            {
                if (objSeguro == null || objSeguro.SeguradoRefId == 0 || objSeguro.VeiculoRefId == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Erro: Todos os campos são obrigatórios para requisição !");
                }

                /* Verifica se o segurado e veículo não estão cadastrados, se tiver não aceitar o cadastro */
                var exite = objRepository.VerificarExisteCadastroSeguroSegurado(objSeguro);
                if (exite.Count == 0)
                {
                    IVeiculoRepository objVeiculoRepository = new VeiculoRepository();
                     var objVeiculo = objVeiculoRepository.GetById(objSeguro.VeiculoRefId);
                    /// Calcula o valor do seguro do veículo
                    Business.SeguroBo calcularSeguro = new Business.SeguroBo();
                    Seguro  objCalculoSeguro = calcularSeguro.calcularSeguro(objVeiculo[0].valor);
                    Seguro obj = new Seguro()
                    {
                        SeguradoRefId = objSeguro.SeguradoRefId,
                        VeiculoRefId = objSeguro.VeiculoRefId,
                        ValorSeguro = objSeguro.ValorSeguro,
                        TaxaRisco = objCalculoSeguro.TaxaRisco,
                        PremioRisco = objCalculoSeguro.PremioRisco,
                        PremioPuro = objCalculoSeguro.PremioPuro,
                        PremioComercial = objCalculoSeguro.PremioComercial
                    };
                    objRepository.Add(obj);
                    objRepository.Save();
                    return Request.CreateResponse(HttpStatusCode.OK, obj);
                }else
                {
                    return Request.CreateResponse("Seguro já cadastrado para esse segurado e veículo, verifique !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound + " - Exceção: " + ex);
            }
        }

        /*
        [Route("api/Seguro/CalcularMediaSeguros")]
        [HttpGet]
        public HttpResponseMessage CalcularMediaSeguros()
        {
            ISeguroRepository objRepository = new SeguroRepository();
            try
            {
                var lista = objRepository.GerarListaMediaSeguros();
                return Request.CreateResponse("Verificar porque o Linq não funciona com o calculo da média de seguros !");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound + " - Exceção: " + ex);
            }
        }
        */

        /*
        [Route("api/Seguro/AlterarSeguro")]
        [HttpPut]
        public string AlterarSeguro(int id, [FromBody]Seguro objSeguro)
        {
            return "Seguro alterado com Sucesso!";
        }
        */

            /*

        [HttpDelete]
        public string ExcluirSeguro(int id)
        {
            return "Seguro alterado com Sucesso!";
        }
        */

        #endregion 

        #region [ VEÍCULOS ]
        [Route("api/Seguro/Cadastrarveiculo")]
        [HttpPost]
        public HttpResponseMessage Cadastrarveiculo([FromBody]Veiculo objVeiculo)
        {
            IVeiculoRepository objRepository = new VeiculoRepository();
            try
            { 
                if (objVeiculo == null || objVeiculo.MarcaModelo== null || objVeiculo.valor == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Erro: Todos os campos são obrigatórios para requisição !");
                }
                Veiculo obj = new Veiculo()
                {
                    MarcaModelo = objVeiculo.MarcaModelo,
                    valor = objVeiculo.valor
                };

                objRepository.Add(obj);
                objRepository.Save();

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound + " - Exceção: " + ex);
            }
        }

        /*
        [Route("api/Seguro/AlterarVeiculo")]
        [HttpPut]
        public void AlterarVeiculo(int id, [FromBody]Veiculo objVeiculo)
        {
            IVeiculoRepository objRepository = new VeiculoRepository();
            objRepository.Update(objVeiculo);
        }
        */

        /*
        [Route("api/Seguro/ExcluirVeiculo/{Id}")]
        [HttpDelete]
        public string ExcluirVeiculo(int Id)
        {
            return "Veículo alterado com Sucesso!";
        }

        */

        [Route("api/Seguro/ConsultaVeiculo")]
        [HttpGet]
        public IHttpActionResult ConcultaVeiculo()
        {
            try
            {
                IVeiculoRepository objRepository = new VeiculoRepository();
                return Ok(objRepository.GetAll());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        #endregion 

        #region [ SEGURADO ]

        [Route("api/Seguro/CadastrarSegurado")]
        [HttpPost]
        public HttpResponseMessage CadastrarSegurado([FromBody]Segurado objSegurado)
        {
            ISeguradoRepository objRepository = new SeguradoRepository();
            try
            { 
                if (objSegurado == null || objSegurado.CPF == "" || objSegurado.Nome == "" || objSegurado.Nome == null  || objSegurado.Idade == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Erro: Todos os campos são obrigatórios para requisição !");
                }
                Segurado obj = new Segurado()
                {
                    Nome = objSegurado.Nome,
                    CPF = objSegurado.CPF,
                    Idade = objSegurado.Idade
                };
                objRepository.Add(obj);
                objRepository.Save();
                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound + " - Exceção: " + ex);
            }
        }

        [Route("api/Seguro/ConsultaSegurado")]
        [HttpGet]
        public IHttpActionResult ConsultarSegurado()
        {
            try
            {
                ISeguradoRepository objRepository = new SeguradoRepository();
                return Ok(objRepository.GetAll());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /*

        [Route("api/Seguro/AlterarSegurado")]
        [HttpPut]
        public void AlterarSegurado(int id, [FromBody]Segurado segurado)
        {
            ISeguradoRepository objRepository = new SeguradoRepository();
            objRepository.Update(segurado);

        }
        */

        /*
        [Route("api/Seguro/ExcluirSegurado")]
        [HttpDelete]
        public string ExcluirSegurado(int id)
        {
            return "Segurado alterado com Sucesso!";
        }

        */

        #endregion
    }
}
