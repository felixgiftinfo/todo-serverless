
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
using Todo_Serverless.Application.Queries;

namespace Todo_Serverless.Functions
{
    public class GetCancelledTodos
    {
        private readonly ITodoAPIService _todoAPIService;
        public GetCancelledTodos(ITodoAPIService todoAPIService)
        {
            _todoAPIService = todoAPIService;
        }

        [FunctionName(nameof(GetCancelledTodos))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "get", Route = "getCancelledTodos")] HttpRequest req)
        {
            var auth = _todoAPIService.AccessTokenProvider.ValidateToken(req);
            if (auth.Status == AccessTokenStatus.Valid)
            {
                var cmd = new GetCancelledTodoQuery(auth.DatabaseName);
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
