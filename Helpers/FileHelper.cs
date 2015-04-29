using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PermissionReporting
{
    public class FileHelper
    {
        /// <summary>
        /// Saves a text file
        /// </summary>
        /// <param name="FileContents">Contents of the file</param>
        /// <param name="FilePath">Path to save to</param>
        public static void SaveFile(string FileContents, string FilePath)
        {
            File.WriteAllText(FilePath, FileContents);
        }
    }
}
