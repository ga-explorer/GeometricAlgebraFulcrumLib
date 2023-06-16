using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions
{
    public sealed class DfPowerScalar :
        DifferentialUnaryFunction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfPowerScalar Create(DifferentialFunction baseFunction, double powerValue, bool canBeSimplified = true)
        {
            return new DfPowerScalar(baseFunction, powerValue, canBeSimplified);
        }

    
        public double PowerValue { get; }

        public long PowerValueIntegerPart 
            => (long)PowerValue.IntegerPart();

        public double PowerValueFractionalPart
            => PowerValue.FractionalPart();

        public bool PowerValueIsInteger 
            => PowerValue.IsInteger();

        public bool PowerValueIsPositiveInteger
            => PowerValue > 0 && PowerValue.IsInteger();
    
        public bool PowerValueIsNegativeInteger
            => PowerValue < 0 && PowerValue.IsInteger();

        public bool PowerValueIsNonPositiveInteger
            => PowerValue <= 0 && PowerValue.IsInteger();
    
        public bool PowerValueIsNonNegativeInteger
            => PowerValue >= 0 && PowerValue.IsInteger();

        public bool PowerValueIsZero
            => PowerValue.IsZero();

        public bool PowerValueIsPositive
            => PowerValue > 0;
    
        public bool PowerValueIsNegative
            => PowerValue < 0;

        public bool PowerValueIsNonPositive
            => PowerValue <= 0;
    
        public bool PowerValueIsNonNegative
            => PowerValue >= 0;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfPowerScalar(DifferentialFunction baseFunction, double powerValue, bool canBeSimplified) 
            : base(baseFunction, canBeSimplified)
        {
            if (powerValue.IsNaNOrInfinite())
                throw new ArgumentException(nameof(powerValue));

            PowerValue = powerValue;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal DifferentialFunction ScalePowerBy(double scalar)
        {
            scalar += PowerValue;

            if (scalar == 0d) return DfConstant.One;

            if (scalar == 1d) return Argument;

            return new DfPowerScalar(
                Argument,
                scalar,
                CanBeSimplified
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            var s = Argument.GetValue(t);

            return Math.Pow(s, PowerValue);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<bool, DifferentialFunction> TrySimplify()
        {
            if (!CanBeSimplified) 
                return new Tuple<bool, DifferentialFunction>(false, this);

            return new Tuple<bool, DifferentialFunction>(true, Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction Simplify()
        {
            if (!CanBeSimplified) return this;

            if (PowerValue == 0d) return DfConstant.One;
        
            var f = Argument.Simplify();

            if (PowerValue == 1d) return f;

            if (f is DfTimes fTimes)
            {
                return fTimes.MapArguments(arg => 
                    new DfPowerScalar(
                        arg,
                        PowerValue,
                        true
                    ).Simplify()
                );
            }

            // https://en.wikipedia.org/wiki/List_of_trigonometric_identities#Power-reduction_formulae
            if (PowerValueIsPositiveInteger && PowerValue >= 2d && f is DfCos fCos)
            {
                var n = (int) PowerValueIntegerPart;
                var theta = fCos.Argument;

                if (n % 2 == 0)
                {
                    // Even n
                    var kRange = (n / 2).GetRange();
                    var s = Math.Pow(2, 1 - n);

                    return n.GetBinomialCoefficient(n / 2) / Math.Pow(2, n) +
                           DfPlus.Create(
                               kRange.Select(k =>
                                   s *
                                   n.GetBinomialCoefficient(k) *
                                   ((n - 2 * k) * theta).Cos()
                               ).ToImmutableArray()
                           ).Simplify();
                }
                else
                {
                    // Odd n
                    var kRange = ((n + 1) / 2).GetRange();
                    var s = Math.Pow(2, 1 - n);

                    return DfPlus.Create(
                        kRange.Select(k =>
                            s * n.GetBinomialCoefficient(k) *
                            ((n - 2 * k) * theta).Cos()
                        ).ToImmutableArray()
                    ).Simplify();
                }
            }

            if (PowerValueIsPositiveInteger && PowerValue >= 2d && f is DfSin fSin)
            {
                var n = (int) PowerValueIntegerPart;
                var theta = fSin.Argument;

                if (n % 2 == 0)
                {
                    // Even n
                    var kRange = (n / 2).GetRange();
                    var s = Math.Pow(2, 1 - n);

                    return n.GetBinomialCoefficient(n / 2) / Math.Pow(2, n) +
                           DfPlus.Create(
                               kRange.Select(k =>
                                   s *
                                   (n / 2 - k).IsEvenToBipolarInteger() *
                                   n.GetBinomialCoefficient(k) *
                                   ((n - 2 * k) * theta).Cos()
                               ).ToImmutableArray()
                           ).Simplify();
                }
                else
                {
                    // Odd n
                    var kRange = ((n + 1) / 2).GetRange();
                    var s = Math.Pow(2, 1 - n);

                    return DfPlus.Create(
                        kRange.Select(k =>
                            s * 
                            ((n - 1) / 2 - k).IsEvenToBipolarInteger() *
                            n.GetBinomialCoefficient(k) *
                            ((n - 2 * k) * theta).Sin()
                        ).ToImmutableArray()
                    ).Simplify();
                }
            }
        
            return f switch
            {
                DfConstant constantFunction => 
                    DfConstant.Create(Math.Pow(constantFunction.Value, PowerValue)),

                DfVar =>
                    new DfPowerScalar(DfVar.DefaultFunction, PowerValue, false),

                DfExp fExp =>
                    DfExp.Create(
                        DfTimes.Create(PowerValue, fExp.Argument)
                    ).Simplify(),
                
                DfPowerScalar fPowerScalar =>
                    new DfPowerScalar(
                        fPowerScalar.Argument,
                        fPowerScalar.PowerValue * PowerValue,
                        false
                    ).Simplify(),

                _ => 
                    new DfPowerScalar(
                        f, 
                        PowerValue, 
                        false
                    )
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
                    PowerValue,
                    new DfPowerScalar(x, PowerValue - 1, true)
                )
            ).Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunction = functionMapping(Argument);

            return new DfPowerScalar(
                baseFunction,
                PowerValue,
                true
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunction = functionMapping(0, Argument);

            return new DfPowerScalar(
                baseFunction,
                PowerValue,
                true
            );
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public override string ToString()
        //{
        //    return $"Power[{Argument}, {PowerValue}]";
        //}

    }
}