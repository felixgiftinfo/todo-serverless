using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo_Serverless.Application.Commands;
using Todo_Serverless.DTOs;
using Todo_Serverless.Helper;
using Todo_Serverless.Models;
using Todo_Serverless.Services;
using Todo_Serverless.Validations;

namespace Todo_Serverless.Application.Handlers
{

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDTO>
    {
        private readonly ILogger _logger;
        private readonly ITodoAPIService _service;
        public RegisterUserHandler(ITodoAPIService todoAPIService, ILogger<RegisterUserHandler> log)
        {
            _service = todoAPIService;
            _logger = log;
        }

        public async Task<UserDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call to RegisterUser made");

            using (var session = await this._service.DatabaseEngine.Client.StartSessionAsync())
            {
                session.StartTransaction();
                var model = this._service.Mapper.Map<UserModel>(request.Model);
                model.DateCreated = DateTimeOffset.UtcNow;
                model.DateModified = DateTimeOffset.UtcNow;

                var user = this._service.DatabaseEngine.GetTable<UserModel>(request.DatabaseName, TableName.USER_TABLE_NAME);
                user.InsertOne(session, model);
                await session.CommitTransactionAsync();


                var modelDTO = this._service.Mapper.Map<UserReadDTO>(model);
                _logger.LogInformation("Call to Register User function completed.");

                return modelDTO;
            }

        }
    }
}
