﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Zurich.Models;
using WebAPI.Zurich.Repository;
using NSwag.Annotations;
using Swashbuckle.Swagger.Annotations;
using WebAPI.Zurich.Atributtes;
using static WebAPI.Zurich.Atributtes.ExceptionAttribute;

namespace WebAPI.Zurich.Controllers
{
    [ExceptionAttribute]
    [ValidateModelAttribute]
    public class SeguroController : ApiController
    {
        #region [ VEÍCULOS ]
        [Route("api/seguro/cadastrarveiculo")]
        [HttpPost]
        public HttpResponseMessage Cadastrarveiculo([FromBody]VeiculoDtos objVeiculo)
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

        [Route("api/seguro/alterarveiculo")]
        [HttpPut]
        public HttpResponseMessage AlterarVeiculo(int id, [FromBody]Veiculo veiculo)
        {
            try
            {
                IVeiculoRepository obj = new VeiculoRepository();
                var exite = obj.GetById(veiculo.Id);
                if (exite.Count != 0)
                {
                    /// Caso exista seguro para o veículo, a alteração não será efetivada
                    Seguro objSeguro = new Seguro() { VeiculoRefId = veiculo.Id };
                    ISeguroRepository objSeguroRepository = new SeguroRepository();
                    var exiteSegu = objSeguroRepository.VerificarExisteSeguroParaVeiculo(objSeguro);
                    if (exiteSegu.Count == 0)
                    {
                        IVeiculoRepository objRepository = new VeiculoRepository();
                        objRepository.Update(veiculo);
                        objRepository.Save();
                        return Request.CreateResponse(HttpStatusCode.OK, "Veículo " + veiculo.MarcaModelo + " foi alterado com sucesso, verifique !");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Ambiguous, "O Veículo " + veiculo.MarcaModelo + " não pode ser alterado porque possui seguro calculado, verifique !");
                    }
                } else {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Veículo não enccontrado!, verifique !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, "Veículo " + veiculo.MarcaModelo + " não foi alterado, ocorreu algum erro, verifique !, "+ ex.InnerException);
            }
        }

        /*
        [Route("api/Seguro/ExcluirVeiculo/{Id}")]
        [HttpDelete]
        public string ExcluirVeiculo(int Id)
        {
            return "Veículo alterado com Sucesso!";
        }

        */

        [Route("api/seguro/consultarveiculo")]
        [HttpGet]
        public IHttpActionResult ConSultaVeiculo()
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
        [Route("api/seguro/cadastrarsegurado")]
        [HttpPost]
        public HttpResponseMessage CadastrarSegurado([FromBody]SeguradoDtos objSegurado)
        {
            ISeguradoRepository objRepository = new SeguradoRepository();
            try
            {
                Business.ValidaCPF Valida = new Business.ValidaCPF();
                if(!Valida.ValidarCPF(objSegurado.CPF))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "CPF inválido, verifique !");
                Business.ValidacaoGeral Validacao = new Business.ValidacaoGeral();
                if(!Validacao.ValidaCamposSeguro(objSegurado))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Erro: Todos os campos são obrigatórios para requisição !");
                if(!Validacao.ValidaIdade(objSegurado.Idade))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Idade não permitida para cadastro de segurado, idade de 18 a 103 anos !");
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

        [Route("api/seguro/consultarsegurado")]
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
                return BadRequest(ex.InnerException.Message);
            }
        }

        [Route("api/seguro/alterarsegurado")]
        [HttpPut]
        public HttpResponseMessage AlterarSegurado(int id, [FromBody]Segurado segurado)
        {
            try
            {
                ISeguradoRepository obj = new SeguradoRepository();
                var exite = obj.VerificarExisteSegurado(segurado);
                if (exite.Count != 0)
                {
                    ISeguradoRepository objRepository = new SeguradoRepository();
                    objRepository.Update(segurado);
                    objRepository.Save();
                    return Request.CreateResponse(HttpStatusCode.OK, "O segurado " + segurado.Nome + " foi alterado com sucesso verifique !");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Segurado não enccontrado!, verifique !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, "O segurado " + segurado.Nome + " não foi alterado, ocorreu algum erro, verifique !"+ ex.InnerException);
            }
        }

        [Route("api/seguro/excluirsegurado")]
        [HttpDelete]
        public HttpResponseMessage ExcluirSegurado(int Id)
        {
            try
            {   // verifica se o segurado não tem seguro, se não tiver exclui
                ISeguroRepository _objSeguroRepository = new SeguroRepository();
                Seguro objSeguro = new Seguro() { SeguradoRefId = Id };
                var exite = _objSeguroRepository.VerificarExisteSeguroParaSegurado(objSeguro);
                if (exite.Count == 0)
                {
                    Segurado objSegurado = new Segurado() { Id = Id };
                    ISeguradoRepository objSeguradoRepository = new SeguradoRepository();
                    var exiteSeg = objSeguradoRepository.VerificarExisteSegurado(objSegurado);
                    if (exiteSeg.Count != 0)
                    {
                        objSeguradoRepository.Delete(Id);
                        objSeguradoRepository.Save();
                        return Request.CreateResponse(HttpStatusCode.OK, "O segurado excluído com sucesso verifique !");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Segurado não cadastrado, verifique !");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Esse segurado possui seguro nao pode ser excluído, verifique !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, "Esse segurado não foi excluído, ocorreu algum erro, verifique !");
            }
        }
        #endregion

        #region [ SEGUROS ]
        [Route("api/seguro/calcularseguro")]
        [HttpPost]
        public HttpResponseMessage CadastrarSeguro([FromBody]SeguroDtos objSeg)
        {
            ISeguroRepository objRepository = new SeguroRepository();
            try
            {
                Seguro objSeguro = new Seguro()
                {
                    SeguradoRefId = objSeg.SeguradoRefId,
                    VeiculoRefId = objSeg.VeiculoRefId
                };
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
                    Seguro objCalculoSeguro = calcularSeguro.calcularSeguro(objVeiculo[0].valor);
                    Seguro obj = new Seguro()
                    {
                        SeguradoRefId = objSeguro.SeguradoRefId,
                        VeiculoRefId = objSeguro.VeiculoRefId,
                        ValorSeguro = objCalculoSeguro.PremioComercial,
                        TaxaRisco = objCalculoSeguro.TaxaRisco,
                        PremioRisco = objCalculoSeguro.PremioRisco,
                        PremioPuro = objCalculoSeguro.PremioPuro,
                        PremioComercial = objCalculoSeguro.PremioComercial
                    };
                    objRepository.Add(obj);
                    objRepository.Save();
                    return Request.CreateResponse(HttpStatusCode.OK, obj);
                }
                else
                {
                    return Request.CreateResponse("Seguro já cadastrado para esse segurado e veículo, verifique !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound + " - Exceção: " + ex);
            }
        }

        [Route("api/seguro")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                ISeguroRepository objRepository = new SeguroRepository();
                return Ok(objRepository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [Route("api/seguro/consultarsegurosegurado/{Id}")]
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

        [Route("api/seguro/excluirseguro/{Id}")]
        [HttpDelete]
        public HttpResponseMessage ExcluirSeguro(int Id)
        {
            try
            {
                ISeguroRepository _objSeguroRepository = new SeguroRepository();
                /// Verifica se tem seguro cadastrado
                var exite = _objSeguroRepository.VerificarExisteSeguro(Id);
                if (exite.Count != 0)
                {
                    ISeguroRepository _objRepository = new SeguroRepository();
                    _objRepository.Delete(Id);
                    _objRepository.Save();
                    return Request.CreateResponse(HttpStatusCode.OK, "O seguro foi excluído com sucesso verifique !");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Seguro não cadastrado, verifique !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, "O Seguro não foi excluído, ocorreu algum erro, verifique !, "+ ex.InnerException);
            }
        }
        
        [Route("api/seguro/relatoriomedia")]
        [HttpGet]
        public HttpResponseMessage CalcularMediaSeguros()
        {
            ISeguroRepository objRepository = new SeguroRepository();
            try
            {
                Array lista = objRepository.GerarListaMediaSeguros();
                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound + " - Exceção: " + ex);
            }
        }
        #endregion
    }
}
