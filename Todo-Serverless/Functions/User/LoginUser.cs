
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
using Todo_Serverless.Models;
using Todo_Serverless.Validations;
using System.Linq;
using Todo_Serverless.Helper;
using Todo_Serverless.Services.AccessTokens;
using MediatR;

namespace Todo_Serverless.Functions.User
{
    public class LoginUser
    {

        private readonly ITodoAPIService _todoAPIService;
        public LoginUser(ITodoAPIService todoAPIService)
        {
            _todoAPIService = todoAPIService;
        }

        [FunctionName(nameof(LoginUser))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "user/login")] HttpRequest req, ILogger log)
        {
            var form = await req.GetJsonBody<LoginDTO, LoginUserValidator>();
            if (!form.IsValid)
            {
                log.LogInformation($"Invalid data.");
                return form.ToBadRequest();
            }
            var data = form.Value;

            var token = _todoAPIService.AccessTokenProvider.GenerateToken(data);
            return new OkObjectResult(token);
        }


    }
}
