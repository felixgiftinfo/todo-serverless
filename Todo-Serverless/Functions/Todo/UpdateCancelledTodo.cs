
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using Todo_Serverless.Services;
using Todo_Serverless.DTOs;
using Todo_Serverless.Services.AccessTokens;
using Todo_Serverless.Application.Commands;

namespace Todo_Serverless.Functions
{
    public class UpdateCancelledTodo
    {
        private readonly ITodoAPIService _todoAPIService;
        public UpdateCancelledTodo(ITodoAPIService todoAPIService)
        {
            _todoAPIService = todoAPIService;
        }

        [FunctionName(nameof(UpdateCancelledTodo))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "cancelledTodo/{id}")] HttpRequest req, string id)
        {
            var auth = _todoAPIService.AccessTokenProvider.ValidateToken(req);
            if (auth.Status == AccessTokenStatus.Valid)
            {
                var cmd = new UpdateCancelledTodoCommand(id, auth.DatabaseName);
                var result = await _todoAPIService.Mediator.Send(cmd);

                return new OkObjectResult(result);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }


    }
}
