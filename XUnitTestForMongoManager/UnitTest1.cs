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
        //appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);

        string newDbName = "stasdb43";//Enter new db name
        string newcollection= "StasUnitTCreateColl3";

        [Fact]  
        public void CreateDB()
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017",null);
                             
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
         public bool DatabaseExists(string database)//Checks(bool) if db exist
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
        public void CreateCollection()
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            
            UnitTest1 existingcollname = new UnitTest1();
            var result = existingcollname.CollectionExists(newcollection);
            
            if (result == true)// Collection already exists check
            {
                Fail("The Collectionn already exists");
            }
            else
            {
                CreateDB create = new CreateDB();
                create.CreateDBs(newDbName, newcollection, appConnection, "Field", "Value");
                var result1 = existingcollname.DatabaseExists(newDbName);

                Assert.True(result1);//Checks true/false saved in "result"
            }
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
