using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public class UpdateCancelledTodoHandler : IRequestHandler<UpdateCancelledTodoCommand, bool>
    {
        private readonly ILogger _logger;
        private readonly ITodoAPIService _service;
        public UpdateCancelledTodoHandler(ITodoAPIService todoAPIService, ILogger<UpdateCancelledTodoHandler> log)
        {
            _service = todoAPIService;
            _logger = log;
        }

        public async Task<bool> Handle(UpdateCancelledTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call to UpdateCancelledTodo function made");
            try
            {

                using (var session = await this._service.DatabaseEngine.Client.StartSessionAsync())
                {
                    session.StartTransaction();

                    var obj = this._service.DatabaseEngine.GetTable<TodoModel>(request.DatabaseName, TableName.TODO_TABLE_NAME);

                    var model = await obj.Find<TodoModel>(session, x => x.Id == request.Id).FirstOrDefaultAsync();
                    if (model == null)
                    {
                        _logger.LogInformation("Call to UpdateCancelledTodo function was terminate because Id not found.");
                        throw new KeyNotFoundException("Id not found.");
                    }
                    model.Completed = false;
                    model.Missed = false;
                    model.Cancelled = true;
                    model.DateModified = DateTimeOffset.UtcNow;

                    var replacedItem = await obj.ReplaceOneAsync(session, x => x.Id == request.Id, model);

                    await session.CommitTransactionAsync();

                    _logger.LogInformation("Call to UpdateCancelledTodo function completed.");

                    return replacedItem != null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
