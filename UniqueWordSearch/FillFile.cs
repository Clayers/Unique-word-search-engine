using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueWordSearch
{
    class FillFile
    {
        public static void Fill(string nameFile, List<Word> words)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + nameFile;

            var file = new FileStream(path, FileMode.OpenOrCreate);

            var fnew = new StreamWriter(file);
            for (var i = 0; i < words.Count; i++)
                fnew.WriteLine($"{words[i].Words} ({words[i].Quantity})");
            fnew.Close();
        }
    }
}
