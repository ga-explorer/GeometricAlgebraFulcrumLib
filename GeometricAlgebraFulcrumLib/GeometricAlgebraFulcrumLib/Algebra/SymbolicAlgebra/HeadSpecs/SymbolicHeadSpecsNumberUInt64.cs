using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberUInt64 : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberUInt64 Create(SymbolicContext context, ulong number)
        {
            return new SymbolicHeadSpecsNumberUInt64(
                context,
                number
            );
        }

        
        public SymbolicContext Context { get; }

        public double NumberFloat64Value 
            => NumberUInt64Value;

        public ulong NumberUInt64Value { get; }

        public string NumberText 
            => NumberUInt64Value.ToString();

        public string HeadText 
            => NumberUInt64Value.ToString();

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


        private SymbolicHeadSpecsNumberUInt64([NotNull] SymbolicContext context, ulong numberValue)
        {
            Context = context;
            NumberUInt64Value = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}