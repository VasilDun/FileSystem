using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using System.Web.Script.Serialization;

namespace FileSystem_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input Folder: ");
            string folder = Console.ReadLine();

            if (Directory.Exists(folder))
            {
                Console.WriteLine("---------------------------");
                DirectoryInfo dir = new DirectoryInfo(@folder);
                var json = new JavaScriptSerializer().Serialize(new Folder(dir.Name, Directory.GetCreationTime(folder).ToString()));
                Console.WriteLine(json);
                Console.WriteLine("---------------------------");
                DisplayFiles(folder);
            }
            else {
                Console.WriteLine("Folder does not exist");
            }
            Console.ReadLine();
        }

        static void DisplayFiles(string folder)
        {
            DirectoryInfo dir = new DirectoryInfo(@folder);

            List<FileInfo> files = new List<FileInfo>();
            List<DirectoryInfo> directories = new List<DirectoryInfo>();

            try
            {
                foreach (FileInfo f in dir.GetFiles())
                {
                    files.Add(f);
                }
                Console.WriteLine("Files:");
                if (files.Count > 0)
                {
                    foreach (FileInfo f in files)
                    {
                        var obj = new File
                        {
                            Name = f.Name,
                            Size = (double)f.Length / 1024.0,
                            Path = f.FullName
                        };
                        var json = new JavaScriptSerializer().Serialize(obj);
                        Console.Write(json);
                    }
                }
                else
                {
                    var json = new JavaScriptSerializer().Serialize("No Files here!");
                    Console.Write(json);
                }
                Console.WriteLine();
            
            
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    directories.Add(d);
                }
                Console.WriteLine("Children:");
                if (directories.Count > 0)
                {
                    foreach (DirectoryInfo d in directories)
                    {
                        var obj = new Folder
                        {
                            Name = d.Name,
                            DateCreated = d.CreationTime.ToString()
                        };

                        var json = new JavaScriptSerializer().Serialize(obj);
                        Console.WriteLine(json+"\n");
                        DisplayFiles(d.FullName);
                    }
                }
                else
                {
                    var json = new JavaScriptSerializer().Serialize("No Children here!");
                    Console.WriteLine(json);
                }
                Console.WriteLine();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Access is denied\n\n");
            }
        }
    }
}
