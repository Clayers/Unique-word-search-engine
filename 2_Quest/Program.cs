using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SearchWordLib;

namespace _2_Quest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pathFiles = FileHelper.GetFilesInDirectory("*.txt", FileHelper.GetCurrentPath(FolderName));
            var data = FileHelper.GetAllTextByPath(pathFiles);

            var SearchWordsMethod = typeof(SearchWord)
                .GetMethod("SearchWords", BindingFlags.NonPublic | BindingFlags.Instance);

            ///ДЛЯ УВЕЛИЧЕНИЯ ДАННЫХ (тесты произ)'
            //Console.WriteLine("Start gen words");
            //StringBuilder sb = new StringBuilder(data);
            //for (var i = 0; i < 1000000; i++) sb.Append(" " + Guid.NewGuid());
            //data = sb.ToString();

            if (SearchWordsMethod != null)
            {
                var timeReflection = new Stopwatch();
                Console.WriteLine("Start Reflection");
                timeReflection.Start();
                var dictReflection =
                    (Dictionary<string, int>) SearchWordsMethod.Invoke(new SearchWord(), new object[] {data});
                timeReflection.Stop();

                Console.WriteLine("Start Thread");
                var sw = new SearchWord();
                var timeThread = new Stopwatch();
                timeThread.Start();
                var dictThread = sw.SearchWordsAsync(data);
                timeThread.Stop();
                Console.WriteLine("write file...");

                var tsReflection = timeReflection.Elapsed;
                Console.WriteLine("RunTime Reflection: " +
                                  $"{tsReflection.Hours:00}:{tsReflection.Minutes:00}:{tsReflection.Seconds:00}.{tsReflection.Milliseconds / 10:00}");

                var tsThread = timeThread.Elapsed;
                Console.WriteLine("RunTime Thread: " +
                                  $"{tsThread.Hours:00}:{tsThread.Minutes:00}:{tsThread.Seconds:00}.{tsThread.Milliseconds / 10:00}");

                var resReflection = SortingRec(dictReflection);
                FillFile.Fill("Reflection_" + OutputFile, resReflection);

                var resThread = SortingRec(dictThread);
                FillFile.Fill("Thread_" + OutputFile, resThread);
            }
        }


        public static List<Word> SortingRec(IDictionary<string, int> words)
        {
            var WordsList = new List<Word>();

            foreach (var item in words) WordsList.Add(new Word(item.Value, item.Key));

            WordsList = WordsList.OrderByDescending(x => x.Quantity).ToList();

            return WordsList;
        }

        #region Settings

        private static readonly string FolderName = "Text"; //Название папки в каталоге программы

        private const string OutputFile = "output.txt"; // название создаваемого файла

        #endregion
    }
}