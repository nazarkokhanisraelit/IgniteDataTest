namespace Simon_Test.Application.Response;

using Microsoft.AspNetCore.Mvc;
using TradePlus.ResultData.Abstract;

public class ExternalActionResult : ActionResult
{
    public ExternalActionResult(IBaseResult result)
    {
        Result = result;
    }

    public IBaseResult Result { get; }
}