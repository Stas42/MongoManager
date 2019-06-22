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
        public string Database { get; set; }
        public string Name { get; set; }


        ////Get Database and Collection  
        //MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017"); 
        //IMongoDatabase db = dbClient.GetDatabase("test");
        //var collList = db.ListCollections().ToList();
        //Console.WriteLine("The list of collections are :");  
        //foreach (var item in collList)  
        //{  
        //   Console.WriteLine(item);  
        //}

        public static void Createdb()
            {

            //Console.WriteLine("The list of collections are :");
            //foreach (var item in collList)
            //{
            //    Console.WriteLine(item);
            //}

        }
    }

}
