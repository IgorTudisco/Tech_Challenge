using CloudGame.Domain.Commom;
using Microsoft.AspNetCore.Mvc;

namespace CloudGame.API.Extensions;

public static class ActionResultExtension
{
    public static IActionResult ToActionResult<T>(this Result<T> result) where T : notnull
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Data);
        if (result.Errors.Any(s => s.Code.Contains("NotFound")))
            return new NotFoundObjectResult(result.Errors);
        return new BadRequestObjectResult(result.Errors);
    }

    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result);
        if (result.Errors.Any(s => s.Code.Contains("NotFound")))
            return new NotFoundObjectResult(result.Errors);
        return new BadRequestObjectResult(result.Errors);
    }
}


