
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
using Todo_Serverless.Application.Commands;

namespace Todo_Serverless.Functions.User
{
    public class RegisterUser
    {

        private readonly ITodoAPIService _todoAPIService;
        public RegisterUser(ITodoAPIService todoAPIService)
        {
            _todoAPIService = todoAPIService;
        }

        [FunctionName(nameof(RegisterUser))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "user/register")] HttpRequest req, ILogger log)
        {
            var auth = _todoAPIService.AccessTokenProvider.ValidateToken(req);
            if (auth.Status == AccessTokenStatus.Valid)
            {
                log.LogInformation($"Request received for {auth.Principal.Identity.Name}.");
                log.LogInformation($"DatanaseName : {auth.DatabaseName}");

                var form = await req.GetJsonBody<UserDTO, UserValidator>();
                if (!form.IsValid)
                {
                    log.LogInformation($"Invalid data.");
                    return form.ToBadRequest();
                }
                var data = form.Value;

                var cmd = new RegisterUserCommand(data, auth.DatabaseName);
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
