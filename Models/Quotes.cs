using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetCoreProj2Admin.Models
{
    public class Quotes
    {
        private MySqlDataReader dbReader;
        private MySqlConnection dbConnection;
        private MySqlCommand dbCommand;
        
        private int _existingFileID;
        private List<SelectListItem> _sltListItem_ForDel;
        private List<Quotes> _myquotes;

        private string _deletionFB;
        private string _additionFB;
        private string _imageForDeletion;
        private string _newImageName;
         //Construct db Con/ com and lists
        public Quotes() {
            _sltListItem_ForDel = new List<SelectListItem>();
           _myquotes = new List<Quotes>();
            dbConnection = new MySqlConnection(Connection.CONNECTION_STRING);
            dbCommand = new MySqlCommand("", dbConnection);

            defaultData();
            getSelLstItms();
            
        }

        // Gets and Sets with Sanitation for form validation
        [Key]
        public int quoteID {get;set;}

        [Required(ErrorMessage="Required: Please provide the authors name")]
        [MaxLength(100)]
        [Display(Name="Author:")]
        
        public string author {get;set;} 

        [Required(ErrorMessage="Required: Please give a quote")]
        [MaxLength(300)]
        [Display(Name="Quote:")]
        
        public string quote {get;set;}

        [MaxLength(100)]
        [Display(Name="URL:")]
        //[RegularExpression(@"^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&amp;%\$#\=~_\-]+))*$", ErrorMessage="Problem: Check the URL you provided and ensure it is correct")]
        public string permalink {get;set;}

        [MaxLength(100)]
        [Display(Name="Image:")]
        public string image {get;set;} 
        
        // feedbacks
        public string deletionFB {
            get {
                return _deletionFB;
            }
        }
        public string additionFB {
            get {
                return _additionFB;
            }
        }
        // For existing image file name
        public int existingFileID {
            get {
                return _existingFileID;
            }
        }

        public string newImageName {
            get {
                return _newImageName;
            }
        }

        // List of quotes not being used
        public List<Quotes> myquotes {
            get {
                return _myquotes;
            }
        }

        public string imageForDeletion {
            get {
                return _imageForDeletion;
            }
        }

        //For Index View to display Select List items for deletion
        public List<SelectListItem> sltListItem_ForDel {
            get {
                return _sltListItem_ForDel;
            }
        }

        // Getting List item for view from db
        private void getSelLstItms() {
            try {
                // Clear list and populate fresh one
                _sltListItem_ForDel.Clear();
                dbConnection.Open();
                dbCommand.CommandText = "SELECT * FROM tblquotes";
                dbReader = dbCommand.ExecuteReader();
                
                while (dbReader.Read()) {
                    SelectListItem listItems = new SelectListItem();
                    //listItems.Text = Convert.ToString(dbReader["quote"]);
                    string trunc = Convert.ToString(dbReader["quote"]);
                    trunc = trunc.Length <= 100 ? trunc : trunc.Substring(0, 100) + "...";
                    //listItems.Text = trunc.Length <= 50 ? trunc : trunc.Substring(0, 50) + "...";
                    listItems.Text = trunc;
                    listItems.Value = Convert.ToString(dbReader["quoteID"]);
                    _sltListItem_ForDel.Add(listItems);

                    //Quote ID should not be zero
                    if (quoteID == 0) {
                        quoteID = Convert.ToInt32(dbReader["quoteID"]);
                    }
                }

                dbReader.Close();
            } catch (Exception e) {
                Console.WriteLine(">>> Problem getting Select List Items:" + e);
            } finally {
                dbConnection.Close();
            }
     
        }
        // check for duplicate file image name
        public string duplicateFile(string filename){
            _newImageName = "";
            try {
                
                dbConnection.Open();
                string sqlString = "SELECT image FROM tblquotes";
                dbCommand = new MySqlCommand(sqlString, dbConnection);
                dbCommand.CommandText = sqlString;
                dbReader = dbCommand.ExecuteReader();
                
                List<string> existingList = new List<string>();;
                while (dbReader.Read()) {
                    //existingList = new List<string>();
                    existingList.Add(Convert.ToString(dbReader["image"]));
                    
                }
                dbReader.Close();

                if (existingList.Contains(filename)) {
                    int rnd = new Random().Next(1, 1000);
                    sqlString = "SELECT COUNT(*) FROM tblquotes";
                    dbCommand = new MySqlCommand(sqlString, dbConnection);
                    dbCommand.CommandText = sqlString;
                    int newnum = Convert.ToInt32(dbCommand.ExecuteScalar()) + rnd;
                    _newImageName = "(" + newnum.ToString() +")" + filename;
                } else {
                    _newImageName = filename;
                }
                
                dbConnection.Close();

            } catch(Exception e) {
                Console.WriteLine("**Error in duplicateFile method" + e);
            } finally {
               dbConnection.Close();
                
            }
            Console.WriteLine("new image name DPFILE" + _newImageName);
            return _newImageName;
        }
        // add quotes to db
        public void addQuote() {
            image = newImageName;
            Console.WriteLine("THIS IS NEW FROM ADDQUOTe" + image + newImageName);
            try {
                
                dbConnection.Open();
                
                // Add to DB
                string sqlString = "INSERT INTO tblquotes (author, quote, permalink, image) " + " VALUES (?author, ?quote, ?permalink, ?image)";
                dbCommand = new MySqlCommand(sqlString, dbConnection);
                dbCommand.Parameters.AddWithValue("?author", author);
                dbCommand.Parameters.AddWithValue("?quote", quote);
                dbCommand.Parameters.AddWithValue("permalink", permalink);
                
                dbCommand.Parameters.AddWithValue("?image", image);

                dbCommand.ExecuteNonQuery();
                dbCommand.Parameters.Clear();

                sqlString = "SELECT @@identity";
                dbCommand.CommandText = sqlString;
                quoteID = Convert.ToInt32(dbCommand.ExecuteScalar());
                dbConnection.Close();
            } catch(Exception e) {
                Console.WriteLine("**Error adding new quote to DB" + e);
                _additionFB = "Quote was not added to Database.";
            } finally {
               dbConnection.Close();
            }
            _additionFB = "Quote was Succesfully added to Database";
        }
        
    // deletion of quotes from db
        public bool deleteQuote() {
            // if id is zero no quote to delete
            if(quoteID == 0) {
                // feed back for quote
                _deletionFB = "Deletion of Quote was Unsuccessful";
                return false;
            } else {
                try {
                    Console.WriteLine("DELETEING" + quoteID);
                    dbConnection.Open();
                    string sqlString = "SELECT image FROM tblquotes WHERE quoteID = ?quoteID";
                    dbCommand.Parameters.AddWithValue("?quoteID", quoteID);
                    dbCommand.CommandText = sqlString;
                    _imageForDeletion = dbCommand.ExecuteScalar().ToString();
                    
                    sqlString = "DELETE FROM tblquotes WHERE quoteID = ?quoteID";
                    dbCommand = new MySqlCommand(sqlString, dbConnection);
                    dbCommand.Parameters.AddWithValue("?quoteID", quoteID);
                    dbCommand.ExecuteNonQuery();
                    
                    dbCommand.Parameters.Clear();
                    //set default data
                    defaultData();
                    
                } catch (Exception e) {
                    Console.WriteLine(">> Error deleting quote" + e);
                    _deletionFB = "Deletion of Quote was Unsuccessful";
                    return false;
                } finally {
                    dbConnection.Close();
                }
                _deletionFB = "Deletion of Quote was successful";
                return true;
            }
        }
        // Default data for get/sets/data for db till quotes are filled with values
         private void defaultData() {
            quoteID = 0;
            author = "";
            quote = "";
            permalink = "";
            image = "";
            
        }
    }
}
