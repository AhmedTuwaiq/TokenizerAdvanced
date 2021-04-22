using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    class IdTokenizer : Tokenizable
    {
        private string[] keywords = {
            "let", "var", "if", "else", "for", "while", "fun", "return"
        };

        private bool isKeyword(string value)
        {
            return keywords.Contains(value);
        }

        public override bool tokenizable(Tokenizer tokenizer)
        {
            char ch = tokenizer.input.peek();
            return char.IsLetter(ch) || ch == '_';
        }

        public override Token tokenize(Tokenizer tokenizer)
        {
            Input input = tokenizer.input;
            Token token = new Token(input.Position, input.LineNumber, "identifier", "");

            char ch = input.peek();

            while (input.hasMore() && (char.IsLetterOrDigit(ch) || ch == '_'))
            {
                token.Value += input.step().Character;
                ch = input.peek();
            }

            return token;
        }
    }
}
