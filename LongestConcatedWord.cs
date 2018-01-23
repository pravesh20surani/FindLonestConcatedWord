using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace LongestWord
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
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                findLongest.findWord(path);
                if (findLongest.GetConcatedWordsCount() > 0)
                {
                    Console.WriteLine("First longest concatenated word is=" + findLongest.GetFirstLongestConcatedWord());
                    Console.WriteLine("Second longest concatenated word is=" + findLongest.GetSecondLongestConcatedWord());
                    Console.WriteLine("Number of words in it=" + findLongest.GetConcatedWordsCount());
                }
                else Console.WriteLine("no concatenated word present in testfile");
                stopWatch.Stop();
                Console.WriteLine($"Time Taken: { stopWatch.ElapsedMilliseconds.ToString()}ms");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private string firstLongestConcatedWord;
        private string secondLongestConcatedWord;
        private int totalCount;
        private Dictionary<string, int> wordDictionary = new Dictionary<string, int>();
        private Dictionary<string, int> lookUp = new Dictionary<string, int>();

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
            return totalCount;
        }
        public void findWord(String filePath)
        {
            try
            {
                string line;
                // Add each word as a key and its respective length as a value
                StreamReader file = new StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    wordDictionary[line] = line.Length;
                }
                findLongestConcatedWord();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void findLongestConcatedWord()
        {
            try
            {
                //traverse in keys of Dictionary
                foreach (var key in wordDictionary.Keys)
                {
                    if (isRequiredWord(key, true))
                    {
                        totalCount++;
                    }
                }
                // Get First and Second Longest Concatenated Word
                int maxOne = 0;
                int maxTwo = 0;
                firstLongestConcatedWord = "";
                secondLongestConcatedWord = "";
                foreach (String word in lookUp.Keys)
                {
                    if (maxOne < lookUp[word])
                    {
                        maxTwo = maxOne;
                        secondLongestConcatedWord = firstLongestConcatedWord;
                        maxOne = lookUp[word];
                        firstLongestConcatedWord = word;
                    }
                    else if (maxTwo < lookUp[word])
                    {
                        maxTwo = lookUp[word];
                        secondLongestConcatedWord = word;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public bool isRequiredWord(String word, bool fullword)
        {
            if (!fullword && wordDictionary.ContainsKey(word))
            {
                return true;
            }
            if (lookUp.ContainsKey(word))
            {
                return true;
            }
            for (int i = 0; i < word.Length - 1; i++)
            {
                if (wordDictionary.ContainsKey(word.Substring(0, i + 1)))
                {
                    if (isRequiredWord(word.Substring(i + 1, word.Length - i - 1), false))
                    {
                        lookUp[word] = word.Length;
                        return true;
                    }
                }
            }
            return false;
        }   
    }
}
