using System;
using System.IO;

namespace _2_Quest
{
    internal static class FileHelper
    {
        public static string[] GetFilesInDirectory(string fileExtensions, string pathFolder)
        {
            try
            {
                return Directory.GetFiles(pathFolder, fileExtensions, SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Error get file path!");
                throw;
            }
        }

        public static string GetAllTextByPath(string[] paths)
        {
            var data = string.Empty;
            try
            {
                foreach (var path in paths)
                    using (var sr = new StreamReader(path))
                    {
                        data += " " + sr.ReadToEnd();
                    }

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error get file!");
                return null;
            }
        }

        public static string GetCurrentPath(string folder)
        {
            return AppDomain.CurrentDomain.BaseDirectory + folder;
        }
    }
}