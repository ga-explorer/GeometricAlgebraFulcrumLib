using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs
{
    public sealed record MetaExpressionHeadSpecsNumberFloat64 : 
        IMetaExpressionHeadSpecsNumber
    {
        public static MetaExpressionHeadSpecsNumberFloat64 Create(MetaContext context, double number)
        {
            return new MetaExpressionHeadSpecsNumberFloat64(
                context,
                number
            );
        }

        
        public MetaContext Context { get; }

        public double NumberFloat64Value { get; }

        public string NumberText 
            => NumberFloat64Value.ToString("G");

        public string HeadText 
            => NumberFloat64Value.ToString("G");

        public bool IsNumber 
            => true;

        public bool IsSymbolicNumber 
            => false;

        public bool IsLiteralNumber 
            => true;

        public bool IsSymbolicNumberOrVariable 
            => false;

        public bool IsVariable 
            => false;

        public bool IsAtomic 
            => true;

        public bool IsComposite 
            => false;

        public bool IsFunction 
            => false;

        public bool IsOperator 
            => false;

        public bool IsArrayAccess 
            => false;


        private MetaExpressionHeadSpecsNumberFloat64(MetaContext context, double numberValue)
        {
            Context = context;
            NumberFloat64Value = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}