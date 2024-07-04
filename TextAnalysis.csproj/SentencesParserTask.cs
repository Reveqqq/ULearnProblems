using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();

            //Разделение текста на предложения. Все символы маленькие
            string[] separatorsOfSentences = { ".", "!", "?", ";", ":", "(", ")" };
            string[] sentences = text.ToLower().Split(separatorsOfSentences, System.StringSplitOptions.RemoveEmptyEntries);


            //Разделение предложений на слова методом SplitIntoWords
            foreach (string sentence in sentences)
            {
                //Проверка на пустую строку
                if (!string.IsNullOrWhiteSpace(sentence) && SplitIntoWords(sentence).Capacity > 0)
                {
                    sentencesList.Add(SplitIntoWords(sentence));
                }
            }
            return sentencesList;
        }

        //Разделение предложения на слова
        public static List<string> SplitIntoWords(string sentence)
        {
            var words = new List<string>();
            var wordsBuilder = new StringBuilder();

            //Составление слов из букв и '
            foreach (char ch in sentence)
            {
                if (char.IsLetter(ch) || ch == '\'')
                    wordsBuilder.Append(ch);
                else
                    wordsBuilder.Append(' ');
            }
            string[] stringsOfWords = wordsBuilder.ToString().Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in stringsOfWords)
            {
                words.Add(word);
            }
            return words;
        }
    }
}