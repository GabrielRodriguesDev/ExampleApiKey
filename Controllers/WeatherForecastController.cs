using Microsoft.AspNetCore.Mvc;
using MyExampleWebApi.Attributes;

namespace MyExampleWebApi.Controllers;

[ApiController]
[Route("[controller]")]
[ApiKeyAttribute()] //! Atributo que valida a ApiKey. Como declaramos ele na controller todas as actions (chamadas),
                    //! vão utilizar esse atributo.

public class WeatherForecastController : ControllerBase
{

    public WeatherForecastController()
    {
    }

    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok(new { message = "Você tem acesso!" });
    }
}

//! Chamadas no Postman para testar
/**
 * TODO: https://localhost:7073/WeatherForecast
 * TODO: https://localhost:7073/WeatherForecast?api_key=12345
 * TODO: https://localhost:7073/WeatherForecast?api_key=gabriel_demo_ApyKey_IlTevUM/z0ey3NwCV/unWg==
*/

//! Results

/**
 * TODO: Na primeira requisição você deve receber um erro 401, pois não informamos o ApiKey. 
 * TODO: Na segunda você deve receber um erro 403 pois o ApiKey é inválido.
 * TODO: Por fim, devemos conseguir visualizar a mensagem "Você tem acesso!".
*/

