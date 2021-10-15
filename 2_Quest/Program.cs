using System.Collections.Generic;

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
            SearchWord SearchWord = new SearchWord();
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
                FillFile.Fill("New", SearchWord.SearchAllWord(Data));
            }
            if (Switch == true)
            {
                FillSearchWord(WordList);
                FillFile.Fill("New", SearchWord.SearchWords(Data, WordList));
            }
        }
    }
}