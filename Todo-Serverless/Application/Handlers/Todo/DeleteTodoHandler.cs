using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
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

    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, long>
    {
        private readonly ILogger _logger;
        private readonly ITodoAPIService _service;
        public DeleteTodoHandler(ITodoAPIService todoAPIService, ILogger<DeleteTodoHandler> log)
        {
            _service = todoAPIService;
            _logger = log;
        }

        public async Task<long> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call to DeleteTodo function made");

            using (var session = await this._service.DatabaseEngine.Client.StartSessionAsync())
            {
                session.StartTransaction();

                var user = this._service.DatabaseEngine.GetTable<TodoModel>(request.DatabaseName, TableName.TODO_TABLE_NAME);
                DeleteResult result = await user.DeleteOneAsync(session, request.Id);
                await session.CommitTransactionAsync();

                _logger.LogInformation("Call to DeleteTodo function completed.");

                return result.DeletedCount;
            }

        }
    }
}
