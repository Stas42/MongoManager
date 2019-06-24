using System;
using Xunit;
using MongoManager;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;

namespace XUnitTestForMongoManager
{
    public class UnitTest1
    {
        AppConnection appConnection = new AppConnection();
        string connectionstring = "mongodb://127.0.0.1:27017";
        string strerror = null;

        [Fact]  
        public void CreateDB()
        {
            appConnection.ConnectServer(connectionstring, strerror);
            string selecteddb = "StasUnitD10";
            string selectedcoll = "StasUnitColl";
            IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
            BsonElement add = new BsonElement("PersonFirstName", "Stas");
            BsonDocument doc = new BsonDocument();
            doc.Add(add);
            collection.InsertOne(doc);
            BsonDocument findDoc = new BsonDocument(new BsonElement("PersonFirstName", "Stas"));
            collection.FindOneAndDelete(findDoc);

            string dbname = "StasUnitD10";
            var dbList = appConnection.client1.ListDatabases().ToList().Select(dbn => dbn.GetValue("name").AsString);
            bool result = string.Equals(dbname, selecteddb);
            Assert.True(result);

        }
        
    }
}
