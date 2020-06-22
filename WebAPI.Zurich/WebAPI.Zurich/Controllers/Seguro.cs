using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Zurich.Models;
using WebAPI.Zurich.Repository;
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
                Business.VeiculoBo Valida = new Business.VeiculoBo();
                if (!Valida.ValidaVeiculo(objVeiculo.MarcaModelo))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Marca/Modelo inválida, deve ter no mínimo 6 letras, favor preencher o campo !");
                if(!Valida.ValidaValor(objVeiculo.valor))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "O valor do veículo nao pode ser menor que R$ 2.000,00 !");
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
                Business.VeiculoBo Valida = new Business.VeiculoBo();
                if (!Valida.ValidaVeiculo(veiculo.MarcaModelo))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Marca/Modelo inválida, favor preencher o campo !");

                if (!Valida.ValidaValor(veiculo.valor))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "O valor do veículo não pode ser menor que R$ 2.000,00 !");

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
        public HttpResponseMessage CadastrarSegurado([FromBody]Segurado objSegurado)
        {
            ISeguradoRepository objRepository = new SeguradoRepository();
            try
            {
                Business.SeguradoBo ValidaSegurado = new Business.SeguradoBo();
                if (!ValidaSegurado.ValidaNome(objSegurado.Nome))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "O nome deve ter no mínimo 6 letras, favor preencher o campo !");

                if (!ValidaSegurado.ValidaIdade(objSegurado.Idade))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "A idade mínima para segurado deve ser de 18 a 103 anos, favor preencher o campo !");

                if(!ValidaSegurado.ValidarCPF(objSegurado.CPF))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "CPF inválido, verifique !");

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
                Business.SeguradoBo ValidaSegurado = new Business.SeguradoBo();
                if (!ValidaSegurado.ValidaNome(segurado.Nome))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "O nome deve ter no mínimo 6 letras, favor preencher o campo !");

                if (!ValidaSegurado.ValidaIdade(segurado.Idade))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "A idade mínima para segurado deve ser de 18 a 103 anos, favor preencher o campo !");

                if (!ValidaSegurado.ValidarCPF(segurado.CPF))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "CPF inválido, verifique !");


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
                        ISeguradoRepository objRepository = new SeguradoRepository();

                        objRepository.Delete(Id);
                        objRepository.Save();
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
                Business.SeguroBo ValidaSeguro = new Business.SeguroBo();
                if (!ValidaSeguro.ValidaVeiculo(objSeg))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Id do veículo inválido !");
                if (!ValidaSeguro.ValidaSeguro(objSeg))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Id do segurado inválido !");

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
        public HttpResponseMessage GetbyId(int Id)
        {
            try
            {
                SeguroDtos objSeg = new SeguroDtos();
                objSeg.SeguradoRefId = Id;
                Business.SeguroBo ValidaSeguro = new Business.SeguroBo();
                if (!ValidaSeguro.ValidaSeguro(objSeg))
                    return Request.CreateErrorResponse(HttpStatusCode.PaymentRequired, "Id do segurado inválido !");


                ISeguroRepository objRepository = new SeguroRepository();
                ///return Ok(objRepository.GetById(Id));
                return Request.CreateResponse(HttpStatusCode.OK, objRepository.GetById(Id));
            }
            catch (Exception ex)
            {
                /// return StatusCode(HttpStatusCode.NotFound);
                return Request.CreateResponse(HttpStatusCode.NotFound, "O segurado não cadastrado, ocorreu algum erro, verifique !" + ex.Message);
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
        
        [Route("api/seguro/mediaseguros")]
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
