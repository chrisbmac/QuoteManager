using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace NetCoreProj2.Models
{
    public class Quotes
    {
        // sql connections/ commands / list for data
        private MySqlConnection dbConnection; 
        private MySqlCommand dbCommand;
        private MySqlDataReader dbReader;

        private List<DBQuotes> _quotes;
        private List<string> _myString;
        
        public Quotes(){
            _quotes = new List<DBQuotes>();
            dbConnection = new MySqlConnection(Connection.CONNECTION_STRING);
            dbCommand = new MySqlCommand("", dbConnection);
            _myString = new List<string>();
            
        }
        // gets/sets

        // return quotes as string for displaying
        public List<string> myString{
            get {
                return _myString = quotes.Select(x => x.ToString()).ToList();
            } 
        }

        public List<DBQuotes> quotes {
            get {
                return _quotes;
            }
        }
        // get the quotes from db
        public void getQuotes() {
           try {

            dbConnection.Open();
            dbCommand.CommandText= "SELECT * FROM tblquotes";
            dbReader = dbCommand.ExecuteReader();
                while (dbReader.Read()) {
                    DBQuotes tempquotes = new DBQuotes();
                    tempquotes.quoteID = dbReader["quoteID"].ToString();
                    tempquotes.author = dbReader["author"].ToString();
                    tempquotes.quote = dbReader["quote"].ToString();
                    tempquotes.permalink = dbReader["permalink"].ToString();
                    tempquotes.image = dbReader["image"].ToString(); 

                    _quotes.Add(tempquotes);
                    
                }
                dbReader.Close();
            } catch (Exception e) {
                Console.WriteLine(">>> An error has occured with get data");
                Console.WriteLine(">>> " + e.Message);
            } finally {
                dbConnection.Close();
            }
        }
    }
}