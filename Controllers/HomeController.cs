using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreProj2Admin.Models;
using System.Dynamic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NetCoreProj2Admin.Models;

namespace NetCoreProj2Admin.Controllers
{
    public class HomeController : Controller
    {   
        private IWebHostEnvironment environment;

        public HomeController(IWebHostEnvironment env) {
            environment = env;
        }

        public IActionResult Index()
        {
            QuotesManager myquotesMAN = new QuotesManager();
            Quotes myquotes = new Quotes();
            /*ViewBag.Message = "Welcome";
            dynamic myModels =  new ExpandoObject();
            myModels.myquotesMAN = QuotesManager.quotes();*/
            ViewData["feedBack"] = ImageUploader.feedBack;
            return View();
        }
        [HttpPost]
        public IActionResult AddQuoteContr(Quotes newQuotes, IFormFile selectedFile) {
            
            if (!ModelState.IsValid) return RedirectToAction("index");

            ImageUploader imageUploader = new ImageUploader(environment, "uploads");
            int num_imgValidation = imageUploader.uploadImage(selectedFile);
            Console.WriteLine("This is the image" + selectedFile);
            Console.WriteLine("test: " + num_imgValidation);
            //ViewData["feedBack"] = "ImageUploader.feedBack";
            Console.WriteLine("this is the feedback" + ImageUploader.feedBack.ToString());
            if (num_imgValidation == 5) {
                newQuotes.addQuote();
                Console.WriteLine("Submission: " + newQuotes.author + newQuotes.quote + newQuotes.permalink + newQuotes.quoteID + newQuotes.image);
                
            } else if (num_imgValidation == 4) {
                 ViewData["feedBack"] = "4";
            } else if (num_imgValidation == 3) {
                ViewData["feedBack"] = "3";
            } else if (num_imgValidation == 2) {
                ViewData["feedBack"] = "2";
            } else if (num_imgValidation == 1) {
                ViewData["feedBack"] = "1";
            } else if (num_imgValidation == 0) {
                ViewData["feedBack"] = "0";
            }

            string mystring1 = ImageUploader.feedBack;
            return RedirectToAction("index");
            //return View("Index");
        }
        [HttpPost]
        public IActionResult DelQuoteContr(){
            return View("Index");
        }

        public void textboxChanged(object sender, EventArgs e){
            Console.WriteLine("TEXTBOXCHANGED" + e);
        }
    }
}
