using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions
{
    /// <summary>
    /// https://www.youtube.com/watch?v=vD5g8aVscUI
    /// </summary>
    public sealed class DfSmoothBlend :
        DifferentialBinaryFunction
    {
        public double MinVarValue { get; }

        public double MaxVarValue { get; }


        private DfSmoothBlend(double minVarValue, double maxVarValue, DifferentialFunction scalarFunction1, DifferentialFunction scalarFunction2, bool canBeSimplified)
            : base(scalarFunction1, scalarFunction2, canBeSimplified)
        {
            if (minVarValue.IsNaNOrInfinite() || minVarValue.IsNaNOrInfinite() || minVarValue >= maxVarValue)
                throw new ArgumentException();

            MinVarValue = minVarValue;
            MaxVarValue = maxVarValue;
        }


        private double SmoothUnitStepFunction(double t)
        {
            Debug.Assert(
                t > MinVarValue && t < MaxVarValue
            );

            //if (t <= ParameterValueMin) return 0;
            //if (t >= ParameterValueMax) return 1;

            t = (t - MinVarValue) / (MaxVarValue - MinVarValue);

            var s = 1 - t;
            var x = 1 / t - 1 / s;

            return 1 / (1 + Math.Exp(x));

            //var e1 = Math.Exp(-1d / t);
            //var e2 = Math.Exp(-1d / (1d - t));

            //return e1 / (e1 + e2);
        }


        public override Tuple<bool, DifferentialFunction> TrySimplify()
        {
            var (s1, f1) = Argument1.TrySimplify();
            var (s2, f2) = Argument2.TrySimplify();

            if (s1 || s2)
                return new Tuple<bool, DifferentialFunction>(
                    true,
                    new DfSmoothBlend(
                        MinVarValue,
                        MaxVarValue,
                        f1,
                        f2,
                        false
                    )
                );

            return new Tuple<bool, DifferentialFunction>(false, this);
        }

        public override DifferentialFunction Simplify()
        {
            return new DfSmoothBlend(
                MinVarValue,
                MaxVarValue,
                Argument1.Simplify(),
                Argument2.Simplify(),
                false
            );
        }

        public override DifferentialFunction GetDerivative1()
        {
            throw new NotImplementedException();
        }

        public override double GetValue(double t)
        {
            if (t <= MinVarValue) return Argument1.GetValue(t);
            if (t >= MaxVarValue) return Argument2.GetValue(t);

            var x = SmoothUnitStepFunction(t);
            var y = 1d - x;

            return Argument1.GetValue(t) * y + Argument2.GetValue(t) * x;
        }

        public double GetDerivativeValue(double t, int order)
        {
            throw new NotImplementedException();
        }

        public DifferentialFunction GetDerivative(int order)
        {
            throw new NotImplementedException();
        }

        public override DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping)
        {
            return new DfSmoothBlend(
                MinVarValue,
                MaxVarValue,
                functionMapping(Argument1),
                functionMapping(Argument2),
                true
            );
        }

        public override DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
        {
            return new DfSmoothBlend(
                MinVarValue,
                MaxVarValue,
                functionMapping(0, Argument1),
                functionMapping(1, Argument2),
                true
            );
        }
    }
}