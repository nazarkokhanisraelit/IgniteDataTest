namespace Simon_Test.Controllers.Abstract;

using Microsoft.AspNetCore.Mvc;
using TradePlus.ResultData;

[ApiController]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
public abstract class ApiController : Controller
{
}