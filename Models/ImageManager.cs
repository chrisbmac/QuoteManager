using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace NetCoreProj2Admin.Models
{
    public class ImageManager
    {
       // property variables
        private List<string> _imageSources;

        // private variables
        private string fullPath;
        private string targetFolder;

        public ImageManager() {
            _imageSources = new List<string>();

        }

        // ----------------------------------------------- get/sets
        public List<string> imageSources {
            get {
                return _imageSources;
            }
        }

        // ----------------------------------------------- public methods
        public void buildImageList(IWebHostEnvironment env, string myTargetFolder) {
            targetFolder = myTargetFolder;

            // determining the full absolute path to the target folder we want to work with
            fullPath = env.WebRootPath + "/" + targetFolder + "/";
            Console.WriteLine(fullPath);
            
            if (!Directory.Exists(fullPath)) {
                Directory.CreateDirectory(fullPath);
            }

            // grab the full path and file of images in the targetted folder
            string[] imageArray = Directory.GetFiles(fullPath, "*.*");
            // populate list with relative URLS for use of <img>
            for (int i=0; i<imageArray.Length; i++) {
                _imageSources.Add("/" + targetFolder + "/" + Path.GetFileName(imageArray[i]));
            }
        }
        
    }
} 
