using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorterStemmerLib
{
    public class PorterStemmer
    {

        public void StemFile(string contents)
        {
            
            while (true)
            {
                int ch = getc(f);
                if (ch == EOF) return;
                if (LETTER(ch))
                {
                    int i = 0;
                    while (TRUE)
                    {
                        if (i == i_max) increase_s();

                        ch = tolower(ch); /* forces lower case */

                        s[i] = ch; i++;
                        ch = getc(f);
                        if (!LETTER(ch)) { ungetc(ch, f); break; }
                    }
                    s[stem(s, 0, i - 1) + 1] = 0;
                    /* the previous line calls the stemmer and uses its result to
                       zero-terminate the string in s */
                    printf("%s", s);
                }
                else putchar(ch);
            }
        }

    }
}
