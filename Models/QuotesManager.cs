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
            //getSelLstItms();
            getQuotes();
            
        }

        public List<Quotes> quotes {
            get {
                return _quotes;
            }
        }

        public void getQuotes() {
           try {
            _quotes.Clear();
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
                    tempquotes.myquotes.Add(tempquotes);
                    //newQuotes.myquotes.Add(tempquotes);
                }
                //tempquotes.myquotes.AddRange(quotes);
                dbReader.Close();
                dbConnection.Close();
                //tempquotes.displayIT();
             } catch (Exception e) {
                Console.WriteLine(">>> An error has occured");
                Console.WriteLine(">>> " + e.Message);
            } finally {
               
                dbConnection.Close();
            }
            //displayIT();
            
        }

        public void getSelLstItms() {
            try {

                dbConnection.Open();
                dbCommand.CommandText = "SELECT * FROM tblquotes";
                dbReader = dbCommand.ExecuteReader();

                while (dbReader.Read()) {
                    Quotes tempList = new Quotes();        
                    SelectListItem listItems = new SelectListItem();
                    listItems.Text = Convert.ToString(dbReader["quote"]);
                    listItems.Value = Convert.ToString(dbReader["quoteID"]);
                    tempList.sltListItem_ForDel.Add(listItems);
                }

                dbReader.Close();
            } catch (Exception e) {
                Console.WriteLine(">>> Problem getting Select List Items:" + e);
            } finally {
                dbConnection.Close();
            }
        }
    }
}