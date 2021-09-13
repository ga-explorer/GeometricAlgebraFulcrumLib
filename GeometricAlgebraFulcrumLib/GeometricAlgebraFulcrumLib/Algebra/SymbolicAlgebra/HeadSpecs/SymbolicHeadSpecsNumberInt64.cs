using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberInt64 : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberInt64 Create(SymbolicContext context, long number)
        {
            return new SymbolicHeadSpecsNumberInt64(
                context,
                number
            );
        }

        
        public SymbolicContext Context { get; }

        public double NumberFloat64Value 
            => NumberInt64Value;

        public long NumberInt64Value { get; }

        public string NumberText 
            => NumberInt64Value.ToString();

        public string HeadText 
            => NumberInt64Value.ToString();

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


        private SymbolicHeadSpecsNumberInt64([NotNull] SymbolicContext context, long numberValue)
        {
            Context = context;
            NumberInt64Value = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}