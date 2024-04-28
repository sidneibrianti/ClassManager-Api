using ClassManager.Api.Contexts.Shared.Controllers;
using ClassManager.Domain.Contexts.Accounts.Commands;
using ClassManager.Domain.Contexts.Accounts.Entities;
using ClassManager.Domain.Contexts.Accounts.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace ClassManager.Api.Contexts.Accounts.Controllers;

[Route("users")]
public class UserController : MainController
{
  [HttpPost]
  public async Task<IResult> Create(
      [FromBody] CreateUserCommand command,
      [FromServices] CreateUserHandler handler
  )
  {
    var result = await handler.Handle(command);
    if (!result.IsSuccess)
      return Results.Json(result, statusCode: result.Status);

    if (result.Data is null)
      return Results.Json(result, statusCode: 500);

    return Results.Ok(result);
  }

  [HttpGet]
  public async Task<IResult> List(
    [FromServices] ListUsersHandler handler
  )
  {
    var result = await handler.Handle();
    if (!result.IsSuccess)
      return Results.Json(result, statusCode: result.Status);

    if (result.Data is null)
      return Results.Json(result, statusCode: 500);

    return Results.Ok(result);
  }

  [HttpPut]
  [Route("{id}")]
  public async Task<IResult> Update(
    [FromRoute] Guid id,
    [FromBody] UpdateUserCommand command,
    [FromServices] UpdateUserHandler handler
  )
  {
    var result = await handler.Handle(id, command);
    if (!result.IsSuccess)
      return Results.Json(result, statusCode: result.Status);

    if (result.Data is null)
      return Results.Json(result, statusCode: 500);

    return Results.Ok(result);
  }


  [HttpDelete]
  [Route("{id}")]
  public async Task<IResult> Delete(
    [FromRoute] Guid id,
    [FromServices] DeleteUserHandler handler
  )
  {
    var result = await handler.Handle(id);
    if (!result.IsSuccess)
      return Results.Json(result, statusCode: result.Status);

    if (result.Data is null)
      return Results.Json(result, statusCode: 500);

    return Results.Ok(result);
  }
}