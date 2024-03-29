﻿using CleanArchitectureTemplate.Core.Models.Errors;

namespace CleanArchitectureTemplate.WebApp;
public abstract class ControllerController : Controller
{
    #region Results

    public Result<T> CreateResult<T>(T value, List<string> errors)
    {
        return new Result<T>(value)
        {
            Errors = errors
        };
    }
    public Result<T> CreateResult<T>(T value)
    {
        return new Result<T>(value);
    }
    public Result<T> CreateResult<T>(T value, int rowCount)
    {
        var result = new Result<T>(value)
        {
            RowCount        = rowCount
        };
        return result;
    }
    public AcceptedResult Accepted<T>(T value, List<string> errors)
    {
        return base.Accepted(CreateResult(value, errors));
    }

    protected BadRequestObjectResult BadRequest<T>(T value, List<string> errors)
    {
        return base.BadRequest(CreateResult(value, errors));
    }

    protected BadRequestObjectResult BadRequest<T>(T value, params string[] errors)
    {
        return base.BadRequest(CreateResult(value, errors.ToList()));
    }

    protected BadRequestObjectResult BadRequest(string message)
    {
        return base.BadRequest(new List<string>
            {
                message
            });
    }

    protected BadRequestObjectResult BadRequest<T>(T value, string message)
    {
        return base.BadRequest(CreateResult(value, new List<string>
            {
                message
            }));
    }

    public ObjectResult Conflict<T>(T value, List<string> errors)
    {
        return StatusCode(StatusCodes.Status409Conflict, value, errors);
    }

    public ObjectResult Conflict<T>(List<string> errors)
    {
        return StatusCode(StatusCodes.Status409Conflict, default(T), errors);
    }

    public CreatedResult Created<T>(string uri, T value, List<string> errors)
    {
        return base.Created(uri, CreateResult(value, errors));
    }

    public CreatedResult Created<T>(Uri uri, T value, List<string> errors)
    {
        return base.Created(uri, CreateResult(value, errors));
    }

    public CreatedAtActionResult CreatedAtAction<T>(string actionName, object routeValues, T value, List<string> errors)
    {
        return base.CreatedAtAction(actionName, routeValues, CreateResult(value, errors));
    }

    public CreatedAtActionResult CreatedAtAction<T>(string actionName, string controllerName, object routeValues, T value, List<string> errors)
    {
        return base.CreatedAtAction(actionName, controllerName, routeValues, CreateResult(value, errors));
    }

    public CreatedAtActionResult CreatedAtAction<T>(string actionName, T value, List<string> errors)
    {
        return base.CreatedAtAction(actionName, CreateResult(value, errors));
    }

    public CreatedAtRouteResult CreatedAtRoute<T>(string routeName, T value, List<string> errors)
    {
        return base.CreatedAtRoute(routeName, CreateResult(value, errors));
    }

    public CreatedAtRouteResult CreatedAtRoute<T>(object routeValues, T value, List<string> errors)
    {
        return base.CreatedAtRoute(routeValues, CreateResult(value, errors));
    }

    public CreatedAtRouteResult CreatedAtRoute<T>(string routeName, object routeValues, T value, List<string> errors)
    {
        return base.CreatedAtRoute(routeName, routeValues, CreateResult(value, errors));
    }

    public ObjectResult Forbidden<T>(T value, List<string> errors)
    {
        return StatusCode(403, value, errors);
    }

    public ObjectResult Forbidden<T>(List<string> errors)
    {
        return StatusCode(403, default(T), errors);
    }

    public ObjectResult InternalError<T>(T value, List<string> errors)
    {
        return StatusCode(500, value, errors);
    }

    public ObjectResult InternalError<T>(List<string> errors)
    {
        return InternalError(default(T), errors);
    }

    protected ObjectResult InternalError<T>(string message)
    {
        return InternalError(default(T), new List<string>
            {
               message
            });
    }

    public NotFoundObjectResult NotFound<T>(T value, List<string> errors)
    {
        return base.NotFound(CreateResult(value, errors));
    }

    public NotFoundObjectResult NotFound<T>(List<string> errors)
    {
        return base.NotFound(CreateResult(default(T), errors));
    }

    public NotFoundObjectResult NotFound<T>()
    {
        return base.NotFound(CreateResult(default(T), new List<string> { GetResourceNotFoundError<T>() }));
    }

    public OkObjectResult Ok<T>(T value, List<string> errors)
    {
        return base.Ok(CreateResult(value, errors));
    }
    public OkObjectResult Ok<T>(T value, int rowCount)
    {
        return base.Ok(CreateResult(value, rowCount));
    }
    public OkObjectResult Ok<T>(T value)
    {
        return base.Ok(CreateResult(value));
    }
    public ObjectResult StatusCode<T>(int statusCode, T value, List<string> errors)
    {
        return base.StatusCode(statusCode, CreateResult(value, errors));
    }

    #endregion Results

    #region Errors
    protected string GetResourceNotFoundError<T>()
    {
        return $"The resource or related resource {typeof(T).Name.Replace("Dto", string.Empty)} that was requested was not found.";
    }
    #endregion Errors
}