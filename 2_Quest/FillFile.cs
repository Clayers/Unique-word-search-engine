using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _2_Quest
{
    internal static class FillFile
    {
        public static void Fill(string nameFile, List<Word> words)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + nameFile;

            var file = new FileStream(path, FileMode.OpenOrCreate);

            using var fnew = new StreamWriter(file);
            for (var i = 0; i < words.Count; i++)
                fnew.WriteLine($"{words[i].Words} ({words[i].Quantity})");
        }
    }
}