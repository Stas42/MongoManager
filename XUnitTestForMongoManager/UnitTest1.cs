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
        //appConnection.ConnectServer("mongx`odb://127.0.0.1:27017", null);

        //Create DB vars
        static string newDbName = "stasdb54";
        string newcollection = "StasUnitTCreateColl4";

        //Create Coll vars
        readonly string newDbName1 = newDbName;
        string newcollection1 = "StasUnitTCreateColl5";


        [Fact]  
        public void CreateDB_1()//Verifies a DB created and exists
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
        public void CreateCollection_2()//Verifies a collection created in specific db 
        {
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
                var result1 = existingcollname.DatabaseExists(newDbName);

                Assert.True(result1);//Checks true/false saved in "result"
            }
        }
        private bool CollectionExists(string collection)//Checks(bool) if db exist
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);
            IMongoDatabase db = appConnection.client1.GetDatabase(newDbName1);
            var collList = db.ListCollections().ToList().Select(coll => coll.GetValue("name").AsString);
            return collList.Contains(collection);
        }
       [Fact]
       public void AddDoc_3()// Verifies the added doc is created in a specific collection (by objectid)
                             // Flow : Add Doc--> Get the id --> Assert.equal(not null)...
        {
            appConnection.ConnectServer("mongodb://127.0.0.1:27017", null);

            AddDoc add = new AddDoc();//Creates doc
            add.AddDocs
                (
                "stasdb54",
                "StasUnitTCreateColl5",
                "Brand",
                "Sam",
                "Model",
                "S9",
                "Price",
                1000,
                appConnection
                );

            //return Convert.ToString(add["_id"]).ToString;

            //var result = return Convert.ToString(add["_id"]);

            //Assert.Equal(add, add);

        }

        //public string DocExist(string docId)
        //{
        //    AddDoc add = new AddDoc();//Creates doc
        //    add.AddDocs
        //        (
        //        newDbName,
        //        newcollection,
        //        "Brand",
        //        "Sam",
        //        "Model",
        //        "S9",
        //        "Price",
        //        1000,
        //        appConnection
        //        );
        //        //return docId["_id"];
        //}

    }
}

