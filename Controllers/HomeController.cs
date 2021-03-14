using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreProj2.Models;
using System.Text.Json;
namespace NetCoreProj2.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class HomeController : ControllerBase
    {   
        
        private List<string> _thequotes = new List<string>();

       public HomeController()
        {
            
        }

        // get param of how many quotes to return
        [HttpGet]
        [Route("Quotes/{parameter1}")]
        public IEnumerable<DBQuotes> Quotes(int parameter1) {
            Console.WriteLine(parameter1);
            var random = new Random();
            Quotes newQuotes = new Quotes();
            newQuotes.getQuotes();
            var returnStr = newQuotes.quotes.OrderBy(quote => random.Next()).Take(parameter1).ToList();
            return returnStr;
        }
    }
}