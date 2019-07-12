using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoManager
{
    public class DeleteDoc
    {
        public void DeleteDocs
            (
            string strDb,
            string strColl,
            string strObjc,
            AppConnection appConnection
            )
        {
            string selecteddb = strDb;
            string selectedcoll = strColl;
            string objectId = strObjc;
            IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
            //delete by id
            var query = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(objectId));
            var updateDoc = collection.DeleteOne(query);
        }
    }
}
