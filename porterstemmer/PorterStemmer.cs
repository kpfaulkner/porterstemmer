using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace porterstemmer
{
    class PorterStemmer
    {

        List<string> targetStringList = new List<string>();

        // Interesting concept of consonant.
        bool Cons( string word, int i)
        {
            var ch = word[i];
            switch (ch)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u': return false;
                case 'y': return (i == 0) ? true : !Cons(word, i-1);
                default: return true;
            }
        }

        /* m() measures the number of consonant sequences between k0 and j. if c is
           a consonant sequence and v a vowel sequence, and <..> indicates arbitrary
           presence,

              <c><v>       gives 0
              <c>vc<v>     gives 1
              <c>vcvc<v>   gives 2
              <c>vcvcvc<v> gives 3
              ....
        */
        public int M(string word)
        {
            var count = 0;
            var index = 0;
            while ( index < word.Length -1)
            {
                if (!Cons(word, index) && Cons(word, index + 1))
                {
                    count++;
                    index += 2;
                }
                else
                {
                    index++;
                }
            }

            return count;

        }

        bool VowelInStem(string stem)
        {
            for (var i = 0; i < stem.Length; i++)
            {
                if (!Cons(stem, i))
                    return true;
            }

            return false;
        }

        /* step1ab() gets rid of plurals and -ed or -ing. e.g.

            caresses  ->  caress
            ponies    ->  poni
            ties      ->  ti
            caress    ->  caress
            cats      ->  cat

            feed      ->  feed
            agreed    ->  agree
            disabled  ->  disable

            matting   ->  mat
            mating    ->  mate
            meeting   ->  meet
            milling   ->  mill
            messing   ->  mess

            meetings  ->  meet

            Naieve approach. Lots of reallocation of string.
       
        */
        string Step1ab(string word)
        {  
            var wordLength = word.Length;

            // last letter of word is 's'
            if (word.EndsWith("s"))
            {  
                if (word.EndsWith("sses"))
                {
                    word = word.Substring(0, wordLength - 2);
                }
                else if (word.EndsWith("ies"))
                {
                    word = word.Substring(0, wordLength -2);
                } else if (word.EndsWith("ss"))
                {
                    word = word.Substring(0, wordLength -2);
                }
            }

            if (word.EndsWith( "eed"))
            {
                if (M(word) > 0)
                {
                    word = word.Substring(0, wordLength - 1);
                }
                else
                {
                    if ((word.EndsWith("ed") || word.EndsWith("ing")) && VowelInStem(word))
                    {

                    }

                }


            }

            /*
           if (ends("\03" "eed")) { if (m() > 0) k--; } else
           if ((ends("\02" "ed") || ends("\03" "ing")) && vowelinstem())
           {  k = j;
              if (ends("\02" "at")) setto("\03" "ate"); else
              if (ends("\02" "bl")) setto("\03" "ble"); else
              if (ends("\02" "iz")) setto("\03" "ize"); else
              if (doublec(k))
              {  k--;
                 {  int ch = b[k];
                    if (ch == 'l' || ch == 's' || ch == 'z') k++;
                 }
              }
              else if (m() == 1 && cvc(k)) setto("\01" "e");
           }
             * */

            return word;
        }

        string Stem(string word)
        {
            var word1 = Step1ab( word ); 
            /*var word2 = Step1c( word1 ); 
            var word3 = Step2(word2); 
            Step3(); 
            Step4(); 
            Step5();
            return k;
            */

            return word;
        }

        internal void StemFile(string filename)
        {
            // need to replace string concat.
            var resultingString = "";

            using (TextReader reader = File.OpenText(filename))
            {
                var line = reader.ReadLine();

                var sp = line.Split();
                foreach (var word in sp)
                {
                    // if all letters, then continue.
                    // LINQ...  slow?
                    var allLetters = word.All(x => Char.IsLetter(x));
                    if (allLetters)
                    {
                        var resultWord = Stem(word);
                        targetStringList.Add(resultWord);
                    }
                    else
                    {
                        targetStringList.Add(word);
                    }

                }
            }
        }

    }
}
