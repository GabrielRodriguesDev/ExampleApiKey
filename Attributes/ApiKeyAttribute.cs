using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyExampleWebApi.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)] //! Limitando o Atributo apenas para uso de classe e métodos.

    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    //! Herdamos de Attribute para utilizarmos  poderemos utilizar em forma uma notação [ApiKey] tanto na classe (Controller) quanto em um método (Action).
    {
        private const string ApiKeyName = "api_key";
        private const string SecretKeyName = "secret_key";
        private const string ApiKey = "gabriel_demo_ApyKey_IlTevUM/z0ey3NwCV/unWg==";

        private const string SecretKey = "gabriel_demo_SecretKey_IlTevUM/z0ey3NwCV/unWg==";


        //! Interceptando a requisição
        /*
        o herdar da classe Attribute e IAsyncActionFilter somos obrigados a implementar o método OnActionExecutionAsync,
        onde poderemos inspecionar a requisição atual.

        Neste caso, vamos inspecionar o context.HttpContext.Request que possui tanto a propriedade Headers quanto Query, 
        se referindo aos cabeçalhos e a URL da requisição respectivamente.

        Logo, se queremos obter um valor da URL, utilizamos context.HttpContext.Request.Query, enquanto para obter um
        valor dos cabeçalhos utilizamos context.HttpContext.Request.Headers.

        A única coisa que precisamos nos atentar é que podemos ter mais de um valor com o mesmo nome ou mesmo nenhum valor.
        Desta forma é recomendado utilizar a extensão TryGetValue para não ter exceções na execução do código.
        */

        public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Query.TryGetValue(ApiKeyName, out var extractedApiKey))  //!Valida se encontra a chave especificada dentro de uma coleção. Se encontrar retorna true, caso não encontre retorna false;
            {
                //!ApiKey não encontrada
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey não encontrada"
                };
                return;
            }
            else
            {
                //!ApiKey inválida

                if (!ApiKey.Equals(extractedApiKey))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 403,
                        Content = "Acesso não autorizado"
                    };
                    return;
                }
            }
            //! Por fim, podemos utilizar o método next() para dar continuidade a requisição quando desejarmos.
            await next();
        }
        // ? Nosso próximo passo é informar a aplicação que estamos utilizando autenticação e autorização.
        // ? Normalmente a linha app.UseAuthentication já vem no código. -> Isso na classe Program
        // ? Obs -> app.UseAuthentication(); deve ser inserido antes de     app.UseAuthorization();
    }
}