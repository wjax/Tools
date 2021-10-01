using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    public class MTools
    {
        public static string AbsolutizePath(string path, bool useActualFolder = false)
        {
            const string REGEX_PATH = @"^[A-Za-z]+:";
            string absolutePath;

            if (Regex.IsMatch(path, REGEX_PATH))
                absolutePath = path;
            else if (useActualFolder)
                absolutePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), path);
            else
                absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            return absolutePath;
        }
    }
}
