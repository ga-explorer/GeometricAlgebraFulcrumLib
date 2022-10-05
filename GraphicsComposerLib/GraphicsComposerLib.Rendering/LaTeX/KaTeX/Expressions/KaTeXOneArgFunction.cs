namespace GraphicsComposerLib.Rendering.LaTeX.KaTeX.Expressions
{
    public sealed class KaTeXOneArgFunction : IKaTeXExpression
    {
        public string TexCodeTemplate { get; }

        public IKaTeXExpression Argument { get; set; }

        public string TexCode
            => TexCodeTemplate.Replace(
                "texArg1", 
                Argument.TexCode
            ); 

        public bool IsLeafExpression 
            => false;

        public bool IsFunctionExpression 
            => true;

        public int ChildExpressionsCount 
            => 1;

        public IEnumerable<IKaTeXExpression> ChildExpressions
        {
            get { yield return Argument; }
        }


        internal KaTeXOneArgFunction(string texCodeTemplate)
        {
            TexCodeTemplate = texCodeTemplate;
        }


        public override string ToString()
        {
            return TexCode;
        }
    }
}