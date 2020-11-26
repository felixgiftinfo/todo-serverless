
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
using Todo_Serverless.Validations;
using Todo_Serverless.Helper;
using Todo_Serverless.Application.Commands;

namespace Todo_Serverless.Functions
{
    public class UpdateTodo
    {
        private readonly ITodoAPIService _todoAPIService;
        public UpdateTodo(ITodoAPIService todoAPIService)
        {
            _todoAPIService = todoAPIService;
        }

        [FunctionName(nameof(UpdateTodo))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "todo/{id}")] HttpRequest req, string id, ILogger log)
        {
            var auth = _todoAPIService.AccessTokenProvider.ValidateToken(req);
            if (auth.Status == AccessTokenStatus.Valid)
            {
                var form = await req.GetJsonBody<TodoDTO, TodoValidator>();
                if (!form.IsValid)
                {
                    log.LogInformation($"Invalid data.");
                    return form.ToBadRequest();
                }

                var cmd = new UpdateTodoCommand(id, form.Value, auth.DatabaseName);
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
