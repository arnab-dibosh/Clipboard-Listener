using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;

namespace Clipboard_Listener
{
    public class Utility
    {
        public static string DestPath { get; set; }

        public static void WriteText(string file, string text)
        {
            string fileName = Path.Combine(DestPath, file);
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                using (StreamWriter writetext = new StreamWriter(fileName))
                {
                    writetext.WriteLine(text);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }  
        }

        public static void DumbFile(StringCollection fileNames)
        {
            foreach (var sourceFile in fileNames)
            {
                try
                {
                    string fileName = Path.GetFileName(sourceFile);
                    File.Copy(sourceFile, Path.Combine(DestPath, fileName));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
