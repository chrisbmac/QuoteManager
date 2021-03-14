using System;
using Microsoft.AspNetCore.Mvc;
using NetCoreProj2Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;


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
            Quotes myquotes = new Quotes();
            TempData["feedBack"] = ImageUploader.feedBack;
            //TempData["deletionFeedback"] = myquotes.deletionFB;
            TempData["addedFB"] = myquotes.additionFB;
            return View(myquotes);
        }

        // Adding quotes to db using form 
        [HttpPost]
        public IActionResult AddQuoteContr(Quotes newQuotes, IFormFile selectedFile) {
            // If form did not pass validation
            if (!ModelState.IsValid) return RedirectToAction("index");

            // else check file name, then add file 
            ImageUploader imageUploader = new ImageUploader(environment, "uploads");
            int num_imgValidation = imageUploader.uploadImage(selectedFile);
            
            // try and add the file
            if (num_imgValidation == 5) {
                newQuotes.duplicateFile(imageUploader.fileName);
                newQuotes.addQuote();
            }
            // file was not successful
            if (num_imgValidation != 5){
                TempData["feedBack"] = ImageUploader.feedBack;
                
                return View("Index", newQuotes);
            }
            TempData["addedFB"] = newQuotes.additionFB;
            return RedirectToAction("index");
  
        }
        // Deletion of quotes
        [HttpPost]
        public IActionResult DelQuoteContr(Quotes newQuotes){
            ImageManager newIM = new ImageManager();
            
            newQuotes.deleteQuote();
            newIM.imageDeletion(environment, newQuotes.imageForDeletion);
            TempData["deletionFeedback"] = newQuotes.deletionFB;
            return RedirectToAction("Index", newQuotes);
        }
    }
}
