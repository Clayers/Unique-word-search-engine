using MyLIbSearchWord;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Reflection;

namespace _2_Quest
{
    internal class Program
    {
        static public string Path = "Text";//Название папки в каталоге программы
        static public bool Switch = false;// Переключатель-поиск слов вписанных в List Word = true|| поиск всех уникальных слов false.
        static public string NameFile = "New";// название создаваемого файла

        static public List<string> FillSearchWord(List<string> WordList)
        {
            WordList.Add("Пьер");
            WordList.Add("день");
            WordList.Add("смотрел");
            WordList.Add("Ежели");
            WordList.Add("plus");

            return WordList;


        }

        private static void Main(string[] args)
        {
            


            List<string> PathFile = new List<string>();// Лист с путями файлов
            SearchFile SearchFile = new SearchFile();
            FillFile FillFile = new FillFile();
            List<Word> Words = new List<Word>();//найденные слова
            List<string> WordList = new List<string>();//искомые слова
            string Data = "";

            PathFile = SearchFile.GetAllFiles("*.txt", SearchFile.GetPath(Path));
            for (int i = 0; i < PathFile.Count; i++)
            {
                Data = Data + SearchFile.GetStringFile(PathFile[i]);
            }
            
            if (Switch == false)
            {
                FillFile.Fill("New", SortingRec((Dictionary<string, int>)typeof(SearchWord).GetMethod("SearchAllWord", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(new SearchWord(), new object[] { Data })));
            }
            if (Switch == true)
            {
                FillSearchWord(WordList);
                FillFile.Fill("New", SortingRec((Dictionary<string, int>)typeof(SearchWord).GetMethod("SearchWords", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(new SearchWord(), new object[] { Data })));
            }
        }


        static public List<Word> SortingRec(Dictionary<string, int> Words)
        {
            List<Word> WordsList = new List<Word>();

            foreach (var item in Words)
            {
                WordsList.Add(new Word(item.Value, item.Key));
            }

            WordsList = WordsList.OrderByDescending(x => x.Quantity).ToList();

            return WordsList;
        }
    }
}