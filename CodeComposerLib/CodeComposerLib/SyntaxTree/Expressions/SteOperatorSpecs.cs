namespace CodeComposerLib.SyntaxTree.Expressions
{
    public enum SteOperatorPosition { Infix = 0, Prefix = 1, Suffix = 2 }

    public enum SteOperatorAssociation { None = 0, Left = 1, Right = 2 }


    public class SteOperatorSpecs : ISteCompositeHeadSpecs
    {
        public int Precedence { get; private set; }

        public string Symbol { get; }

        public string TrimmedSymbol => Symbol.Trim();

        public SteOperatorPosition Position { get; private set; }

        public SteOperatorAssociation Association { get; private set; }

        public string HeadText => Symbol;


        public SteOperatorSpecs(string opSymbol, int opPrecedence, SteOperatorPosition opPosition, SteOperatorAssociation opAssociation)
        {
            Precedence = opPrecedence;
            Association = opAssociation;
            Symbol = opSymbol;
            Position = opPosition;
        }


        public override string ToString()
        {
            return Symbol;
        }
    }
}
