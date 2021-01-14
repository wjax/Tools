using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
        public delegate void DownloadCompleteDelegate(string localFile);
        public static event DownloadCompleteDelegate DownloadCompleteEvent;

        private static BlockingQueue<Download> downloadJobs = new BlockingQueue<Download>();
        private static List<string> ongoingFiles = new List<string>();
        private static bool exit = false;

        private static object lockOngoingFiles = new object();

        static Downloader()
        {
            Task.Run(() => RunningCallback());
        }

        private static string BuildKey(string local, string remote)
        {
            return local + remote;
        }

        public static void AddDownload(string local, string remote)
        {
            lock (lockOngoingFiles)
            {
                if (!ongoingFiles.Contains(BuildKey(local, remote)))
                {
                    downloadJobs.Enqueue(new Download()
                    {
                        LocalPath = local,
                        RemoteUri = remote
                    });
                    ongoingFiles.Add(BuildKey(local, remote));
                }
            }
        }

        private static void RunningCallback()
        {
            while (!exit)
            {
                Download job = downloadJobs.Dequeue();
                if (Download(job.LocalPath, job.RemoteUri))
                    DownloadCompleteEvent?.Invoke(job.LocalPath);

                lock (lockOngoingFiles)
                {
                    ongoingFiles.Remove(BuildKey(job.LocalPath, job.RemoteUri));
                }
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
