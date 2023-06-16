namespace WebComposerLib.LaTeX.KaTeX.Expressions
{
    public sealed class WclKaTeXListArgFunction : IWclKaTeXExpression
    {
        private readonly List<IWclKaTeXExpression> _argsList 
            = new List<IWclKaTeXExpression>();


        public string TexCodeTemplate { get; }

        public IWclKaTeXExpression this[int argIndex]
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
                        Enumerable.Repeat<IWclKaTeXExpression>(
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

        public IEnumerable<IWclKaTeXExpression> ChildExpressions
            => _argsList;


        internal WclKaTeXListArgFunction(string texCodeTemplate)
        {
            TexCodeTemplate = texCodeTemplate;
        }


        public WclKaTeXListArgFunction ClearArguments()
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