using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;

namespace Todo_Serverless.Application.Commands
{
    public class RegisterUserCommand : IRequest<UserDTO>
    {
        public UserDTO Model { get; set; }
        public string DatabaseName { get; set; }
        public RegisterUserCommand(UserDTO model, string databaseName)
        {
            this.Model = model;
            this.DatabaseName = databaseName;
        }
    }
}
