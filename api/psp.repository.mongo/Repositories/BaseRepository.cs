using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using psp.core;
using psp.repository.mongo.Cache;

namespace psp.repository.mongo.Repositories
{
    public class BaseRepository<T>
    {
        protected String _connectionString;
        protected MongoClient _client;
        protected MongoServer _server;
        protected MongoDatabase _database;
        protected MongoCollection _collection;
        protected ICacheStorage _cache;

        public BaseRepository(string connectionString, MongoClient client, MongoServer server, MongoDatabase database, MongoCollection collection, ICacheStorage cache)
        {
            _connectionString = connectionString;
            _client = client;
            _server = server;
            _database = database;
            _collection = collection;
            _cache = cache;
        }

        public BaseRepository(string collection)
        {
            _connectionString = ConfigurationManager.AppSettings["mongoConnectionString"];
            _client = new MongoClient(_connectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase(ConfigurationManager.AppSettings["mongoDatabase"]);
            _collection = _database.GetCollection<T>(collection);
        }
    }
}
