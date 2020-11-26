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

    public class InsertTodoHandler : IRequestHandler<InsertTodoCommand, TodoReadDTO>
    {
        private readonly ILogger _logger;
        private readonly ITodoAPIService _service;
        public InsertTodoHandler(ITodoAPIService todoAPIService, ILogger<InsertTodoHandler> log)
        {
            _service = todoAPIService;
            _logger = log;
        }

        public async Task<TodoReadDTO> Handle(InsertTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call to InsertTodo function made");
            try
            {


                using (var session = await this._service.DatabaseEngine.Client.StartSessionAsync())
                {
                    session.StartTransaction();
                    var model = this._service.Mapper.Map<TodoModel>(request.Model);
                    model.Id = Guid.NewGuid().ToString();
                    model.DateCreated = DateTimeOffset.UtcNow;
                    model.DateModified = DateTimeOffset.UtcNow;

                    var user = this._service.DatabaseEngine.GetTable<TodoModel>(request.DatabaseName, TableName.TODO_TABLE_NAME);
                    user.InsertOne(session, model);
                    await session.CommitTransactionAsync();


                    var modelDTO = this._service.Mapper.Map<TodoReadDTO>(model);
                    _logger.LogInformation("Call to InsertTodo function completed.");

                    return modelDTO;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
