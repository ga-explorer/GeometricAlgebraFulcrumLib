namespace CodeComposerLib.SyntaxTree.Expressions
{
    public class SteNumberHeadSpecs : ISteAtomicHeadSpecs
    {
        public string NumberText { get; }

        public bool IsSymbolic { get; }

        public bool IsLiteral => !IsSymbolic;

        public string HeadText => NumberText;


        public SteNumberHeadSpecs(string numberText, bool isSymbolic)
        {
            NumberText = numberText;
            IsSymbolic = isSymbolic;
        }

        public SteNumberHeadSpecs(double number)
        {
            NumberText = number.ToString("G");
            IsSymbolic = false;
        }

        public SteNumberHeadSpecs(float number)
        {
            NumberText = number.ToString("G");
            IsSymbolic = false;
        }

        public SteNumberHeadSpecs(int number)
        {
            NumberText = number.ToString("G");
            IsSymbolic = false;
        }

        public SteNumberHeadSpecs(long number)
        {
            NumberText = number.ToString("G");
            IsSymbolic = false;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}
