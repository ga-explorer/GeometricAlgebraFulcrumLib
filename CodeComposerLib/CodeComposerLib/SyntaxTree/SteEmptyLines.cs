namespace CodeComposerLib.SyntaxTree
{
    public sealed class SteEmptyLines : SteSyntaxElement
    {
        public int LinesCount { get; set; }


        public SteEmptyLines()
        {
            LinesCount = 1;
        }

        public SteEmptyLines(int count)
        {
            LinesCount = count;
        }
    }
}
