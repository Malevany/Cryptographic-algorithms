using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace InfSecLab
{
    abstract class WorkWithFile
    {
        public static string ReadFile(string path)
        {
            string textFile = File.ReadAllText(path);
            return textFile;
        }
        public static void WriteFile(string path, string outputMessage)
        {
            using (StreamWriter SW = new StreamWriter(path))
                SW.WriteLine(outputMessage);
        }
    }
}
