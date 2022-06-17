using Azure.Storage.Blobs;
using REST_API_XFIA.Modules.Service;
using System.Reflection;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace REST_API_XFIA.Modules.Service
{
    public class StorageService : IStorageService
    {
        

        public StorageService()
        {
            
        }

        public string Upload(IFormFile formFile, string name = "")
        {
            var FileDic = "Files\\Images";

            string DirPath = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FileDic);

            if (!Directory.Exists(DirPath))
            { 
                Directory.CreateDirectory(DirPath);
            }
            
            var filePath = Path.Combine(DirPath, formFile.FileName);
            if (!name.Equals(""))
            {
                var FileName = name + "." +formFile.FileName.Split(".")[1];
                filePath = Path.Combine(DirPath, FileName);
            }
            using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                formFile.CopyTo(fileStream);
            }
            return filePath;
        }
    }
}
