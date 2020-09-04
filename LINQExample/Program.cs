using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace LINQExample
{
    class Program
    {
        static readonly string query = "Wikipedia is an online free-content encyclopedia project that aims to help" +
            " create a world in which everyone can freely share in the sum of all knowledge. It is supported by" +
            " the Wikimedia Foundation and based on a model of openly editable content. The name Wikipedia is a" +
            " blending of the words wiki and encyclopedia. Wikipedia articles provide links designed to guide" +
            " the user to related pages with additional information.";
        static void Main(string[] args)
        {
            string searchString = "wikipedia";
            NumberOfWords(searchString);

            string[] wordsToMatch = { "Wikipedia", "wiki" , "encyclopedia" };
            GetSentenceWithMatchingWords(wordsToMatch);

            string inputString = "Wikipedia124542Search";
            CountDigits(inputString);

            List<string> inputStrings = new List<string>();
            inputStrings.Add("Visual C#");
            inputStrings.Add("Visual C");
            inputStrings.Add("Visual Node");
            inputStrings.Add("Visual F#");
            RegexExample(inputStrings);
        }

        static void CountDigits(string inputString)
        {
            IEnumerable<char> stringQuery =
              from ch in inputString
              where Char.IsDigit(ch)
              select ch;

            Console.WriteLine(string.Join(" ",stringQuery));
             
            int count = stringQuery.Count();
            Console.WriteLine("Count = {0}", count);
        }

        static void GetSentenceWithMatchingWords(string[] wordsToMatch)
        {
            string[] sentences = query.Split(new char[] { '.', '?', '!' });

            var result = from sentence in sentences
                                let w = sentence.Split(
                                    new char[] { '.', ' ', ',' },
                                        StringSplitOptions.RemoveEmptyEntries
                                    )
                                where w.Distinct()
                                       .Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                                select sentence;

            foreach (string sentence in result)
            {
                Console.WriteLine(sentence.Trim());
            }
        }
        static void NumberOfWords(string searchString)
        {

            string[] source = query.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

            var result = from words in source
                         where words.ToLowerInvariant() == searchString.ToLowerInvariant()
                         select words;

            int wordCount = result.Count();
            Console.WriteLine("{0} occurrences(s) were found.", wordCount);
        }

        public static void RegexExample(List<string> inputStrings)
        {
            System.Text.RegularExpressions.Regex searchTerm =
                new System.Text.RegularExpressions.Regex(@"Visual (F#|C#)");

            var matchedStrings =
                from inputString in inputStrings
                let matches = searchTerm.Matches(inputString)
                where matches.Count > 0
                select new
                {
                    name = inputStrings,
                    matchedValues = from System.Text.RegularExpressions.Match 
                                    match in matches
                                    select match.Value
                };

            foreach (var v in matchedStrings)
            {
                Console.WriteLine(v.matchedValues.FirstOrDefault());
            }
        }
    }
}
