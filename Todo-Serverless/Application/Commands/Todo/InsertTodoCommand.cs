using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;

namespace Todo_Serverless.Application.Commands
{
    public class InsertTodoCommand : IRequest<TodoReadDTO>
    {
        public TodoDTO Model { get; set; }
        public string DatabaseName { get; set; }
        public InsertTodoCommand(TodoDTO model, string databaseName)
        {
            this.Model = model;
            this.DatabaseName = databaseName;
        }
    }
}
