using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Interpolators
{
    public abstract class DifferentialInterpolatorFunction :
        DifferentialCustomFunction
    {
        public override bool IsConstant 
            => false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DifferentialInterpolatorFunction() 
            : base(false)
        {
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfAkimaSplineInterpolator AsAkimaSplineInterpolator()
        {
            return (DfAkimaSplineInterpolator)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfBarycentricInterpolator AsBarycentricInterpolator()
        {
            return (DfBarycentricInterpolator)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfCatmullRomSplineInterpolator AsCatmullRomSplineInterpolator()
        {
            return (DfCatmullRomSplineInterpolator)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfCatmullRomSplineSignalInterpolator AsCatmullRomSplineSignalInterpolator()
        {
            return (DfCatmullRomSplineSignalInterpolator)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevSignalInterpolator AsChebyshevSignalInterpolator()
        {
            return (DfChebyshevSignalInterpolator)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfLinearSplineSignalInterpolator AsLinearSplineSignalInterpolator()
        {
            return (DfLinearSplineSignalInterpolator)this;
        }
    }
}