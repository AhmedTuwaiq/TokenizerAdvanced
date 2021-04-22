using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    class Token
    {
        public int n = 01;
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
}
