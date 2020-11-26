using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;

namespace Todo_Serverless.Application.Commands
{
    public class DeleteTodoCommand : IRequest<long>
    {
        public string Id { get; set; }
        public string DatabaseName { get; set; }
        public DeleteTodoCommand(String id, string databaseName)
        {
            this.Id = id;
            this.DatabaseName = databaseName;
        }
    }
}
