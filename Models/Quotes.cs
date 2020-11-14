using System;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
namespace NetCoreProj2Admin.Models
{
    public class Quotes
    {
         private MySqlConnection dbConnection;
        private MySqlCommand dbCommand;
        public Quotes() {
            defaultData();
        }
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

        //[Required(ErrorMessage="Required: Please provide the url of the quote")]
        [MaxLength(100)]
        [Display(Name="URL:")]
        [RegularExpression(@"^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&amp;%\$#\=~_\-]+))*$", ErrorMessage="Problem: Check the URL you provided and ensure it is correct")]
        public string permalink {get;set;}

        [Required(ErrorMessage="Required: Please provide the image of the author")]
        [MaxLength(100)]
        [Display(Name="Image:")]
        //[RegularExpression(@"")]
        public string image {get;set;} 

        
        public void addQuote() {
            try {
                
                dbConnection = new MySqlConnection(Connection.CONNECTION_STRING);
                dbConnection.Open();
                
                // Add to DB
                string sqlString = "INSERT INTO tblquotes (author, quote, permalink, image) " + " VALUES (?author, ?quote, ?permalink, ?image)";
                dbCommand = new MySqlCommand(sqlString, dbConnection);
                //dbCommand.Parameters.AddWithValue("?quoteID", quoteID);
                dbCommand.Parameters.AddWithValue("?author", author);
                dbCommand.Parameters.AddWithValue("?quote", quote);
                dbCommand.Parameters.AddWithValue("permalink", permalink);
                dbCommand.Parameters.AddWithValue("?image", image);

                dbCommand.ExecuteNonQuery();
                dbCommand.Parameters.Clear();

                sqlString = "SELECT @@identity";
                dbCommand.CommandText = sqlString;
                quoteID = Convert.ToInt32(dbCommand.ExecuteScalar());
            } catch(Exception e) {
                Console.WriteLine("**Error adding new quote to DB" + e);
            } finally {
               

            }
        }

         private void defaultData() {
            quoteID = 0;
            author = "";
            quote = "";
            permalink = "";
            image = "";
            
        }
    }
}
/*INSERT INTO `tblquotes` (`quoteID`, `author`, `quote`, `permalink`, `image`) VALUES
(1, 'chris mac', 'today is today', 'www.google.com', 'airliner.PNG'),
(2, 'don med', 'tonight is a night', 'www.yahoo.com', 'airliner.PNG'),
(3,'mac rom','last night last week','www.youtube.com','airliner.PNG'),
(4,'smeagul','whats a hobbit precious','www.drphil.com','airliner.PNG'),
(5,'tree beard','i hate orcs','www.drsues.com','airliner.PNG'),
(6,'soromon','join us','www.drsues.com','airliner.PNG'),
(7,'sauron','I see you','www.drsues.com','airliner.PNG'),
(8,'aragorn','sit down legolas','www.drsues.com','airliner.PNG'),
(9,'legolas','22','www.amazon.ca','airliner.PNG'),
(10,'frodo','the ring must be destroyed sam','www.drsues.com','airliner.PNG'),
(11,'gandalf','one ring ','www.drsues.com','airliner.PNG'),
(12,'gimli','lotr','www.drsues.com','airliner.PNG'),
(13,'abc','lorem empsum all day', 'www.drsues.com','airliner.PNG'),
(14,'kyle reed','There are two major products that come out of Berkeley: LSD and UNIX.  We don’t believe this to be a coincidence','www.drsues.com','airliner.PNG'),
(15,'cole cole','blue green red rgb','www.colorses.com','airliner.PNG'),
(16,'benard','steak on sundays','www.steaues.com','airliner.PNG'),
(17,'Larry DeLuca','I’ve noticed lately that the paranoid fear of computers becoming intelligent and taking over the world has almost entirely disappeared from the common culture.  Near as I can tell, this coincides with the release of MS-DOS','www.drsues.com','airliner.PNG'),
(18,'Alan Kay','Most software today is very much like an Egyptian pyramid with millions of bricks piled on top of each other, with no structural integrity, but just done by brute force and thousands of slaves','http://quotes.stormconsultancy.co.uk/quotes/38','airliner.PNG');*/