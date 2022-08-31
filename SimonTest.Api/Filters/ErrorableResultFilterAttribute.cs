#pragma warning disable CS1591
namespace SimonTest.Api.Filters;

using ActionResults;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TradePlus.ResultData;
using TradePlus.ResultData.Abstract.Generics;
using TradePlus.ResultData.Extensions;

public class ActionResultFilterAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is not ExternalActionResult actionResult)
        {
            return;
        }

        if (actionResult.Result.IsSuccess)
        {
            if (actionResult.Result is IResult<object> objectResult)
            {
                context.Result = new OkObjectResult(objectResult.Data);
                return;
            }

            context.Result = new OkResult();

            return;
        }

        var errorResponse = actionResult.Result.ToErrorResponse();

        switch (actionResult.Result.Status)
        {
            case ResultStatus.InvalidArgument:
                context.Result = new BadRequestObjectResult(errorResponse);
                break;
            case ResultStatus.Forbidden:
                context.Result = new ForbidResult(new AuthenticationProperties(actionResult.Result.Errors!));
                break;
            case ResultStatus.Unauthenticated:
                context.Result = new UnauthorizedObjectResult(errorResponse);
                break;
            default:
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ActionResultFilterAttribute>>();
                logger.LogWarning("Failure result: {@Result}", actionResult.Result);
                context.Result = new BadRequestObjectResult(errorResponse);
                break;
        }
    }
}