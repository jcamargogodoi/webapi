
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Zurich.Atributtes
{
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {

            var status = ((System.Web.Http.HttpResponseException)context.Exception).Response.StatusCode;
            var mensagem = ((System.Web.Http.HttpResponseException)context.Exception).Response.Content.ReadAsStringAsync();
            throw new HttpResponseException(
             context.Request.CreateErrorResponse(status, mensagem.Result.ToString()));

        }


        public class ValidateModelAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(HttpActionContext actionContext)
            {

                if (actionContext.ActionArguments.Any(kv => kv.Value == null))
                {
                    var modelRequest = string.Join(" ", actionContext.ActionArguments.Keys.ToArray());

                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("{0} Campos obrigatorios", modelRequest));
                }

                if (actionContext.ModelState.IsValid == false)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, actionContext.ModelState);
                }
            }
        }
    }
}