using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberFloat64 : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberFloat64 Create(SymbolicContext context, double number)
        {
            return new SymbolicHeadSpecsNumberFloat64(
                context,
                number
            );
        }

        
        public SymbolicContext Context { get; }

        public double NumberValue { get; }

        public string NumberText 
            => NumberValue.ToString("G");

        public string HeadText 
            => NumberValue.ToString("G");

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


        private SymbolicHeadSpecsNumberFloat64([NotNull] SymbolicContext context, double numberValue)
        {
            Context = context;
            NumberValue = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}