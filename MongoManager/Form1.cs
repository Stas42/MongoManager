using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using System.Data.SqlClient;
using MongoDB.Bson.Serialization.Attributes;
using System.Drawing.Drawing2D;

//using MongoDB.Builders;


namespace MongoManager
{
    public partial class Form1 : Form
    {
        private AppConnection appConnection = new AppConnection();//initialized local variable
        List<string> dblist = new List<string>();
        
        List<string> dblistOperations = new List<string>();
        List<string> dblistDocs = new List<string>();
        List<string> collectionDocs = new List<string>();
        public Form1()

        {
            InitializeComponent();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
                        
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string connectionstring;
                pictureBox3.Visible = true;
                connectionstring = textBox1.Text;
                string strerror = null;
                bool bret = appConnection.ConnectServer(connectionstring, strerror);//
                if (bret)
                {
                    textBox3.Clear();
                    MessageBox.Show("The Client is connected to .....\n" + connectionstring.ToString(), "Mongo Manager Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    richTextBox3.Text = richTextBox3.Text + System.Environment.NewLine + "user connected at " + DateTime.Now;// The log
                    textBox3.Text = textBox3.Text + System.Environment.NewLine + "connected";

                    pictureBox2.Visible = true;

                    if (pictureBox2.Visible == true)
                    {
                        pictureBox1.Visible = false;
                        pictureBox3.Visible = false;
                    }
                }
                else
                {
                    textBox3.Clear();
                    richTextBox1.Clear();
                    MessageBox.Show("Failed to Create Client.....\n" + strerror, "Mongo Manager Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.log.Error("Failed to Create Client");
                    richTextBox3.Text = richTextBox3.Text + System.Environment.NewLine + "Connection Error at: " + DateTime.Now;// The log
                    textBox3.Text = textBox3.Text + System.Environment.NewLine + "not connected";
                    comboBox3.ResetText();
                    comboBox4.ResetText();
                    pictureBox1.Visible = true;
                }
            

                if (pictureBox1.Visible == true)
                {
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ShowDocsButton(object sender, EventArgs e)//Show Button
        {
            richTextBox1.Clear();
            string selectedDb = comboBox3.Text;
            string selectColl = comboBox4.Text;
            IMongoDatabase db = appConnection.client1.GetDatabase(selectedDb);
            IMongoCollection<BsonDocument> collVariable = db.GetCollection<BsonDocument>(selectColl);
            var resultDoc = collVariable.Find(new BsonDocument()).ToList();
            //richTextBox1.Text = "Output: \n";
            foreach (var item in resultDoc)
            {
                richTextBox1.Text = richTextBox1.Text + item + System.Environment.NewLine;
            }
         }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Create DB")
            {
                textBox4.Visible = true;
                textBox4.Text = "Enter Collection Name";
                comboBox2.Enabled = false;
                textBox6.Text = "Enter DB Name";
                textBox4.ReadOnly = false;
            }
            else 
            {
                textBox4.Visible = false;
                textBox6.Clear();
                comboBox2.Enabled = true;
                textBox6.Text = "Enter Collection Name";
                comboBox2.Text = "Choose DB";
                textBox4.ReadOnly = true;
             }
        }
        private void ComboBox2_DropDown(object sender, System.EventArgs e)
        {
            dblistOperations.Clear();
            comboBox2.DataSource = null;
            comboBox2.Text = "Choose DB";
            using (IAsyncCursor<BsonDocument> cursor = appConnection.client1.ListDatabases())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        dblistOperations.Add(Convert.ToString(doc["name"]));
                    }
                      comboBox2.DataSource = dblistOperations;
                 }
            }
        }
        private void ComboBox3_DropDown(object sender, System.EventArgs e)
        {
            try
            {
                //Clears "dblsit" list + erases Datasource--> 
                //-->That way every time the "ComboBox3_DropDown" is called it's bring the updated data
                dblist.Clear();
                comboBox3.DataSource = null;
                comboBox4.Text = "Choose Collection";

                using (IAsyncCursor<BsonDocument> cursor = appConnection.client1.ListDatabases())
                {
                    while (cursor.MoveNext())
                    {
                        foreach (var doc in cursor.Current)
                        {
                            dblist.Add(Convert.ToString(doc["name"]));
                        }
                        comboBox3.DataSource = dblist;
                        comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to Create Client.....\n" + ex.ToString(), "Mongo Manager Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ComboBox4_DropDown(object sender, System.EventArgs e)
        {
            string selected = comboBox3.Text;
            List<string> collectionList = new List<string>();
            IMongoDatabase db = appConnection.client1.GetDatabase(selected);
            var collList = db.ListCollections().ToList();
            //"The list of collections are"
            foreach (var item in collList)
            {
                collectionList.Add(Convert.ToString(item["name"]));
            }
            comboBox4.DataSource = collectionList;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
        }
            private void button2_Click_2(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Create DB")//Creates DB
            {
                string selecteddb = textBox6.Text;
                string selectedcoll = textBox4.Text;
                IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
                BsonElement add = new BsonElement("PersonFirstName", "Stas");
                BsonDocument doc = new BsonDocument();
                doc.Add(add);
                collection.InsertOne(doc);
                BsonDocument findDoc = new BsonDocument(new BsonElement("PersonFirstName", "Stas"));
                collection.FindOneAndDelete(findDoc);
            }
            else//Creates Collection
            {
                string selecteddb = comboBox2.Text;
                string selectedcoll = textBox6.Text;
                IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
                BsonElement add = new BsonElement("PersonFirstName", "Stas");
                BsonDocument doc = new BsonDocument();
                doc.Add(add);
                collection.InsertOne(doc);
                BsonDocument findDoc = new BsonDocument(new BsonElement("PersonFirstName", "Stas"));
                collection.FindOneAndDelete(findDoc);
            }
        }
            private void comboBox6_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox6.Text == "Add")
            {
                button3.Text = "Add";
                textBox5.Enabled = true;
                textBox7.Enabled = true;
                textBox9.Enabled = true;
                textBox12.Enabled = true;
                textBox11.Enabled = true;
                textBox10.Enabled = true;
                textBox8.Enabled = false;
            }
            else if (comboBox6.Text == "Delete")
            {
                button3.Text = "Delete";
                textBox5.Enabled = false;
                textBox7.Enabled = false;
                textBox9.Enabled = false;
                textBox12.Enabled= false;
                textBox11.Enabled= false;
                textBox10.Enabled= false;
                textBox8.Enabled = true;
            }
            else
            {
                button3.Text = "Update";
                textBox5.Enabled = true;
                textBox7.Enabled = true;
                textBox9.Enabled = true;
                textBox12.Enabled = true;
                textBox11.Enabled = true;
                textBox10.Enabled = true;
                textBox8.Enabled = true;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem == "Add")//Add
            {
                string selecteddb = comboBox7.Text;
                string selectedcoll = comboBox5.Text;
                string fieldName = textBox5.Text;
                string fieldValue = textBox7.Text;
                string fieldName1 = textBox10.Text;
                string fieldValue1 = textBox9.Text;
                string fieldName2 = textBox12.Text;
                int fieldValue2 = Convert.ToInt32(textBox11.Text);
                IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
                var doc = new BsonDocument
                {
                {fieldName, fieldValue},
                {fieldName1, fieldValue1},
                {fieldName2, fieldValue2}
                };
                collection.InsertOneAsync(doc);
                //collection.InsertOne(doc);
            }
            else if (comboBox6.SelectedItem == "Delete")//Delete
            {
                string selecteddb = comboBox7.Text;
                string selectedcoll = comboBox5.Text;
                string objectId = textBox8.Text;
                IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
                //delete by id
                var query = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(objectId));
                var updateDoc = collection.DeleteOne(query);
            }
            else //Update
            {
                string selecteddb = comboBox7.Text;
                string selectedcoll = comboBox5.Text;
                string fieldName = textBox5.Text;
                string fieldValue = textBox7.Text;
                string fieldName1 = textBox10.Text;
                string fieldValue1 = textBox9.Text;
                string fieldName2 = textBox12.Text;
                int fieldValue2 = Convert.ToInt32(textBox11.Text);
                string objectId = textBox8.Text;
                IMongoDatabase db = appConnection.client1.GetDatabase(selecteddb);
                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(selectedcoll);
                
                BsonDocument findDoc = new BsonDocument
                {
                {fieldName, fieldValue},
                {fieldName1, fieldValue1},
                {fieldName2, fieldValue2}
                };
                var query = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(objectId));
                var updateDoc = collection.FindOneAndReplace(query, findDoc);

                ShowDocsButton(sender, e);

            }
        }       private void ComboBox7_DropDown(object sender, System.EventArgs e)
        {
            comboBox7.Text = "Choose DB";
            using (IAsyncCursor<BsonDocument> cursor = appConnection.client1.ListDatabases())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        dblistDocs.Add(Convert.ToString(doc["name"]));
                    }

                    comboBox7.DataSource = dblistDocs;
                    comboBox7.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
                }
            }
        }
        private void ComboBox5_DropDown(object sender, System.EventArgs e)
        {
            comboBox5.Text = "Choose Collection";
            string selected = comboBox7.Text;
            IMongoDatabase db = appConnection.client1.GetDatabase(selected);
            var collList = db.ListCollections().ToList();
            //"The list of collections are"
            foreach (var item in collList)
            {
                collectionDocs.Add(Convert.ToString(item["name"]));
            }
            comboBox5.DataSource = collectionDocs;
            comboBox5.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
        }
    }
}

