using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace NetCoreProj2Admin.Models
{
    public class ImageUploader
    {
       public const int ERROR_NO_FILE = 0;
        public const int ERROR_TYPE = 1;
        public const int ERROR_SIZE = 2;
        public const int ERROR_NAME_LENGTH = 3;
        public const int ERROR_SAVE = 4;
        public const int SUCCESS = 5;
        private int _num_ImgValidation;

        // this is the file size limit in bytes that IFormFile approach can handle
        // do have the option to stream larger files - but is more complicated
        private const int UPLOADLIMIT = 4194304;

        // needed for getting path to web app's location
        private string targetFolder;
        // path to the upload folder
        private string fullPath;
        
        private static string _feedBack;
        public ImageUploader(IWebHostEnvironment env, string myTargetFolder) {
            // initialization
            targetFolder = myTargetFolder;         
            _feedBack = "";
            _num_ImgValidation = -1;
            // check to see if web app's root folder has an "uploads" folder - if not create it
            fullPath = env.WebRootPath + "/" + targetFolder + "/";
            if (!Directory.Exists(fullPath)) {
                Directory.CreateDirectory(fullPath);
            }
        }
        public int num_ImgValidation {
            get {
                return _num_ImgValidation;
            }
        }
        
        public static string feedBack{
            get {
                return _feedBack;
            }
        }

        // --------------------------------------------------- public methods
        public int uploadImage(IFormFile file) {
            // has the user selected a file to upload?
            Console.WriteLine(file);
            if (file != null) {
                // has the user uploaded an image file?
                string contentType = file.ContentType;
                if ((contentType == "image/png") || (contentType == "image/jpeg") || (contentType == "image/gif")) {
                    // is the file too big?
                    long size = file.Length;
                    if ((size > 0) && (size < UPLOADLIMIT)) {
                        // is the filename too long?
                        string fileName = Path.GetFileName(file.FileName);
                        if (fileName.Length <= 100) {

                            // WE are go to save the file to the server!!!
                            FileStream stream = new FileStream((fullPath + fileName), FileMode.Create);
                            try {
                                file.CopyTo(stream);
                                stream.Close();
                                _feedBack = "File was successfully writin to server";
                                return _num_ImgValidation = ImageUploader.SUCCESS;
                            } catch {
                                stream.Close();
                                _feedBack = "Error while writing file name to server";
                                return _num_ImgValidation = ImageUploader.ERROR_SAVE;
                            }

                        } else {
                            _feedBack = "Error, file name to long";
                            return _num_ImgValidation = ImageUploader.ERROR_NAME_LENGTH;
                        }
                    } else {
                        _feedBack = "Error, file size to large";
                        return _num_ImgValidation = ImageUploader.ERROR_SIZE;
                    }
                } else {
                    _feedBack = "Error, file wrong type";
                    return _num_ImgValidation = ImageUploader.ERROR_TYPE;
                }
            } else {
                _feedBack = "Error, no file can be found";
                return _num_ImgValidation = ImageUploader.ERROR_NO_FILE;
            }
        }
    }

} 