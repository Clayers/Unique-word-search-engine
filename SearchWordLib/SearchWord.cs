using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SearchWordLib
{
    public class SearchWord
    {
        private IDictionary<string, int> SearchWords(string data)
        {
            var wordsArr = RemovePunctuationMarks(data);
            var dict = new Dictionary<string, int>();
            Calculate(wordsArr, dict);
            return dict;
        }

        private void Calculate(IEnumerable<string> wordsArr, IDictionary<string, int> dict)
        {
            foreach (var word in wordsArr)
                if (dict.ContainsKey(word))
                     dict[word]++;
                else
                    dict.Add(word, 1);
        }

        public IDictionary<string, int> SearchWordsAsync(string data)
        { 
            var dict = new ConcurrentDictionary<string, int>();
            var wordsArr = RemovePunctuationMarks(data);

            int cores = Environment.ProcessorCount;
            int totalWork = (int)Math.Floor(wordsArr.Length / (decimal)cores);

            var resultDict = new IDictionary<string, int>[cores];

            Parallel.For(0, cores, (x) =>
            {
                resultDict[x] = new Dictionary<string, int>();
                Calculate(wordsArr.Skip(totalWork * x).Take(totalWork), resultDict[x]);
            });

            Parallel.ForEach(wordsArr,
                (word) => dict.TryAdd(word, 0));

            Parallel.ForEach(dict,
                (KeyValuePair<string, int> kvp) =>
                {
                    foreach (var res in resultDict)
                    {
                        if(res.ContainsKey(kvp.Key))
                            dict[kvp.Key] += res[kvp.Key];
                    }
                });

            return dict;
        }

        private static string[] RemovePunctuationMarks(string Data)
        {
            //Data = Regex.Replace(Data, "[.?!)(,:]", "");
            //Data = Regex.Replace(Data, "<br/>|--|{|}", "");
            //Data = Regex.Replace(Data, "\"", "");
            //Data = Regex.Replace(Data, "[0-9]", "");
            //Data = Regex.Replace(Data, @" \[(.*?)\]", "");
            //Data = Regex.Replace(Data, @"\[\]", "");
            //Data = Regex.Replace(Data, @"\B\p{Lu}", "");
            //Data = Regex.Replace(Data, @"\;", "");
            //Data = Regex.Replace(Data, @"\s+", " ");

            var words = Data.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}