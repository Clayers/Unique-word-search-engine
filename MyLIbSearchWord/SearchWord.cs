using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyLIbSearchWord
{
    public class SearchWord
    {


        private Dictionary<string, int> SearchAllWord(string Data)
        {
            Dictionary<string, int> Words = new Dictionary<string, int>();
            Words.Clear();
            string[] wordsArr = RemovePunctuationMarks(Data);

            for (int i = 1; i < wordsArr.Length - 1; i++)
            {
                if (!Words.ContainsKey(wordsArr[i]))
                {
                    Words.Add(wordsArr[i], 1);
                }
                else
                {
                    Words[wordsArr[i]]++;
                }
            }
            return Words;
        }

        private Dictionary<string, int> SearchWords(string Data, List<string> WordSearch)
        {
            Dictionary<string, int> Words = new Dictionary<string, int>();
            Words.Clear();
            string[] wordsArr = RemovePunctuationMarks(Data);
            foreach (var item in WordSearch)
            {
                Words.Add(item, 0);
            }
            for (int i = 1; i < wordsArr.Length - 1; i++)
            {
                if (Words.ContainsKey(wordsArr[i]))
                {
                    Words[wordsArr[i]]++;
                }
            }
            return Words;
        }

        private string[] RemovePunctuationMarks(string Data)
        {
            Data = Regex.Replace(Data, "[.?!)(,:]", "");
            Data = Regex.Replace(Data, "<br/>|--|{|}", "");
            Data = Regex.Replace(Data, "\"", "");
            Data = Regex.Replace(Data, "[0-9]", "");
            Data = Regex.Replace(Data, @" \[(.*?)\]", "");
            Data = Regex.Replace(Data, @"\[\]", "");
            Data = Regex.Replace(Data, @"\B\p{Lu}", "");
            Data = Regex.Replace(Data, @"\;", "");
            Data = Regex.Replace(Data, @"\s+", " ");

            string[] words = Data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}
