namespace GraphicsComposerLib.Rendering.LaTeX.KaTeX.Expressions
{
    public sealed class KaTeXTwoArgFunction : IKaTeXExpression
    {
        public string TexCodeTemplate { get; }

        public IKaTeXExpression Argument1 { get; set; }

        public IKaTeXExpression Argument2 { get; set; }

        public string TexCode
            => TexCodeTemplate.Replace(
                "texArg1", 
                Argument1?.TexCode ?? string.Empty
            ).Replace(
                "texArg2",
                Argument2?.TexCode ?? string.Empty
            ); 

        public bool IsLeafExpression 
            => false;

        public bool IsFunctionExpression 
            => true;

        public int ChildExpressionsCount 
            => 2;

        public IEnumerable<IKaTeXExpression> ChildExpressions
        {
            get
            {
                yield return Argument1;
                yield return Argument2;
            }
        }


        internal KaTeXTwoArgFunction(string texCodeTemplate)
        {
            TexCodeTemplate = texCodeTemplate;
        }


        public override string ToString()
        {
            return TexCode;
        }
    }
}