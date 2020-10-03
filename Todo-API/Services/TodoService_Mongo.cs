using AutoMapper;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_API.DTOs;
using Todo_API.Models;

namespace Todo_API.Services
{

    public class TodoService_Mongo : ITodoService
    {
        // Using MongoDB
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMongoClient _client;
        private IMongoDatabase _database;
        private readonly IMapper _mapper;
        public TodoService_Mongo(IConfiguration configuration, ILogger<TodoService_Mongo> log, IMapper autoMapper)
        {
            _mapper = autoMapper;
            _logger = log;
            _configuration = configuration;
            var conStr = "mongodb+srv://user:password@cluster0.kdx7e.azure.mongodb.net/test?retryWrites=true&w=majority";
            _client = new MongoClient(conStr);
            this.EnsureDatabase();
        }

        public IMongoCollection<TodoModel> Todos
        {
            get
            {
                return _database.GetCollection<TodoModel>(ServiceConfiguration.CONTAINER_NAME);
            }
        }
        void EnsureDatabase()
        {
            if (_client != null)
            {
                _database = _client.GetDatabase(ServiceConfiguration.DATABASE_NAME);

                var names = _database.ListCollectionNames().ToList();
                var tables = new List<string>() { ServiceConfiguration.CONTAINER_NAME };
                foreach (var item in tables)
                {
                    if (!names.Any(x => x == item))
                    {
                        _database.CreateCollection(item);
                    }
                }
            }
        }

        public async Task<TodoReadDTO> AddTodo(TodoDTO todo)
        {
            _logger.LogInformation("Call to AddTodo function made");

            using (var session = await _client.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var model = _mapper.Map<TodoModel>(todo);

                    this.Todos.InsertOne(session, model); 
                    await session.CommitTransactionAsync();

                    var modelDTO = this._mapper.Map<TodoReadDTO>(model);
                    _logger.LogInformation("Call to AddTodo function completed.");
                    return modelDTO;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Call to AddTodo function was terminate.");
                    await session.AbortTransactionAsync();
                    throw ex;
                }
            }
        }

    }
}
