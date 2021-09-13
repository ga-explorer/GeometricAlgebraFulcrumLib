using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberInt32 : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberInt32 Create(SymbolicContext context, int number)
        {
            return new SymbolicHeadSpecsNumberInt32(
                context,
                number
            );
        }

        
        public SymbolicContext Context { get; }

        public double NumberFloat64Value 
            => NumberInt32Value;

        public int NumberInt32Value { get; }

        public string NumberText 
            => NumberInt32Value.ToString();

        public string HeadText 
            => NumberInt32Value.ToString();

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


        private SymbolicHeadSpecsNumberInt32([NotNull] SymbolicContext context, int numberValue)
        {
            Context = context;
            NumberInt32Value = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}