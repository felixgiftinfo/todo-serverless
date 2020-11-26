using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;

namespace Todo_Serverless.Application.Queries
{
    public class GetCancelledTodoQuery : IRequest<IEnumerable<TodoReadDTO>>
    {
        public string DatabaseName { get; set; }
        public GetCancelledTodoQuery( string databaseName)
        {
            this.DatabaseName = databaseName;
        }
    }
}
