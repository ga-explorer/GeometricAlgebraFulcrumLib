using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions
{
    public sealed class DfSin :
        DifferentialUnaryFunction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfSin Create(DifferentialFunction baseFunction, bool canBeSimplified = true) 
        {
            return new DfSin(baseFunction, canBeSimplified);
        }
    
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfSin(DifferentialFunction baseFunction, bool canBeSimplified) 
            : base(baseFunction, canBeSimplified)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            var s = Argument.GetValue(t);

            return Math.Sin(s);
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
                    DfConstant.Create(Math.Sin(argConstant.Value))
                );

            return argSimplified
                ? new Tuple<bool, DifferentialFunction>(true, new DfSin(arg, false))
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
                    DfConstant.Create(Math.Sin(constantFunction.Value)),

                _ => new DfSin(x, false)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            var x = Argument;
            var xDt = x.GetDerivative1();

            return DfTimes.Create(
                xDt, 
                DfCos.Create(x)
            ).Simplify();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunction = functionMapping(Argument);

            return new DfSin(baseFunction, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunction = functionMapping(0, Argument);

            return new DfSin(baseFunction, true);
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public override string ToString()
        //{
        //    return $"Sin[{Argument}]";
        //}

    }
}