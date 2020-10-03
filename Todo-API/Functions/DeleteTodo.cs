
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
    public class DeleteTodo
    {
        private readonly ITodoService _service;
        public DeleteTodo(ITodoService service)
        {
            _service = service;
        }

        [FunctionName(nameof(DeleteTodo))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "delete", Route = "todo/{id}")] HttpRequest req, string id)
        {
            var result = await this._service.DeleteTodo( id);
            return new OkObjectResult(result);
        }


    }
}
