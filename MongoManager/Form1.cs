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
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
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
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Create DB")
            {
                UpdateTextBoxes1(true, false, "Enter DB Name", "Enter Collection Name",false);
            }
            else 
            {
                UpdateTextBoxes1(false, true, "Enter Collection Name", "Choose DB", true);
                textBox6.Clear();
             }
        }
        private void UpdateTextBoxes1(bool b1, bool b2, string strText, string strText1, bool b3)//5 Items. The method sets fields values 
        {
            textBox4.Visible = b1;
            comboBox2.Enabled = b2;
            textBox6.Text = strText;//"Enter Collection Name";
            if (comboBox1.SelectedItem == "Create DB")
            {
                textBox4.Text = strText1;
            }
            else
            {
                comboBox2.Text = strText1;//"Choose DB";
            }
            textBox4.ReadOnly = b3;
        }
        private void ComboBox2_DropDown(object sender, System.EventArgs e)//DB Operations -->Choose DB 
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
        private void ComboBox3_DropDown(object sender, System.EventArgs e)//Connections-->Choose DB drop
        {
            try
            {
                //Clears "dblsit" list + erases Datasource--> 
                //-->That way every time the "ComboBox3_DropDown" is called it brings the updated data
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
        private void ComboBox4_DropDown(object sender, System.EventArgs e)//Connections-->Choose Collection
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
                CreateDB dbs = new CreateDB();
                dbs.CreateDBs(textBox6.Text, textBox4.Text, appConnection, "PersonFirstName", "Stas");
            }
            else//Creates Collection
            {
                CreateDB coll = new CreateDB();
                coll.CreateDBs(comboBox2.Text, textBox6.Text, appConnection, "PersonFirstName", "Stas");
            }
        }
        private void comboBox6_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox6.Text == "Add")
            {
                UpdateTextBoxes("Add", true, true, true, true, true, true,false);
            }
            else if (comboBox6.Text == "Delete")
            {
                UpdateTextBoxes("Delete", false, false, false, false, false, false, true);
            }
            else
            {
                UpdateTextBoxes("Update", true, true, true, true, true, true, true);
            }
        }
        private void UpdateTextBoxes(String strText, 
            bool b1, bool b2, bool b3, bool b4, bool b5, bool b6, bool b7)//8 Items. The method sets fields values 
        {
            button3.Text = strText;
            textBox5.Enabled = b1;
            textBox7.Enabled = b2;
            textBox9.Enabled = b3;
            textBox12.Enabled = b4;
            textBox11.Enabled = b5;
            textBox10.Enabled = b6;
            textBox8.Enabled = b7;
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem == "Add")//Add
            {
                AddDoc doc = new AddDoc();
                doc.AddDocs
                    (
                    comboBox7.Text,
                    comboBox5.Text,
                    textBox5.Text,
                    textBox7.Text,
                    textBox10.Text,
                    textBox9.Text,
                    textBox12.Text,
                    Convert.ToInt32(textBox11.Text),
                    appConnection
                    );
                ShowDocsButton(sender, e);
            }
            else if (comboBox6.SelectedItem == "Delete")//Delete
            {
                DeleteDoc del = new DeleteDoc();
                del.DeleteDocs(comboBox7.Text, comboBox5.Text,textBox8.Text,appConnection);
                ShowDocsButton(sender, e);
            }
            else //Update
            {
                UpdateDoc udoc = new UpdateDoc();
                udoc.UpdateDocs
                    (
                comboBox7.Text,
                comboBox5.Text,
                textBox5.Text,
                textBox7.Text,
                textBox10.Text,
                textBox9.Text,
                textBox12.Text,
                Convert.ToInt32(textBox11.Text),
                textBox8.Text,
                appConnection
                    );          
                ShowDocsButton(sender, e);
            }
        }
        private void ComboBox7_DropDown(object sender, System.EventArgs e)//Docs Operations-->Choose DB
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
        private void ComboBox5_DropDown(object sender, System.EventArgs e)//Docs Operations-->Choose Collection
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

