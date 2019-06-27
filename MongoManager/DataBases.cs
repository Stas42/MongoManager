using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoManager
{
    public class Dbs
    {
        public Dbs()
        {

        }

        public void CreateCollection(string strDB, string strCollection,
                                     AppConnection appConnection,
                                     string  strData1,
                                     string  strData2)
        {
            string selecteddb = strDB;
            string selectedcoll = strCollection;
            IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
            BsonElement add = new BsonElement(strData1, strData2);
            BsonDocument doc = new BsonDocument();
            doc.Add(add);
            collection.InsertOne(doc);
            BsonDocument findDoc = new BsonDocument(new BsonElement(strData1, strData2));
            collection.FindOneAndDelete(findDoc);
        }
    }
        
 
}
