using System;
using System.Collections.Generic;
using Xunit;
using MongoManager;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace XUnitTestForMongoManager
{
    public class UnitTest1
    {
        AppConnection appConnection = new AppConnection();
        string newDbName = "stasdb35";//Enter new db name
        string newcollection= "StasUnitColl3";

        [Fact]  
        public void CreateDB()
        {
            //Vars:
            appConnection.ConnectServer("mongodb://127.0.0.1:27017",null);
            //string newDbName = "stasdb34";//Enter new db name
            //string selectedcoll = "StasUnitColl";

            //Create new db
            IMongoDatabase db = appConnection.client1.GetDatabase(newDbName);
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(newcollection);
            BsonElement add = new BsonElement("PersonFirstName", "Stas");
            BsonDocument doc = new BsonDocument();
            doc.Add(add);
            collection.InsertOne(doc);
            BsonDocument findDoc = new BsonDocument(new BsonElement("PersonFirstName", "Stas"));
            collection.FindOneAndDelete(findDoc);
                       
            UnitTest1 existingdbname = new UnitTest1();
            var result = existingdbname.DatabaseExists(newDbName);

            Assert.True(result);//Checks true/false saved in "result"
        }
         private bool DatabaseExists(string database)//Checks(bool) if db exist
         {  
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            var existdbList = appConnection.client1.ListDatabases().ToList().Select(db => db.GetValue("name").AsString);
            return existdbList.Contains(database);
         }

        [Fact]
        public void CreateCollection()
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            IMongoDatabase db = appConnection.client1.GetDatabase(newDbName);
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(newcollection);
            BsonElement add = new BsonElement("PersonFirstName", "Stas");
            BsonDocument doc = new BsonDocument();
            doc.Add(add);
            collection.InsertOne(doc);
            BsonDocument findDoc = new BsonDocument(new BsonElement("PersonFirstName", "Stas"));
            collection.FindOneAndDelete(findDoc);

            UnitTest1 existingcollname = new UnitTest1();
            var result = existingcollname.CollectionExists(newcollection);

            Assert.True(result);//Checks true/false saved in "result"
        }

        private bool CollectionExists(string collection)//Checks(bool) if db exist
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            IMongoDatabase db = appConnection.client1.GetDatabase(newDbName);
            var collList = db.ListCollections().ToList().Select(coll => coll.GetValue("name").AsString);
            return collList.Contains(collection);
        }

    }
}
