using System;
using System.Collections.Generic;
using System.IO;

namespace _2_Quest
{
    internal class SearchFile
    {
        public List<string> GetAllFiles(string FileExtensions, string pathfolder)
        {
            try
            {
                List<string> files = new List<string>();
                files.AddRange(Directory.GetFiles(pathfolder, FileExtensions, SearchOption.AllDirectories));
                return files;
            }
            catch (Exception)
            {
                Console.WriteLine("Error get file path!");
                throw;
            }
        }

        public string GetStringFile(string path)
        {
            string Date = @"";
            StreamReader srv = new StreamReader(path);
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    Date = sr.ReadToEnd();
                    return Date;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error get file!");
                return null;
            }
        }

        public string GetPath(string folder)
        {
            return AppDomain.CurrentDomain.BaseDirectory + folder;
        }
    }
}