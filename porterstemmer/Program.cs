using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace porterstemmer
{
    class PorterStemmerCommand
    {
        static void Main(string[] args)
        {

            var ps = new PorterStemmer();

            var res1 = ps.M("ca");
            var res2 = ps.M("caca");
            var res3 = ps.M("cacaca");
            var res4 = ps.M("cacacaca");

            Console.ReadKey();

        }
    }
}
