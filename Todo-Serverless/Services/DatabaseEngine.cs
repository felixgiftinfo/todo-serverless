using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todo_Serverless.Services
{
    public class DatabaseEngine
    {

        public IMongoClient Client { get; }
        //  public IMongoDatabase Database { get; private set; }

        public DatabaseEngine(string connectionString)
        {
            Client = new MongoClient(connectionString);
        }

        public IMongoDatabase GetDtabase(string databaseName)
        {
            return Client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetTable<T>(string databaseName, string tableName)
        {
            var db = Client.GetDatabase(databaseName);

            return db.GetCollection<T>(tableName);
        }
    }
}
