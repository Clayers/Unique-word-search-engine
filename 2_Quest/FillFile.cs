using System;
using System.Collections.Generic;
using System.IO;

namespace _2_Quest
{
    internal class FillFile
    {
        public void Fill(string NameFile, List<Word> Words)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + NameFile + ".txt";

            FileStream file = new FileStream(path, FileMode.OpenOrCreate);

            StreamWriter fnew = new StreamWriter(file);
            for (int i = 0; i < Words.Count; i++)
            {
                fnew.WriteLine(String.Format(Words[i].Words.ToString() + "(" + Words[i].Quantity.ToString() + ")"));
            }
            fnew.Close();
        }
    }
}