﻿using MediatR;
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

    public class GetCancelledTodoHandler : IRequestHandler<GetCancelledTodoQuery, IEnumerable<TodoReadDTO>>
    {
        private readonly ILogger _logger;
        private readonly ITodoAPIService _service;
        public GetCancelledTodoHandler(ITodoAPIService todoAPIService, ILogger<GetCancelledTodoHandler> log)
        {
            _service = todoAPIService;
            _logger = log;
        }

        public async Task<IEnumerable<TodoReadDTO>> Handle(GetCancelledTodoQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call to GetCancelledTodo function made");
            List<TodoReadDTO> models = new List<TodoReadDTO>();

            using (var session = await this._service.DatabaseEngine.Client.StartSessionAsync())
            {
                session.StartTransaction();

                var todo = this._service.DatabaseEngine.GetTable<TodoModel>(request.DatabaseName, TableName.TODO_TABLE_NAME);
                var results = await todo.Find(session, x => x.Cancelled == true).ToListAsync();
                foreach (var item in results)
                {
                    var modelDTO = this._service.Mapper.Map<TodoReadDTO>(item);
                    models.Add(modelDTO);
                }

                _logger.LogInformation("Call to GetCancelledTodo function completed.");

                return models;
            }

        }

    }
}