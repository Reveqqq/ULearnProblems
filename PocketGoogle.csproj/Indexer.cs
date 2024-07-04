using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly Dictionary<string, Dictionary<int, List<int>>> 
            indexer = new Dictionary<string, Dictionary<int, List<int>>>();
        public void Add(int id, string documentText)
        {
            char[] separators = { ' ', '.', ',', '!', '?', ':', '–', '\r', '\n' };
            string[] text = documentText.Split(separators);
            var i = 0;
            foreach (var word in text)
            {
                if (word == "" || word == "-")
                {
                    i += word.Length + 1;
                    continue;
                }
                if (!indexer.ContainsKey(word))
                {
                    indexer[word] = new Dictionary<int, List<int>>();
                    indexer[word][id] = new List<int>();
                }
                if (!indexer[word].ContainsKey(id))
                    indexer[word][id] = new List<int>();
                indexer[word][id].Add(i);
                i += word.Length + 1;
            }
        }

        public List<int> GetIds(string word)
        {
            var list = new List<int>();
            if (indexer.ContainsKey(word)) 
            {
                foreach (var x in indexer[word])
                {
                    list.Add(x.Key);
                }
            }
            return list;
        }

        public List<int> GetPositions(int id, string word)
        {
            var list = new List<int>();
            if (indexer.ContainsKey(word) && indexer[word].ContainsKey(id))
            {
                var positions = indexer[word][id];
                list = positions;
            }
            return list;
        }

        public void Remove(int id)
        {
            foreach (var word in indexer.Keys)
            {
                indexer[word].Remove(id);
            }
        }
    }
}
