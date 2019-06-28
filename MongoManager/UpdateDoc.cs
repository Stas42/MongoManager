using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoManager
{
    class UpdateDoc
    {
        public void UpdateDocs
            (
                string strDB,
                string strColl,
                string strData1,
                string strData2,
                string strData3,
                string strData4,
                string strData5,
                int strData6,
                string strObjc,
                AppConnection appConnection
         )
        {
            string selecteddb = strDB;
            string selectedcoll = strColl;
            string fieldName = strData1;
            string fieldValue = strData2;
            string fieldName1 = strData3;
            string fieldValue1 = strData4;
            string fieldName2 = strData5;
            int fieldValue2 = strData6;
            string objectId = strObjc;

            IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
            BsonDocument findDoc = new BsonDocument
            {
            {fieldName, fieldValue},
            {fieldName1, fieldValue1},
            {fieldName2, fieldValue2}
            };
            var query = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(strObjc));
            var updateDoc = collection.FindOneAndReplace(query, findDoc);
        }
    }
}
