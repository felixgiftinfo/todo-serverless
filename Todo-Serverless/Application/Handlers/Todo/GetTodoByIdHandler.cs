using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo_Serverless.Application.Queries;
using Todo_Serverless.DTOs;
using Todo_Serverless.Helper;
using Todo_Serverless.Models;
using Todo_Serverless.Services;
using Todo_Serverless.Validations;

namespace Todo_Serverless.Application.Handlers
{

    public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdQuery, TodoReadDTO>
    {
        private readonly ILogger _logger;
        private readonly ITodoAPIService _service;
        public GetTodoByIdHandler(ITodoAPIService todoAPIService, ILogger<GetTodoHandler> log)
        {
            _service = todoAPIService;
            _logger = log;
        }

        public async Task<TodoReadDTO> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call to GetTodoById function made");
            try
            {
                using (var session = await this._service.DatabaseEngine.Client.StartSessionAsync())
                {
                    session.StartTransaction();

                    var todo = this._service.DatabaseEngine.GetTable<TodoModel>(request.DatabaseName, TableName.TODO_TABLE_NAME);
                    var result = await todo.Find(session, x => x.Id == request.Id).ToListAsync();
                    var modelDTO = this._service.Mapper.Map<TodoReadDTO>(result);

                    _logger.LogInformation("Call to GetTodoById function completed.");

                    return modelDTO;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Call to GetTodoById function was terminated.");
                throw new Exception("Call to GetTodoById function was terminated.");
            }

        }

    }
}
