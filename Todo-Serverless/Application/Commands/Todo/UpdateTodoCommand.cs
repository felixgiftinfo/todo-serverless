using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;

namespace Todo_Serverless.Application.Commands
{
    public class UpdateTodoCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public TodoDTO Model { get; set; }
        public string DatabaseName { get; set; }
        public UpdateTodoCommand(string id, TodoDTO model, string databaseName)
        {
            this.Id = id;
            this.Model = model;
            this.DatabaseName = databaseName;
        }
    }
}
