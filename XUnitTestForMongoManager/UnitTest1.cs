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

        [Fact]
        public void CreateDB_1()//Verifies a DB created and exists
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            string newDbName = "stasdb55";
            string newcollection = "StasUnitTCreateColl5";

            UnitTest1 existingdbname = new UnitTest1();
            var result = existingdbname.DatabaseExists(newDbName);

            if (result == true)// DB already exists check
            {
                Fail("DB already exists");
            }
            else
            {
                CreateDB create = new CreateDB();
                create.CreateDBs(newDbName, newcollection, appConnection, "Field", "Value");
                var result1 = existingdbname.DatabaseExists(newDbName);

                Assert.True(result1);//Checks true/false saved in "result"
            }
        }
        public bool DatabaseExists(string database)//Checks(bool) if a db exist
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            var existdbList = appConnection.client1.ListDatabases().ToList().Select(db => db.GetValue("name").AsString);
            return existdbList.Contains(database);
        }
        public static void Fail(string message)
        {
            throw new Xunit.Sdk.XunitException(message);
        }

        [Fact]
        public void CreateCollection_2()//Verifies a collection created in specific db 
        {
            string newDbName1 = "stasdb55";
            string newcollection1 = "StasUnitTCreateColl6";
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);

            UnitTest1 existingcollname = new UnitTest1();
            var result = existingcollname.CollectionExists(newcollection1);

            if (result == true)// Collection already exists check
            {
                Fail("The Collectionn already exists");
            }
            else
            {
                CreateDB create = new CreateDB();
                create.CreateDBs(newDbName1, newcollection1, appConnection, "Field", "Value");
                var result1 = existingcollname.DatabaseExists(newDbName1);

                Assert.True(result1);//Checks true/false saved in "result"
            }
        }

        private bool CollectionExists(string collection)//Checks(bool) if db exist
        {
            string newDbName1 = "stasdb55";
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            IMongoDatabase db = appConnection.client1.GetDatabase(newDbName1);
            var collList = db.ListCollections().ToList().Select(coll => coll.GetValue("name").AsString);
            return collList.Contains(collection);
        }

        [Fact]
        public void AddDoc_3()// Verifies the added doc is created in a specific collection (by objectid)
                              // Flow : Add Doc--> Get its id and save it in a variable 
                              // Perform  Assert.Notnull(saved after created) ...
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);

            AddDoc addDoc = new AddDoc();
            var result = addDoc.AddDocs
                (
                "stasdb55",
                "StasUnitTCreateColl6",
                "Brand",
                "Mitsubishi4",
                "Model",
                "A3",
                "Price",
                250000,
                appConnection
                );

            Assert.NotNull(result);
        }

        [Fact]//The False result is expected. Passed if fails.
        public void DeleteDoc_4()//The "method test" tests the DeleteDoc method by its id from specific collection
                                 //Verifies it doesn't exist 
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            string db = "stasdb55";
            string coll = "StasUnitTCreateColl6";
            string id = "5d28b17813301f69d4cd33ba";

            DeleteDoc del = new DeleteDoc();
            del.DeleteDocs
                (
                    db,
                    coll,
                    id,
                    appConnection
                );
            UnitTest1 docID = new UnitTest1();
            var result = docID.SelectDoc(db,coll,id);

            Assert.False(result);
        }
        private bool SelectDoc(string db, string coll, string id) //Selects and verifies if a doc exists - Bool
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            IMongoDatabase dbs = appConnection.client1.GetDatabase(db);
            IMongoCollection<BsonDocument> collection = dbs.GetCollection<BsonDocument>(coll);
            var filter_id = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var entity = collection.Find(filter_id).FirstOrDefault();
            return (Convert.ToString(entity?["_id"])).Equals(id);//"entity?" - handles null reference exception +
                                                                 //+ makes "entity" variable to be nullble            
        }
    }
} 

