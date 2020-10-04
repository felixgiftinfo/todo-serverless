
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
    public class GetTodoById
    {
        private readonly ITodoService _service;
        public GetTodoById(ITodoService service)
        {
            _service = service;
        }

        [FunctionName(nameof(GetTodoById))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/{id}")] HttpRequest req, string id)
        {
            var result = await this._service.GetTodoById(id);
            return new OkObjectResult(result);
        }


    }
}
