namespace SimonTest.Api.Controllers.Abstract;

using Microsoft.AspNetCore.Mvc;
using TradePlus.ResultData;

[ApiController]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
public abstract class ApiController : Controller
{
}