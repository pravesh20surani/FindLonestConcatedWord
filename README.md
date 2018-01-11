# FindLonestConcatedWord
Find Longest Concated Word From The File
Approach I took - 
a. Read All Words from file and put it into a dictionary(Word as a Key and Length of Word as a Value).
b. Sort Dictionary based on Value and generate KeyValuePair.
c. Iterate through Key Value Pair and put it into a dictionary which ideally generates sorted dictionary based on length of word.
d. Add all words into Trie data structure(https://en.wikipedia.org/wiki/Trie).
e. Call findLongestConcatedWord() method which takes SortedWord as a parameter.
f. Iterate through sorted dictionary keys in Descending order. Here we are also checking for all substrings of that word through IsRequiredWord() method.
g. Consecutively, we put all concatedwords into concatedWords list. First element of concatedWords gives us the first longest concated word. While second element gives us the second longest concated element. Total count of words in the concatedWords list gives us the number of words which can be built from other words.
