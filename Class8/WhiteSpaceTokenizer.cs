using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    class WhiteSpaceTokenizer : Tokenizable
    {
        public override bool tokenizable(Tokenizer tokenizer)
        {
            return tokenizer.input.peek() == ' ';
        }

        public override Token tokenize(Tokenizer tokenizer)
        {
            Input input = tokenizer.input;
            Token token = new Token(input.Position, input.LineNumber, "WhiteSpace", "");

            while(input.peek() == ' ')
                token.Value += input.step().Character;

            if (token.Value.Length == 0)
                return null;

            return token;
        }
    }
}
