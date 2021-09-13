using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberUInt32 : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberUInt32 Create(SymbolicContext context, uint number)
        {
            return new SymbolicHeadSpecsNumberUInt32(
                context,
                number
            );
        }

        
        public SymbolicContext Context { get; }

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


        private SymbolicHeadSpecsNumberUInt32([NotNull] SymbolicContext context, uint numberValue)
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