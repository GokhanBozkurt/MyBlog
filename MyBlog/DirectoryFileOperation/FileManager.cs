using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFileOperation
{
    public class FileManager
    {
        public static void ReadFilesFromDirectory(string path, string searchPattern)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles();

            foreach (var item in files)
            {
                Console.WriteLine(item.Name );
            }
        }
    }
}
