using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetCoreProj2Admin.Models
{
    public class QuotesManager
    {
        
        private MySqlConnection dbConnection; 
        private MySqlCommand dbCommand;
        private MySqlDataReader dbReader;
        private List<Quotes> _quotes;
        public QuotesManager() {
            _quotes = new List<Quotes>();
            dbConnection = new MySqlConnection(Connection.CONNECTION_STRING);
            dbCommand = new MySqlCommand("", dbConnection); 
        }
        public List<Quotes> quotes {
            get {
                return _quotes;
            }
        }

        public void getQuotes() {
           try {

            dbConnection.Open();
            dbCommand.CommandText= "SELECT * FROM tblquotes";
            dbReader = dbCommand.ExecuteReader();
                while (dbReader.Read()) {
                    Quotes tempquotes = new Quotes();
                    tempquotes.quoteID = Convert.ToInt32(dbReader["quoteID"]);
                    tempquotes.author = dbReader["author"].ToString();
                    tempquotes.quote = dbReader["quote"].ToString();
                    tempquotes.permalink = dbReader["permalink"].ToString();
                    tempquotes.image = dbReader["image"].ToString(); 

                    _quotes.Add(tempquotes);
                    
                }
                dbReader.Close();
             } catch (Exception e) {
                Console.WriteLine(">>> An error has occured");
                Console.WriteLine(">>> " + e.Message);
            } finally {
                dbConnection.Close();
            }
        }
    }
}