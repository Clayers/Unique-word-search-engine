using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using System.Threading.Tasks;

namespace SearchWordLib
{
    public class SearchWord
    {


        public Dictionary<string, int> SearchWordsTread(string data)//мультипоточность
        {
            var wordsArr = RemovePunctuationMarks(data);
            var dict = new ConcurrentDictionary<string, int>();
            Parallel.For(0, wordsArr.Length, i => {
                dict.AddOrUpdate(wordsArr[i], 1, (key, oldValue) => oldValue + 1);
            });
            return Transformation(dict);
        }


        private IDictionary<string, int> SearchWords(string data)// приватная функция без потоков
        {
            var wordsArr = RemovePunctuationMarks(data);
            var dict = new Dictionary<string, int>();
            foreach (var word in wordsArr)
                if (dict.ContainsKey(word))
                    dict[word]++;
                else
                    dict.Add(word, 1);
            return dict;
        }

        private Dictionary<string, int> Transformation(ConcurrentDictionary<string, int> dict)//преобразование из ConcurentDictonary в Dictonary
        {
            return dict.ToDictionary(pair => pair.Key, pair => pair.Value); ;
        }
        

        private void Calculate(IEnumerable<string> wordsArr, IDictionary<string, int> dict)// метод для SearchWordsAsync
        {
            foreach (var word in wordsArr)
                if (dict.ContainsKey(word))
                    dict[word]++;
                else
                    dict.Add(word, 1);
        }

        public IDictionary<string, int> SearchWordsAsync(string data)// поток при помощи выделения количества ядер пк(интересно ваше мнение по этому поводу), на пк с 16 ядрами работает быстрее чем без потоков, проверялось на Войне и мир- текст дублировали 5 раз = 2^5 
        {
            var dict = new ConcurrentDictionary<string, int>();
            var wordsArr = RemovePunctuationMarks(data);

            int cores = Environment.ProcessorCount;
            int totalWork = (int)Math.Floor(wordsArr.Length / (decimal)cores);

            var resultDict = new IDictionary<string, int>[cores];

            // делим и и вычисляем куски строки паралельно. Сторока делится на количество ядер
            Parallel.For(0, cores, (x) =>
            {
                resultDict[x] = new ConcurrentDictionary<string, int>();
                Calculate(wordsArr.Skip(totalWork * x).Take(totalWork), resultDict[x]);
            });

            Parallel.ForEach(wordsArr,
                (word) => dict.TryAdd(word, 0));
            // сумируем все куски строки
            Parallel.ForEach(dict,
                (KeyValuePair<string, int> kvp) =>
                {
                    foreach (var res in resultDict)
                    {
                        if (res.ContainsKey(kvp.Key))
                            dict[kvp.Key] += res[kvp.Key];
                    }
                });

            return dict;
        }

        private static string[] RemovePunctuationMarks(string Data)// метод чистит текст, функция Regex трати много времени. 
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

            var words = Data.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}
