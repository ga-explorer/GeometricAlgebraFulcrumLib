using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions
{
    public class SampledD0FunctionIntegrator
        : DifferentialBasicFunction
    {
        public Float64Signal TimeSignal { get; }

        public Float64Signal ValueSignal { get; }

        public DifferentialFunction DerivativeFunction { get; }

    
        public override bool CanBeSimplified 
            => false;

        public override bool IsConstant 
            => false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SampledD0FunctionIntegrator(Float64Signal valueSignal, DifferentialFunction derivativeFunction)
        {
            ValueSignal = valueSignal;
            DerivativeFunction = derivativeFunction;
            TimeSignal = valueSignal.GetSampledTimeSignal();
        }


        public override double GetValue(double t)
        {
            var (index1, index2) = 
                TimeSignal.GetSampleIndexFromTime(t);

            if (index1 == index2)
                return ValueSignal[index1];
                
            var c1 = ValueSignal[index1];
            var c2 = ValueSignal[index2];

            var t1 = TimeSignal[index1];
            var t2 = TimeSignal[index2];

            var v1 = c1 + Integrate.OnClosedInterval(
                DerivativeFunction.GetValue,
                t1,
                t
            );
        
            var v2 = c2 - Integrate.OnClosedInterval(
                DerivativeFunction.GetValue,
                t,
                t2
            );

            return (v1 + v2) / 2;
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
        public override DifferentialFunction GetDerivative1()
        {
            return DerivativeFunction.GetDerivativeN(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivativeN(int order)
        {
            return order == 0
                ? this
                : DerivativeFunction.GetDerivativeN(order - 1);
        }
    }
}