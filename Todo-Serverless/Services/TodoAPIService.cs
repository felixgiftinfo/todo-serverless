using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.Services.AccessTokens;

namespace Todo_Serverless.Services
{
    public interface ITodoAPIService
    {
        IAccessTokenProvider AccessTokenProvider { get; }
        IMediator Mediator { get; }
        DatabaseEngine DatabaseEngine { get; }
        IMapper Mapper { get; }
    }

    public class TodoAPIService : ITodoAPIService
    {
        public IMediator Mediator { get; }
        public IAccessTokenProvider AccessTokenProvider { get; }
        public DatabaseEngine DatabaseEngine { get; }
        public IMapper Mapper { get; }
        public TodoAPIService(IMediator mediator, IMapper autoMapper)
        {

            var issuerToken = Environment.GetEnvironmentVariable("IssuerToken");
            var audience = Environment.GetEnvironmentVariable("Audience");
            var issuer = Environment.GetEnvironmentVariable("Issuer");
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            this.Mapper = autoMapper;
            this.Mediator = mediator;
            this.AccessTokenProvider = new AccessTokenProvider(issuerToken, audience, issuer);
            this.DatabaseEngine = new DatabaseEngine(connectionString);
        }
    }
}
