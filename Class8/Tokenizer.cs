using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    class Tokenizer
    {
        public readonly Input input;
        public Tokenizable[] handlers;

        public List<Token> tokens;
        public bool saveTokens;

        public Tokenizer(string source, Tokenizable[] handlers)
        {
            this.input = new Input(source);
            this.handlers = handlers;
        }

        public Tokenizer(Input input, Tokenizable[] handlers)
        {
            this.input = input;
            this.handlers = handlers;
        }

        public Token tokenize()
        {
            foreach (var handler in this.handlers)
                if (handler.tokenizable(this)) return handler.tokenize(this);

            return null;
        }

        public List<Token> all()
        {
            Token token = this.tokenize();

            if (token == null)
                return null;

            List<Token> tokens = new();
            

            while(token != null)
            {
                tokens.Add(token);
                token = this.tokenize();
            }
            
            return tokens;
        }
    }
}
