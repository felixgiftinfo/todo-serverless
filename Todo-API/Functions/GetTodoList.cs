
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
    public class GetTodoList
    {
        private readonly ITodoService _service;
        public GetTodoList(ITodoService service)
        {
            _service = service;
        }

        [FunctionName(nameof(GetTodoList))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.User,  "get", Route = "todo")] HttpRequest req)
        {
            var results = await this._service.GetTodoList();
            return new OkObjectResult(results);
        }


    }
}
