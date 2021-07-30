using System.Net;

namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string uri, string path);
    }

    class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string uri, string path)
        {
            var client = new WebClient();
            client.DownloadFile(uri, path);
        }
    }
}
