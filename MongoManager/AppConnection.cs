using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Windows.Forms;

namespace MongoManager
{
    public class AppConnection
    {
       private  MongoClient client = null;

        public MongoClient client1
        {
            get { return client;}
            
        }

        public bool ConnectServer(string connectionstring, string strerror)
        {
            try
            {            
                 client = new MongoClient(connectionstring);    
            }
            catch (Exception ex)
            {
               strerror = ex.Message;
               return false;
            }
            return true;
            
        }
    }
}
