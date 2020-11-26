using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;

namespace Todo_Serverless.Application.Queries
{
    public class GetTodoByIdQuery : IRequest<TodoReadDTO>
    {
        public string Id { get; set; }
        public string DatabaseName { get; set; }
        public GetTodoByIdQuery( string databaseName, string id)
        {
            this.DatabaseName = databaseName;
            this.Id = id;
        }
    }
}
