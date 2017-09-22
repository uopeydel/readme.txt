using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
//using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using Readme.DataAccess.MongoDB.Models;

namespace Readme.DataAccess.MongoDB
{
    public class MongoDBConnection
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; } = false;

        public IMongoDatabase _database { get; }

        public MongoDBConnection()
        {
            ConnectionString = "mongodb://localhost:27017/ReadmeMongoDemo";
            DatabaseName = MongoUrl.Create(ConnectionString).DatabaseName;
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            //IsSSL = true;
            if (IsSSL)
            {
                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
            }
            MongoClient mongoClient = new MongoClient(settings);
            _database = mongoClient.GetDatabase(DatabaseName);

            //TestConnection();
        }

        public void TestConnection()
        {
            try
            {
                var coll = _database.GetCollection<LogUsers>("LogUsers");
                var alldata = coll.AsQueryable().FirstOrDefault();
                var alldataa = alldata;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }
    }
}
