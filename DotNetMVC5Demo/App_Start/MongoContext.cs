﻿using MongoDB.Driver;
using System;
using System.Configuration;

namespace DotNetMVC5Demo.App_Start
{
    public class MongoContext
    {
        MongoClient _client;
        MongoServer _server;
        public MongoDatabase _database;
        public MongoContext()        //constructor   
        {
            
            // Reading credentials from Web.config file   
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; //CarDatabase  
            var MongoUsername = ConfigurationManager.AppSettings["MongoUsername"]; //demouser  
            var MongoPassword = ConfigurationManager.AppSettings["MongoPassword"]; //Pass@123  
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"];  //27017  
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];  //localhost  

            /*
            // Creating credentials  
            var credential = MongoCredential.CreateMongoCRCredential(MongoDatabaseName, MongoUsername, MongoPassword);

            // Creating MongoClientSettings  
            var settings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                Server = new MongoServerAddress(MongoHost)
            };*/


            string connectionString = ConfigurationManager.AppSettings["MongoConnection"];
            _client = new MongoClient(connectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase(MongoDatabaseName);
        }
    }
}