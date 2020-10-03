
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
    public class UpdateTodo
    {
        private readonly ITodoService _service;
        public UpdateTodo(ITodoService service)
        {
            _service = service;
        }

        [FunctionName(nameof(UpdateTodo))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "put", Route = "todo/{id}")] HttpRequest req, string id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TodoDTO>(requestBody);
            var result = await this._service.UpdateTodo(data, id);
            return new OkObjectResult(result);
        }


    }
}
