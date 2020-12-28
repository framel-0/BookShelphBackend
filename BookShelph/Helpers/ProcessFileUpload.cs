using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BookShelph.Helpers
{

    public interface IProcessFileUpload
    {
        FileResult SaveFile(IFormFile file, string path);
        void DeleteFile(string fileName, string path);
    }
    public class ProcessFileUpload : IProcessFileUpload
    {
        private IWebHostEnvironment _hostingEnvironment;

        public ProcessFileUpload(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void DeleteFile(string fileName, string path)
        {

            if (fileName != null)
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, path, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

        }

        public FileResult SaveFile(IFormFile file, string path)
        {
            string uniqueFileName = null;
            string resultFileName = null;
            decimal size = 0;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }


                string fileName = file.FileName;
                resultFileName = Path.GetFileName(fileName);
                long fileSize = file.Length;
                size = fileSize / 1000000;
                //string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                uniqueFileName = Guid.NewGuid().ToString() + "_" + resultFileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }



            }

            return new FileResult
            {
                UniqueFileName = uniqueFileName,
                FileName = resultFileName,
                FileSize = size,
            };
        }

    }
    public class FileResult
    {
        public string UniqueFileName { get; set; }
        public string FileName { get; set; }
        public decimal FileSize { get; set; }
    }
}
