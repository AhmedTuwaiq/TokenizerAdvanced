using System;
using System.Linq;
using System.Collections.Generic;

namespace Tokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer tokenizer = new Tokenizer("how are you", new Tokenizable[] {
                new IdTokenizer()
            });

            Console.WriteLine(tokenizer.tokenize());
        }
    }
}
