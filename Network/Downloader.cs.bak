using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tools.Base;

namespace Tools.Network
{
    internal class Download
    {
        public string LocalPath;
        public string RemoteUri;
        public bool Completed = false;
    }

    public class Downloader
    {
        private static BlockingQueue<Download> downloadJobs = new BlockingQueue<Download>();
        private static bool exit = false;

        static Downloader()
        {
            Task.Run(() => RunningCallback());
        }

        public static void AddDownload(string local, string remote)
        {
            downloadJobs.Enqueue(new Download() {
                LocalPath = local,
                RemoteUri = remote
            });
        }

        private static void RunningCallback()
        {
            while (!exit)
            {
                Download job = downloadJobs.Dequeue();
                Download(job.LocalPath, job.RemoteUri);
            }
        }

        public static bool Download(string local, string remote)
        {
            try
            {
                if (File.Exists(local))
                {
                    return false;
                }

                var client = new HttpClient();
                using (var stream = client.GetStreamAsync(remote).Result)
                using (var outputStream = File.OpenWrite(local))
                {
                    stream.CopyTo(outputStream);
                }

                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
