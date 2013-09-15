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

        void Step1ab(string word)
        {  
            // last letter of word is 's'
            if (word[ word.Length -1] == 's')
            {  
                if (ends("\04" "sses")) k -= 2; else
                if (ends("\03" "ies")) setto("\01" "i"); else
                if (b[k-1] != 's') k--;
            }
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
        }

        string Stem(string word)
        {
            var word1 = Step1ab( word ); 
            var word2 = Step1c( word1 ); 
            var word3 = Step2(word2); 
            Step3(); 
            Step4(); 
            Step5();
            return k;

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
