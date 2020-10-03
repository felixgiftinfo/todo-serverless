
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
using Todo_API.Services;
using Todo_API.DTOs;

namespace Todo_API.Functions
{
    public class GetMissedTodos
    {
        private readonly ITodoService _service;
        public GetMissedTodos(ITodoService service)
        {
            _service = service;
        }

        [FunctionName(nameof(GetMissedTodos))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "get", Route = "missedTodo")] HttpRequest req)
        {
            var results = await this._service.GetMissedTodos();
            return new OkObjectResult(results);
        }


    }
}
