using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Polynomials
{
    public abstract class DfPolynomialBasis :
        DifferentialCustomFunction
    {
        public int Degree { get; }

        public override bool IsConstant 
            => Degree == 0;
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DfPolynomialBasis(int degree)
            : base(false)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            Degree = degree;
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<bool, DifferentialFunction> TrySimplify()
        {
            return new Tuple<bool, DifferentialFunction>(false, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction Simplify()
        {
            return this;
        }
    }
}