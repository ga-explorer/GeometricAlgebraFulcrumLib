using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions
{
    public sealed class DfCos :
        DifferentialUnaryFunction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfCos Create(DifferentialFunction baseFunction, bool canBeSimplified = true)
        {
            return new DfCos(baseFunction, canBeSimplified);
        }


        public override bool IsConstant 
            => Argument.IsConstant;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfCos(DifferentialFunction baseFunction, bool canBeSimplified) 
            : base(baseFunction, canBeSimplified)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            var s = Argument.GetValue(t);

            return Math.Cos(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<bool, DifferentialFunction> TrySimplify()
        {
            if (!CanBeSimplified) 
                return new Tuple<bool, DifferentialFunction>(false, this);

            var (argSimplified, arg) = Argument.TrySimplify();

            if (arg is DfConstant argConstant)
                return new Tuple<bool, DifferentialFunction>(
                    true,
                    DfConstant.Create(Math.Cos(argConstant.Value))
                );

            return argSimplified
                ? new Tuple<bool, DifferentialFunction>(true, new DfCos(arg, false))
                : new Tuple<bool, DifferentialFunction>(false, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction Simplify()
        {
            if (!CanBeSimplified) return this;

            var x = Argument.Simplify();

            return x switch
            {
                DfConstant constantFunction => 
                    DfConstant.Create(Math.Cos(constantFunction.Value)),

                _ => new DfCos(x, false)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            var x = Argument;
            var xDt = x.GetDerivative1();

            return DfTimes.Create(
                xDt,
                DfTimes.Create(
                    -1,
                    DfSin.Create(x)
                )
            ).Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunction = functionMapping(Argument);

            return new DfCos(baseFunction, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunction = functionMapping(0, Argument);

            return new DfCos(baseFunction, true);
        }
    }
}