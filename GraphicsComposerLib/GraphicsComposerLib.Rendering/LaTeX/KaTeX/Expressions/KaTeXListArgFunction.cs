namespace GraphicsComposerLib.Rendering.LaTeX.KaTeX.Expressions
{
    public sealed class KaTeXListArgFunction : IKaTeXExpression
    {
        private readonly List<IKaTeXExpression> _argsList 
            = new List<IKaTeXExpression>();


        public string TexCodeTemplate { get; }

        public IKaTeXExpression this[int argIndex]
        {
            get
            {
                if (argIndex < 0) 
                    throw new IndexOutOfRangeException();

                if (argIndex > _argsList.Count)
                    return null;

                return _argsList[argIndex];
            }
            set
            {
                if (argIndex < 0) 
                    throw new IndexOutOfRangeException();

                if (argIndex >= _argsList.Count)
                    _argsList.AddRange(
                        Enumerable.Repeat<IKaTeXExpression>(
                            null,
                            argIndex - _argsList.Count + 1
                        )
                    );

                _argsList[argIndex] = value;
            }
        }

        public string TexCode
            => TexCodeTemplate;
            //=> TexCodeTemplate.Replace(
            //    "texArg1", 
            //    Argument1?.TexCode ?? string.Empty
            //).Replace(
            //    "texArg2",
            //    Argument2?.TexCode ?? string.Empty
            //); 

        public bool IsLeafExpression 
            => false;

        public bool IsFunctionExpression 
            => true;

        public int ChildExpressionsCount 
            => _argsList.Count;

        public IEnumerable<IKaTeXExpression> ChildExpressions
            => _argsList;


        internal KaTeXListArgFunction(string texCodeTemplate)
        {
            TexCodeTemplate = texCodeTemplate;
        }


        public KaTeXListArgFunction ClearArguments()
        {
            _argsList.Clear();

            return this;
        }


        public override string ToString()
        {
            return TexCode;
        }
    }
}