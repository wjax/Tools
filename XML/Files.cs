using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.XML
{
    public class Files
    {
        public static string ReadFile(string source)
        {
            StreamReader sr = new StreamReader(source);
            return sr.ReadToEnd();
        }

        public static void WriteFile(string source, string content)
        {
            try
            {
                StreamWriter sw = new StreamWriter(source);
                sw.Write(content);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error writing the file. ", ex);
            }
        }
    }
}
