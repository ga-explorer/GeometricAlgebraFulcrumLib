using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions
{
    public abstract class DifferentialBinaryFunction
        : DifferentialCompositeFunction
    {
        public override bool IsUnary
            => false;

        public override bool IsBinary
            => true;

        public override bool IsNary
            => false;
    
        public DifferentialFunction Argument1 { get; }

        public DifferentialFunction Argument2 { get; }
    
        public override int ArgumentCount 
            => 2;

        public override IReadOnlyList<DifferentialFunction> Arguments 
            => new[] { Argument1, Argument2 };


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DifferentialBinaryFunction(DifferentialFunction baseFunction1, DifferentialFunction baseFunction2, bool canBeSimplified)
            : base(canBeSimplified)
        {
            Argument1 = baseFunction1;
            Argument2 = baseFunction2;
        }
    }
}