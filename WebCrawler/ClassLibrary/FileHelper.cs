using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class FileHelper
    {
        public static void SaveAsFile(String pFileNamePath, String pContent)
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(pFileNamePath))
            {
                sw.Write(pContent);
            }
        }
    }
}
