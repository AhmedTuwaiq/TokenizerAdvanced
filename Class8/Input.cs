using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
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

        public bool withInRange(int index)
        {
            return index > length && -1 < index;
        }

        public string substring(int startIndex, int length)
        {
            if (withInRange(startIndex) && withInRange(length))
                return this.input.Substring(startIndex, length);

            return null;
        }
    }
}
