using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SearchWordLib;

namespace UniqueWordSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathFiles = FileHelper.GetFilesInDirectory("*.txt", FileHelper.GetCurrentPath(FolderName));
            var data = FileHelper.GetAllTextByPath(pathFiles);//Получаем всю строку
            SearchWordWCF(data);
           // SearchWordReflection(data);
          //  SearchWordTread(data);
           // SearchWordTreadCore(data);
           
            Console.ReadKey();
        }

        public static void SearchWordWCF(string data)// WCF
        {
            Console.WriteLine("Start WCF");
            Dictionary<string, int> res;
            using (var client = new ServiceReference1.Service1Client())
            {
                res = client.WcfSearchFile(data);
            }
            Console.WriteLine("End WCF");
            var resReflection = SortingRec(res);
            FillFile.Fill("WCF_" + OutputFile, resReflection);
        }


        public static void SearchWordReflection(string data)// метод цапуска приватного потока из библы и подсчета времени, а также вывода в файл
        {
            var timeReflection = new Stopwatch();
            Console.WriteLine("Start Reflection");

            timeReflection.Start();
            var SearchWordsMethod = typeof(SearchWord)
                .GetMethod("SearchWords", BindingFlags.NonPublic | BindingFlags.Instance);
            var dictReflection =
                (Dictionary<string, int>)SearchWordsMethod.Invoke(new SearchWord(), new object[] { data });
            timeReflection.Stop();

            var tsReflection = timeReflection.Elapsed;
            Console.WriteLine("RunTime Reflection: " +
                              $"{tsReflection.Hours:00}:{tsReflection.Minutes:00}:{tsReflection.Seconds:00}.{tsReflection.Milliseconds / 10:00}");
            var resReflection = SortingRec(dictReflection);
            FillFile.Fill("Reflection_" + OutputFile, resReflection);

        }

        public static void SearchWordTreadCore(string data)// метод запуска мультипотоука при помощи ядер, а также вывода в файл
        {
            Console.WriteLine("Start Thread Core");   
            var timeThreadCore = new Stopwatch();
            timeThreadCore.Start();
            var sw = new SearchWord();
            var dictThread = sw.SearchWordsAsync(data);
            timeThreadCore.Stop();
            var tsThread = timeThreadCore.Elapsed;
            Console.WriteLine("RunTime Thread Core: " +
                              $"{tsThread.Hours:00}:{tsThread.Minutes:00}:{tsThread.Seconds:00}.{tsThread.Milliseconds / 10:00}");
            var resThread = SortingRec(dictThread);
            FillFile.Fill("ThreadCore_" + OutputFile, resThread);

        }


        public static void SearchWordTread(string data)// метод цапуска мультипотокапотока из библы и подсчета времени, а также вывода в файл
        {
            Console.WriteLine("Start Thread");
            var timeThread = new Stopwatch();
            timeThread.Start();
            var sw = new SearchWord();
            var dictThread = sw.SearchWordsTread(data);
            timeThread.Stop();

            var tsnewThread = timeThread.Elapsed;
            Console.WriteLine("RunTime Thread: " +
                              $"{tsnewThread.Hours:00}:{tsnewThread.Minutes:00}:{tsnewThread.Seconds:00}.{tsnewThread.Milliseconds / 10:00}");
            var resnewThread = SortingRec(dictThread);
            FillFile.Fill("Thread_" + OutputFile, resnewThread);
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
