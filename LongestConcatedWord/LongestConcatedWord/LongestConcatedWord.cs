using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace langestWord
{
    class LongestConcatedWord
    {
        public static void Main(string[] args)
        {
            try
            {
                // Here, Path is HardCoded, we can either use ServerPath or just add the file into the project
                string path = @"D:\WordFile.txt";

                LongestConcatedWord findLongest = new LongestConcatedWord();

                findLongest.findWord(path);
                if (findLongest.GetConcatedWordsCount() > 0)
                {
                    Console.WriteLine("First longest concatenated word is=" + findLongest.GetFirstLongestConcatedWord());
                    Console.WriteLine("Second longest concatenated word is=" + findLongest.GetSecondLongestConcatedWord());
                    Console.WriteLine("number of words in it=" + findLongest.GetConcatedWordsCount());

                }
                else Console.WriteLine("no concatenated word present in testfile");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private Trie t = new Trie();
        public List<String> concatedWords = new List<string>();
        private string firstLongestConcatedWord;
        private string secondLongestConcatedWord;
        public String GetFirstLongestConcatedWord()
        {
            return firstLongestConcatedWord;
        }
        public String GetSecondLongestConcatedWord()
        {
            return secondLongestConcatedWord;
        }
        public int GetConcatedWordsCount()
        {
            return concatedWords.Count;
        }

        public bool isRequiredWord(String word, bool fullword)
        {
            // Remove the word so that the word is not matched to itself to find the longest word
            if (fullword)
            {
                t.RemoveWord(word);
            }
            // Loop over the length of the word
            for (int i = 0; i < word.Length; i++)
            {
                //System.out.println(word.substring(0, i+1));
                if (t.HasWord(word.Substring(0, i + 1)))
                {
                    // Check if entire word is in trie or Reminder is in trie
                    if (i == word.Length - 1 || isRequiredWord(word.Substring(i + 1, word.Length - i - 1), false))
                    {
                        return true;
                    }
                }
            }
            if (fullword)
            {
                t.AddWord(word);
            }
            return false;
        }

        public void AddWordsIntoTrie(Dictionary<String, int> sortedWords)
        {
            foreach (string key in sortedWords.Keys)
            {
                t.AddWord(key);
            }
        }
        public void findLongestConcatedWord(Dictionary<string, int> sortedWords)
        {
            try
            {
                //traverse in sortedWords From Longest Word
                for (int i = sortedWords.Keys.Count - 1; i >= 0; i--)
                {
                    if (isRequiredWord(sortedWords.ElementAt(i).Key, true))
                    {
                        concatedWords.Add(sortedWords.ElementAt(i).Key);
                    }
                    // We can get this one also in directly output but here the purpose is for testing and making sure we are getting longest words
                    if (concatedWords.Count == 1)
                    {
                        firstLongestConcatedWord = concatedWords[0];
                    }
                    if (concatedWords.Count == 2)
                    {
                        secondLongestConcatedWord = concatedWords[1];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void findWord(String filePath)
        {
            try
            {
                string line;
                Dictionary<string, int> sortedDictionary = new Dictionary<string, int>();
                // Add each word as a key and its respective length as a value
                Dictionary<string, int> wordDictionary = new Dictionary<string, int>();

                StreamReader file = new StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    wordDictionary[line] = line.Length;
                }
                //insert the entries sorted by value into Dictionary.
                var wordList = wordDictionary.ToList();
                wordList.Sort((x, y) => x.Value.CompareTo(y.Value));
                foreach (KeyValuePair<String, int> keyValue in wordList)
                {
                    var key = keyValue.Key;
                    var value = keyValue.Value;
                    sortedDictionary[key] = value;
                }
                //Adding Words to Trie Data Structure
                AddWordsIntoTrie(sortedDictionary);
                //Main Part of Program - Logic embeded
                findLongestConcatedWord(sortedDictionary);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}