using System.IO;
using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Storages
{
	public interface IStorageContext
	{
        void Authenticate(string url, string key, string user, string passwd);
        string UploadFile(string bucketPath, string filePath);
        string UploadFile(string bucketPath, Stream fileStream);
        void DownloadFile(string bucketPath, string fileLocalPath);
        void SetLogger(ILogger logger);
        ILogger GetLogger();        
    }    
}
