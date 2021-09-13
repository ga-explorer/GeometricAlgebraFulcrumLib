using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
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


        private SymbolicHeadSpecsNumberFloat64([NotNull] SymbolicContext context, double numberValue)
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