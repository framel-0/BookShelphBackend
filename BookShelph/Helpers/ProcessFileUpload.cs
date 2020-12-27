using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BookShelph.Helpers
{

    public interface IProcessFileUpload
    {
        string SaveFile(IFormFile file, string path);
    }
    public class ProcessFileUpload : IProcessFileUpload
    {
        private IWebHostEnvironment _hostingEnvironment;

        public ProcessFileUpload(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string SaveFile(IFormFile file, string path)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }


                string fileName = file.FileName;
                string result = Path.GetFileName(fileName);
                //string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                uniqueFileName = Guid.NewGuid().ToString() + "_" + result;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }



            }

            return uniqueFileName;
        }
    }
}
