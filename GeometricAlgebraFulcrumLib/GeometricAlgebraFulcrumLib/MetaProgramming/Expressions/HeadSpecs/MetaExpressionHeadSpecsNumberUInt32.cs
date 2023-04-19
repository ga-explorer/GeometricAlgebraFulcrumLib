using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs
{
    public sealed record MetaExpressionHeadSpecsNumberUInt32 : 
        IMetaExpressionHeadSpecsNumber
    {
        public static MetaExpressionHeadSpecsNumberUInt32 Create(MetaContext context, uint number)
        {
            return new MetaExpressionHeadSpecsNumberUInt32(
                context,
                number
            );
        }

        
        public MetaContext Context { get; }

        public double NumberFloat64Value 
            => NumberUInt32Value;

        public uint NumberUInt32Value { get; }

        public string NumberText 
            => NumberUInt32Value.ToString();

        public string HeadText 
            => NumberUInt32Value.ToString();

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


        private MetaExpressionHeadSpecsNumberUInt32(MetaContext context, uint numberValue)
        {
            Context = context;
            NumberUInt32Value = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}