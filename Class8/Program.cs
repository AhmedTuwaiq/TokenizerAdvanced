using System;
using System.Linq;
using System.Collections.Generic;

namespace Class8
{
    abstract class Tokenizable
    {
        public abstract bool tokenizable(Tokenizer tokenizer);
        public abstract Token tokenize(Tokenizer tokenizer);
    }

    class Token
    {
        public int Position { get; set; }
        public int LineNumber { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public Token(int position, int lineNumber, string type, string value)
        {
            this.Position = position;
            this.LineNumber = lineNumber;
            this.Type = type;
            this.Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class Input
    {
        private readonly string input;
        private readonly int length;
        private int position;
        private int lineNumber;

        //Properties
        public int Length
        {
            get
            {
                return this.length;
            }
        }
        public int Position
        {
            get
            {
                return this.position;
            }
        }
        public int NextPosition
        {
            get
            {
                return this.position + 1;
            }
        }
        public int LineNumber
        {
            get
            {
                return this.lineNumber;
            }
        }
        public char Character
        {
            get
            {
                if (this.position > -1) return this.input[this.position];
                else return '\0';
            }
        }
        public Input(string input)
        {
            this.input = input;
            this.length = input.Length;
            this.position = -1;
            this.lineNumber = 1;
        }

        public bool hasMore(int numOfSteps = 1)
        {
            if (numOfSteps <= 0 || (this.position + numOfSteps) < 0) throw new Exception("Invalid number of steps");
            return (this.position + numOfSteps) < this.length;
        }

        public bool hasLess(int numOfSteps = 1)
        {
            if (numOfSteps <= 0) throw new Exception("Invalid number of steps");
            return (this.position - numOfSteps) > -1;
        }

        //callback -> delegate
        public Input step(int numOfSteps = 1)
        {
            if (this.hasMore(numOfSteps))
                this.position += numOfSteps;
            else
            {
                throw new Exception("There is no more step");
            }
            return this;
        }

        public Input back(int numOfSteps = 1)
        {
            if (this.hasLess(numOfSteps))
                this.position -= numOfSteps;
            else
            {
                throw new Exception("There is no more step");
            }
            return this;
        }

        public Input reset()
        {
            this.position = -1;
            this.lineNumber = 1;
            return this;
        }

        public char peek(int numOfSteps = 1)
        {
            if (hasMore(numOfSteps))
            {
                return this.input[this.position + numOfSteps];
            }

            return '\0';
        }
    }

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

            while(input.hasMore() && (char.IsLetterOrDigit(ch) || ch == '_'))
            {
                token.Value += input.step().Character;
                ch = input.peek();
            }

            return token;
        }
    }

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
            return null;
        }
    }

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
