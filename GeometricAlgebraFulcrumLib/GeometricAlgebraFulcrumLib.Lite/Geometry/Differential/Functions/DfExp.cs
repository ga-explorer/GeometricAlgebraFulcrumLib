using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions
{
    public sealed class DfExp :
        DifferentialUnaryFunction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfExp Create(DifferentialFunction baseFunction, bool canBeSimplified = true)
        {
            return new DfExp(baseFunction, canBeSimplified);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfExp(DifferentialFunction baseFunction, bool canBeSimplified) 
            : base(baseFunction, canBeSimplified)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            var s = Argument.GetValue(t);

            return Math.Exp(s);
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
                    DfConstant.Create(Math.Exp(argConstant.Value))
                );

            return argSimplified
                ? new Tuple<bool, DifferentialFunction>(true, new DfExp(arg, false))
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
                    DfConstant.Create(Math.Exp(constantFunction.Value)),

                _ => new DfExp(x, false)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            return DfTimes.Create(
                Argument.GetDerivative1(), 
                this
            ).Simplify();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunction = functionMapping(Argument);

            return new DfExp(baseFunction, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunction = functionMapping(0, Argument);

            return new DfExp(baseFunction, true);
        }
    }
}