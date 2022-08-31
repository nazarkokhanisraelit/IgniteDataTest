namespace SimonTest.Api.Extensions;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using ActionResults;
using TradePlus.ResultData.Abstract;
using TradePlus.ResultData.Abstract.Generics;

public static class ActionResultExtensions
{
    public static IActionResult ToActionResult(this IResult result) => new ExternalActionResult(result);

    public static IActionResult ToActionResult<T>(this IResult<T> result) => new ExternalActionResult(result);

    public static IActionResult OkResult<T>(this T result) => new OkObjectResult(result);

    public static IActionResult CreatedResult<T>(this T result) => new ObjectResult(HttpStatusCode.Created);
}