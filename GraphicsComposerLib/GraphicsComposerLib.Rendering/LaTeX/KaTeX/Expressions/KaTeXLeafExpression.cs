namespace GraphicsComposerLib.Rendering.LaTeX.KaTeX.Expressions
{
    public sealed class KaTeXLeafExpression : IKaTeXExpression
    {
        public string TexCode { get; }

        public bool IsLeafExpression 
            => true;

        public bool IsFunctionExpression 
            => false;

        public int ChildExpressionsCount 
            => 0;

        public IEnumerable<IKaTeXExpression> ChildExpressions
            => Enumerable.Empty<IKaTeXExpression>();


        internal KaTeXLeafExpression(string texCode)
        {
            TexCode = texCode;
        }


        public override string ToString()
        {
            return TexCode;
        }
    }
}